﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server_v0._1._0
{
    class Program
    {
        static TcpListener server = null;
        static Int32 port = 22222;
        static void Main(string[] args)
        {
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                Console.WriteLine(">> " + "Ожидание подключения первого игрока... ");
                while (true)
                {

                    TcpClient client1 = server.AcceptTcpClient();
                    Console.WriteLine(">> " + "Подключился первый игрок");
                    Console.WriteLine(">> " + "Ожидание подключения второго игрока... ");
                    TcpClient client2 = server.AcceptTcpClient();
                    Console.WriteLine(">> " + "Подключился второй игрок");
                    Game game = new Game(client1, client2);
                    Thread clientThread = new Thread(new ThreadStart(game.Process));
                    clientThread.Start();
                }
            }
            finally { server.Stop(); }
        }
    }
    public class Game
    {
        int Mana1=1 , Mana2 = 1;
        List<Card> deckPlayer1 = new List<Card>();
        List<Card> deckPlayer2 = new List<Card>();
        List<Card> deckPlayer1InHand = new List<Card>();
        List<Card> deckPlayer2InHand = new List<Card>();
        List<Card> deckPlayer1OnBord = new List<Card>();
        List<Card> deckPlayer2OnBord = new List<Card>();
        TcpClient client1;
        TcpClient client2;
        public Game(TcpClient tcpClient1, TcpClient tcpClient2)
        {
            client1 = tcpClient1;
            client2 = tcpClient2;
        }
        public void Process()
        {
            NetworkStream stream1 = null;
            NetworkStream stream2 = null;
            try
            {
                string[] lines;
                stream1 = client1.GetStream();
                stream2 = client2.GetStream();
                Byte[] bytes1 = new Byte[4096];
                Byte[] bytes2 = new Byte[4096];
                String data1 = null;
                String data2 = null;
                Random rnd = new Random(1);
                Random rndCard = new Random(0);
                while (true)
                {
                    
                    byte[] msg;
                    data1 = null;
                    data2 = null;
                    int i1;
                    int i2;
                    msg = System.Text.Encoding.ASCII.GetBytes("29");
                    stream1.Write(msg, 0, msg.Length);
                    stream2.Write(msg, 0, msg.Length);
                    while ((i1 = stream1.Read(bytes1, 0, bytes1.Length)) != 0 && (i2 = stream2.Read(bytes2, 0, bytes2.Length)) != 0)
                    {
                        data1 = System.Text.Encoding.ASCII.GetString(bytes1, 0, i1);
                        data1 = data1.Substring(0, data1.Length - 1);
                        lines = data1.Split(';');

                        foreach (string l in lines)
                        {
                            if (l[2] == 'H')
                                deckPlayer1.Add(JsonConvert.DeserializeObject<Minion>(l));
                            else
                                deckPlayer1.Add(JsonConvert.DeserializeObject<Spell>(l));
                        }

                        data2 = System.Text.Encoding.ASCII.GetString(bytes2, 0, i2);
                        data2 = data2.Substring(0, data2.Length - 1);
                        lines = data2.Split(';');

                        foreach (string l in lines)
                        {
                            if (l[2] == 'H')
                                deckPlayer2.Add(JsonConvert.DeserializeObject<Minion>(l));
                            else
                                deckPlayer2.Add(JsonConvert.DeserializeObject<Spell>(l));
                        }

                        for (int i = 0; i < 7; i++)
                        {
                            int a = rndCard.Next(deckPlayer1.Count - i);
                            int b = rndCard.Next(deckPlayer2.Count - i);
                            deckPlayer1InHand.Add(deckPlayer1[a]);
                            deckPlayer2InHand.Add(deckPlayer2[b]);
                            deckPlayer1.RemoveAt(a);
                            deckPlayer2.RemoveAt(b);
                        }

                        string mes1 = "";
                        string mes2 = "";
                        
                        foreach (Card c in deckPlayer1InHand)
                        {
                            if (c is Minion)
                                mes1 += JsonConvert.SerializeObject((Minion)c);
                            else
                                mes1 += JsonConvert.SerializeObject((Spell)c);
                            mes1 += ';';
                        }

                        foreach (Card c in deckPlayer2InHand)
                        {
                            if (c is Minion)
                                mes1 += JsonConvert.SerializeObject((Minion)c);
                            else
                                mes1 += JsonConvert.SerializeObject((Spell)c);
                            mes1 += ';';
                        }

                        foreach (Card c in deckPlayer2InHand)
                        {
                            if (c is Minion)
                                mes2 += JsonConvert.SerializeObject((Minion)c);
                            else
                                mes2 += JsonConvert.SerializeObject((Spell)c);
                            mes2 += ';';
                        }

                        foreach (Card c in deckPlayer1InHand)
                        {
                            if (c is Minion)
                                mes2 += JsonConvert.SerializeObject((Minion)c);
                            else
                                mes2 += JsonConvert.SerializeObject((Spell)c);
                            mes2 += ';';
                        }
                        mes1 += deckPlayer1.Count.ToString(); mes1 += ';';
                        mes2 += deckPlayer2.Count.ToString(); mes2 += ';';
                        mes1 += "Your step";
                        mes2 += "DYour step";
                        msg = System.Text.Encoding.ASCII.GetBytes(mes1);
                        stream1.Write(msg, 0, msg.Length);
                        msg = System.Text.Encoding.ASCII.GetBytes(mes2);
                        stream2.Write(msg, 0, msg.Length);
                        int curMana = 1;
                        Step(stream1, stream2, deckPlayer1InHand, deckPlayer2InHand,deckPlayer1OnBord,deckPlayer2OnBord,deckPlayer1,deckPlayer2,Mana1,Mana2,curMana);
                    }
                }
            }   
            catch (SocketException e)
            {
                Console.WriteLine(">> " + "SocketException: {0}", e);
            }
            finally
            {
                client1.Close();
                client2.Close();
                stream1.Close();
                stream2.Close();
            }
            Console.WriteLine("\n>> " + "Hit enter to continue...");
            Console.Read();
        }
        
        public void Step(NetworkStream stream1, NetworkStream stream2, List<Card> cards1, List<Card> cards2, List<Card> cardsbord1, List<Card> cardsbord2, List<Card> allCards1, List<Card> allCards2, int mana1,int mana2, int curMana)
        {

            int count1, count2;
            int i;
            Byte[] bytes = new Byte[4096];
            while ((i = stream1.Read(bytes, 0, bytes.Length)) != 0)
            {
                String data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                #region  drop card on the bord
                
                if (int.TryParse(data, out count1))
                {

                    if (cardsbord2.Count <= 7)
                    {
                        if (curMana >= cards1[count1].Cost)
                        {
                            curMana -= cards1[count1].Cost;
                            String mes = "";
                            String mes2 = "Hand;";
                            if (cards1[count1] is Minion minion)
                            {
                                cardsbord1.Add(new Minion(minion.Name, minion.Cost, minion.Health, minion.Damage));
                                mes = JsonConvert.SerializeObject((Minion)cardsbord1[cardsbord1.Count - 1]);
                                mes2 += JsonConvert.SerializeObject((Minion)cardsbord1[cardsbord1.Count - 1]);
                            }
                            else
                            {
                                Spell spell = (Spell)cards1[count1];
                                cardsbord1.Add(new Spell(spell.Name, spell.Cost, spell.MagicDamage));
                                mes = JsonConvert.SerializeObject((Spell)cardsbord1[cardsbord1.Count - 1]);
                                mes2 += JsonConvert.SerializeObject((Spell)cardsbord1[cardsbord1.Count - 1]);
                            }
                            cards1.RemoveAt(count1);
                            foreach (Card c in cards1)
                            {
                                mes += ';';
                                if (c is Minion)
                                    mes += JsonConvert.SerializeObject((Minion)c);
                                else
                                    mes += JsonConvert.SerializeObject((Spell)c);

                            }

                            foreach (Card c in cards1)
                            {
                                mes2 += ';';
                                if (c is Minion)
                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                else
                                    mes2 += JsonConvert.SerializeObject((Spell)c);
                            }
                            mes2 += ';' + curMana.ToString();
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mes);
                            stream2.Write(msg, 0, msg.Length);
                            msg = System.Text.Encoding.ASCII.GetBytes(mes2);
                            stream1.Write(msg, 0, msg.Length);
                        }
                        else
                        {
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes("Bord is full.");
                            stream1.Write(msg, 0, msg.Length);
                        }
                    }
                    else
                    {
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("Not enough energy.");
                        stream1.Write(msg, 0, msg.Length);
                    }
                    Step(stream1, stream2, cards1, cards2, cardsbord1, cardsbord2,allCards1,allCards2, mana1, mana2,curMana);
                }
                #endregion
                else
                {
                    if (data == "End step")
                    {
                        foreach (Minion m in cardsbord1)
                        {
                            m.CanAttack = true;
                        }
                        
                        String mes = "";
                        if(mana1<10) mana1++;
                        byte[] msg;
                        if (allCards2.Count > 0)
                        {
                            Random rndCard = new Random(0);
                            int b = rndCard.Next(allCards2.Count);
                            cards2.Add(allCards2[b]);
                            allCards2.RemoveAt(b);
                            foreach (Card c in cards2)
                            {
                                mes += ';';
                                if (c is Minion)
                                    mes += JsonConvert.SerializeObject((Minion)c);
                                else
                                    mes += JsonConvert.SerializeObject((Spell)c);

                            }
                            mes += ";next";
                            foreach (Card c in deckPlayer2)
                            {
                                mes += ';';
                                if (c is Minion)
                                    mes += JsonConvert.SerializeObject((Minion)c);
                                else
                                    mes += JsonConvert.SerializeObject((Spell)c);

                            }
                        }
                        mes += ';' + mana1.ToString();
                        curMana = mana1;
                        msg = System.Text.Encoding.ASCII.GetBytes("Your step" + mes);
                        stream2.Write(msg, 0, msg.Length);
                        msg = System.Text.Encoding.ASCII.GetBytes("DYour step" + mes);
                        stream1.Write(msg, 0, msg.Length);
                        Step(stream2, stream1, cards2, cards1, cardsbord2, cardsbord1,allCards2,allCards1, mana2, mana1,curMana);
                    }
                    else
                    {
                        byte[] msg;
                        string mes1 = "";
                        string mes2 = "";
                        string[] counts = data.Split(';');
                        count1 = int.Parse(counts[0]);
                        count2 = int.Parse(counts[1]);
                        Minion m1 = (Minion)cardsbord1[count1];
                        Minion m2 = (Minion)cardsbord2[count2];
                        if (m1.CanAttack)
                        {
                            m1.Health -= m2.Damage;
                            m2.Health -= m1.Damage;
                            if (m1.Health > 0)
                                cardsbord1[count1] = m1;
                            else
                                cardsbord1.RemoveAt(count1);
                            if (m2.Health > 0)
                                cardsbord2[count2] = m2;
                            else
                                cardsbord2.RemoveAt(count2);
                            m1.CanAttack = false;
                            mes1 += "Attac";
                            mes2 += "Attac";

                            foreach (Card c in cardsbord1)
                            {
                                mes1 += ';';
                                if (c is Minion)
                                    mes1 += JsonConvert.SerializeObject((Minion)c);
                                else
                                    mes1 += JsonConvert.SerializeObject((Spell)c);
                                
                            }
                            mes1 += ";next";

                            foreach (Card c in cardsbord2)
                            {
                                mes1 += ';';
                                if (c is Minion)
                                    mes1 += JsonConvert.SerializeObject((Minion)c);
                                else
                                    mes1 += JsonConvert.SerializeObject((Spell)c);
                                
                            }

                            foreach (Card c in cardsbord2)
                            {
                                mes2 += ';';
                                if (c is Minion)
                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                else
                                    mes2 += JsonConvert.SerializeObject((Spell)c);
                                
                            }
                            mes2 += ";next";
                            foreach (Card c in cardsbord1)
                            {
                                mes2 += ';';
                                if (c is Minion)
                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                else
                                    mes2 += JsonConvert.SerializeObject((Spell)c);
                                
                            }
                            msg = System.Text.Encoding.ASCII.GetBytes(mes1);
                            stream1.Write(msg, 0, msg.Length);
                            msg = System.Text.Encoding.ASCII.GetBytes(mes2);
                            stream2.Write(msg, 0, msg.Length);
                        }
                        else
                        {
                            msg = System.Text.Encoding.ASCII.GetBytes("Card can not attack.");
                            stream1.Write(msg, 0, msg.Length);
                        }
                        Step(stream1, stream2, cards1, cards2, cardsbord1, cardsbord2,allCards1,allCards2, mana1, mana2,curMana);
                    }
                }
            }
        }
    }
}
