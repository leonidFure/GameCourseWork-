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
                
                while (true)
                {
                    Console.WriteLine(">> " + "Ожидание подключения первого игрока... ");
                    TcpClient client1 = server.AcceptTcpClient();
                    Console.WriteLine(">> " + "Подключился новый игрок");
                    Console.WriteLine(">> " + "Ожидание подключения второго игрока... ");
                    TcpClient client2 = server.AcceptTcpClient();
                    Console.WriteLine(">> " + "Подключился второй игрок");
                    Connect(client1, client2);
                }
            }
            finally { server.Stop(); }
        }
        static void Connect(TcpClient client1, TcpClient client2)
        {
            try
            {
                NetworkStream networkStream1 = client1.GetStream();
                NetworkStream networkStream2 = client2.GetStream();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("Are you ready?");
                networkStream1.Write(msg, 0, msg.Length);
                networkStream2.Write(msg, 0, msg.Length);
                Game game = new Game(client1, client2);
                Thread clientThread = new Thread(new ThreadStart(game.Process));
                clientThread.Start();
            }
            catch
            {
                client1 = client2;
                client2 = null;
                client2 = server.AcceptTcpClient();
                Connect(client1, client2);
            }
        }
    }
    public class Game
    {
        
        Player player1 = new Player(30, 1);
        Player player2 = new Player(30, 1);
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

                    bool testDecks1 = true;
                    bool testDecks2 = true; 
                    byte[] msg;
                    byte[] msg1, msg2;
                    data1 = null;
                    data2 = null;
                    int i1;
                    int i2;
                    i1 = stream1.Read(bytes1, 0, bytes1.Length);
                    i2 = stream2.Read(bytes2, 0, bytes2.Length);
                    data1 = System.Text.Encoding.ASCII.GetString(bytes1, 0, i1);
                    data2 = System.Text.Encoding.ASCII.GetString(bytes2, 0, i2);
                    msg1 = System.Text.Encoding.ASCII.GetBytes(player1.Health.ToString() + ';' + data1 + ';' + data2);
                    msg2 = System.Text.Encoding.ASCII.GetBytes(player2.Health.ToString() + ';' + data2 + ';' + data1);
                    stream1.Write(msg1, 0, msg1.Length);
                    stream2.Write(msg2, 0, msg2.Length);

                    i1 = stream1.Read(bytes1, 0, bytes1.Length);
                    i2 = stream2.Read(bytes2, 0, bytes2.Length);
                    data1 = System.Text.Encoding.ASCII.GetString(bytes1, 0, i1);
                    data1 = data1.Substring(0, data1.Length - 1);
                    lines = data1.Split(';');

                    foreach (string l in lines)
                    {
                        if (l[2] == 'H')
                            player1.MyDeck.Add(JsonConvert.DeserializeObject<Minion>(l));
                        if (l[2] == 'P')
                            player1.MyDeck.Add(JsonConvert.DeserializeObject<TargetSpell>(l));
                        if (l[2] == 'D')
                            player1.MyDeck.Add(JsonConvert.DeserializeObject<MassSpell>(l));
                    }

                    data2 = System.Text.Encoding.ASCII.GetString(bytes2, 0, i2);
                    data2 = data2.Substring(0, data2.Length - 1);
                    lines = data2.Split(';');

                    foreach (string l in lines)
                    {
                        if (l[2] == 'H')
                            player2.MyDeck.Add(JsonConvert.DeserializeObject<Minion>(l));
                        if (l[2] == 'P')
                            player2.MyDeck.Add(JsonConvert.DeserializeObject<TargetSpell>(l));
                        if (l[2] == 'D')
                            player2.MyDeck.Add(JsonConvert.DeserializeObject<MassSpell>(l));
                    }
                    if (player1.MyDeck.Count == 30 && player2.MyDeck.Count == 30)
                    {
                        foreach (Card c in player1.MyDeck)
                        {
                            if (c is Minion minion)
                            {
                                if (minion.Cost < 0|| minion.Name.Length <= 0 || minion.Health < 0 || minion.Damage < 0)
                                {
                                    testDecks1 = false;
                                }
                                if (minion.Cost > int.MaxValue ||  minion.Health > int.MaxValue || minion.Damage > int.MaxValue)
                                {
                                    testDecks1 = false;
                                }

                            }
                            if (c is MassSpell spell)
                            {
                                if (spell.Cost < 0 || spell.Feature.Length <= 0 || spell.Name.Length <= 0)
                                {
                                    testDecks1 = false;
                                }
                                if (spell.Cost > int.MaxValue)
                                {
                                    testDecks1 = false;
                                }
                            }
                            if (c is TargetSpell target)
                            {
                                if (target.Cost < 0 || target.Feature.Length <= 0 || target.Name.Length <= 0|| target.Points < 0)
                                {
                                    testDecks1 = false;
                                }
                                if (target.Cost > int.MaxValue || target.Points > int.MaxValue)
                                {
                                    testDecks1 = false;
                                }
                            }
                        }
                        foreach (Card c in player2.MyDeck)
                        {
                            if (c is Minion minion)
                            {
                                if (minion.Cost < 0 || minion.Name.Length <= 0 || minion.Health < 0 || minion.Damage < 0)
                                {
                                    testDecks1 = false;
                                }
                                if (minion.Cost > int.MaxValue || minion.Health > int.MaxValue || minion.Damage > int.MaxValue)
                                {
                                    testDecks1 = false;
                                }
                            }
                            if (c is MassSpell spell)
                            {
                                if (spell.Cost < 0 || spell.Feature.Length <= 0 || spell.Name.Length <= 0)
                                {
                                    testDecks1 = false;
                                }
                                if (spell.Cost > int.MaxValue)
                                {
                                    testDecks1 = false;
                                }
                            }
                            if (c is TargetSpell target)
                            {
                                if (target.Cost < 0 || target.Feature.Length <= 0 || target.Name.Length <= 0 || target.Points < 0)
                                {
                                    testDecks1 = false;
                                }
                                if (target.Cost > int.MaxValue || target.Points > int.MaxValue)
                                {
                                    testDecks1 = false;
                                }
                            }
                        }
                        if (testDecks1 && testDecks2)
                        {

                            for (int i = 0; i < 7; i++)
                            {
                                int a = rndCard.Next(player1.MyDeck.Count - i);
                                int b = rndCard.Next(player2.MyDeck.Count - i);
                                player1.CardsInMyHand.Add(player1.MyDeck[a]);
                                player2.CardsInMyHand.Add(player2.MyDeck[b]);
                                player1.MyDeck.RemoveAt(a);
                                player2.MyDeck.RemoveAt(b);
                            }

                            string mes1 = "";
                            string mes2 = "";

                            foreach (Card c in player1.CardsInMyHand)
                            {
                                if (c is Minion)
                                    mes1 += JsonConvert.SerializeObject((Minion)c);
                                if(c is MassSpell mass)
                                    mes1+= JsonConvert.SerializeObject(mass);
                                if (c is TargetSpell target)
                                    mes1 += JsonConvert.SerializeObject(target);
                                mes1 += ';';
                            }

                            foreach (Card c in player2.CardsInMyHand)
                            {
                                if (c is Minion)
                                    mes1 += JsonConvert.SerializeObject((Minion)c);
                                if (c is MassSpell mass)
                                    mes1 += JsonConvert.SerializeObject(mass);
                                if (c is TargetSpell target)
                                    mes1 += JsonConvert.SerializeObject(target);
                                mes1 += ';';
                            }

                            foreach (Card c in player2.CardsInMyHand)
                            {
                                if (c is Minion)
                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                if (c is MassSpell mass)
                                    mes2 += JsonConvert.SerializeObject(mass);
                                if (c is TargetSpell target)
                                    mes2 += JsonConvert.SerializeObject(target);
                                mes2 += ';';
                            }

                            foreach (Card c in player1.CardsInMyHand)
                            {
                                if (c is Minion)
                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                if (c is MassSpell mass)
                                    mes2 += JsonConvert.SerializeObject(mass);
                                if (c is TargetSpell target)
                                    mes2 += JsonConvert.SerializeObject(target);
                                mes2 += ';';
                            }

                            mes1 += player1.MyDeck.Count.ToString(); mes1 += ';';
                            mes2 += player2.MyDeck.Count.ToString(); mes2 += ';';
                            mes1 += "Your step";
                            mes2 += "DYour step";
                            msg = System.Text.Encoding.ASCII.GetBytes(mes1);
                            stream1.Write(msg, 0, msg.Length);
                            msg = System.Text.Encoding.ASCII.GetBytes(mes2);
                            stream2.Write(msg, 0, msg.Length);
                            Step(stream1, stream2, player1, player2, 1);
                            return;
                        }
                        else
                        {
                            if (!testDecks1)
                            {
                                msg = System.Text.Encoding.ASCII.GetBytes("You have bad deck");
                                stream1.Write(msg, 0, msg.Length);


                                if (!testDecks2)
                                {
                                    msg = System.Text.Encoding.ASCII.GetBytes("You have bad deck");
                                    stream2.Write(msg, 0, msg.Length);
                                }
                                else
                                {
                                    msg = System.Text.Encoding.ASCII.GetBytes("Your oponent have bad deck");
                                    stream2.Write(msg, 0, msg.Length);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (player1.MyDeck.Count != 30)
                        {
                            msg = System.Text.Encoding.ASCII.GetBytes("You have bad deck");
                            stream1.Write(msg, 0, msg.Length);


                            if (player2.MyDeck.Count != 30)
                            {
                                msg = System.Text.Encoding.ASCII.GetBytes("You have bad deck");
                                stream2.Write(msg, 0, msg.Length);
                            }
                            else
                            {
                                msg = System.Text.Encoding.ASCII.GetBytes("Your oponent have bad deck");
                                stream2.Write(msg, 0, msg.Length);
                            }
                        }
                        return;
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(">> " + "SocketException: {0}", e);
            }
            catch (System.IO.IOException)
            {
                try
                {
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("You win");
                    stream1.Write(msg, 0, msg.Length);
                    msg = System.Text.Encoding.ASCII.GetBytes("You lose");
                    stream2.Write(msg, 0, msg.Length);
                }
                catch
                {

                }
                try
                {
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("You win");
                    stream2.Write(msg, 0, msg.Length);
                    msg = System.Text.Encoding.ASCII.GetBytes("You lose");
                    stream1.Write(msg, 0, msg.Length);
                }
                catch
                {

                }
                Console.WriteLine("Error");
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
        
        public void Step(NetworkStream stream1, NetworkStream stream2, Player player1,Player player2, int curMana)
        {
            try
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

                        if (player1.MyCardsOnBord.Count < 7)
                        {
                            if (curMana >= player1.CardsInMyHand[count1].Cost)
                            {
                                if (player1.CardsInMyHand[count1] is TargetSpell)
                                {
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("Please, choose card.");
                                    stream1.Write(msg, 0, msg.Length);
                                }
                                else
                                {
                                    curMana -= player1.CardsInMyHand[count1].Cost;
                                    String mes = "";
                                    String mes2 = "Hand;";
                                    if (player1.CardsInMyHand[count1] is Minion minion)
                                    {
                                        if (minion.IsCharge)
                                            player1.MyCardsOnBord.Add(new Minion(minion.Name, minion.Cost, minion.Health, minion.Damage, minion.IsTaunt, minion.IsCharge, true));
                                        else
                                            player1.MyCardsOnBord.Add(new Minion(minion.Name, minion.Cost, minion.Health, minion.Damage, minion.IsTaunt, minion.IsCharge));
                                        if (minion.IsTaunt)
                                            player1.HaveTaunt++;
                                        mes = JsonConvert.SerializeObject((Minion)player1.MyCardsOnBord[player1.MyCardsOnBord.Count - 1]);
                                        mes2 += JsonConvert.SerializeObject((Minion)player1.MyCardsOnBord[player1.MyCardsOnBord.Count - 1]);
                                    }
                                    if (player1.CardsInMyHand[count1] is MassSpell mass)
                                    {
                                        if (mass.Feature == "damage")
                                        {
                                            mes = "AOESpell";
                                            mes2 = "AOEEnemySpell";
                                            List<Minion> Minions = new List<Minion>();
                                            foreach (Card c in player2.MyCardsOnBord)
                                            {
                                                if (c is Minion m)
                                                {
                                                    m.Health -= mass.Drow;
                                                    if (m.Health > 0)
                                                    {
                                                        Minions.Add(m);
                                                    }
                                                }
                                            }
                                            player2.MyCardsOnBord.Clear();
                                            foreach (Minion min in Minions)
                                            {
                                                player2.MyCardsOnBord.Add(min);
                                            }
                                            foreach (Card c in player2.MyCardsOnBord)
                                            {
                                                mes += ';';
                                                if (c is Minion)
                                                    mes += JsonConvert.SerializeObject((Minion)c);
                                            }
                                            mes += ";next";
                                            foreach (Card c in player2.MyCardsOnBord)
                                            {
                                                mes2 += ';';
                                                if (c is Minion)
                                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                            }
                                            mes2 += ";next";
                                        }
                                        if (mass.Feature == "heal")
                                        {
                                            mes2 = "AOESpell";
                                            mes = "AOEEnemySpell";
                                            foreach (Card c in player1.MyCardsOnBord)
                                            {
                                                if (c is Minion m)
                                                {
                                                    m.Health += mass.Drow;
                                                }
                                            }
                                            foreach (Card c in player1.MyCardsOnBord)
                                            {
                                                mes += ';';
                                                if (c is Minion)
                                                    mes += JsonConvert.SerializeObject((Minion)c);
                                            }
                                            mes += ";next";
                                            foreach (Card c in player1.MyCardsOnBord)
                                            {
                                                mes2 += ';';
                                                if (c is Minion)
                                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                            }
                                            mes2 += ";next";
                                        }
                                    }
                                    player1.CardsInMyHand.RemoveAt(count1);
                                    foreach (Card c in player1.CardsInMyHand)
                                    {
                                        mes += ';';
                                        if (c is Minion)
                                            mes += JsonConvert.SerializeObject((Minion)c);
                                        if (c is MassSpell mas)
                                            mes += JsonConvert.SerializeObject(mas);
                                        if (c is TargetSpell target)
                                            mes += JsonConvert.SerializeObject(target);
                                    }

                                    foreach (Card c in player1.CardsInMyHand)
                                    {
                                        mes2 += ';';
                                        if (c is Minion)
                                            mes2 += JsonConvert.SerializeObject((Minion)c);
                                        if (c is MassSpell mas)
                                            mes2 += JsonConvert.SerializeObject(mas);
                                        if (c is TargetSpell target)
                                            mes2 += JsonConvert.SerializeObject(target);
                                    }

                                    mes2 += ';' + curMana.ToString(); mes2 += ';' + player1.MyDeck.Count.ToString();
                                    mes += ';' + curMana.ToString(); ; mes += ';' + player1.MyDeck.Count.ToString();
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mes);
                                    stream2.Write(msg, 0, msg.Length);
                                    msg = System.Text.Encoding.ASCII.GetBytes(mes2);
                                    stream1.Write(msg, 0, msg.Length);

                                }
                            }
                            else
                            {
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes("Not enough energy.");
                                stream1.Write(msg, 0, msg.Length);
                            }
                            
                        }
                        else
                        {
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes("Bord is full.");
                            stream1.Write(msg, 0, msg.Length);
                        }

                        Step(stream1, stream2, player1, player2, curMana);
                        return;
                    }
                    #endregion
                    else
                    {
                        if (data == "End step")
                        {
                            foreach (Minion m in player1.MyCardsOnBord)
                            {
                                m.CanAttack = true;
                            }
                            String mes = "";
                            if (player1.Energy < 10) player1.Energy++;
                            byte[] msg;
                            if (player2.MyDeck.Count > 0)
                            {
                                Random rndCard = new Random(0);
                                int b = rndCard.Next(player2.MyDeck.Count);
                                player2.CardsInMyHand.Add(player2.MyDeck[b]);
                                player2.MyDeck.RemoveAt(b);
                                foreach (Card c in player2.CardsInMyHand)
                                {
                                    mes += ';';
                                    if (c is Minion)
                                        mes += JsonConvert.SerializeObject((Minion)c);
                                    else
                                        mes += JsonConvert.SerializeObject((Spell)c);

                                }
                                mes += ";next";
                                foreach (Card c in player2.MyDeck)
                                {
                                    mes += ';';
                                    if (c is Minion)
                                        mes += JsonConvert.SerializeObject((Minion)c);
                                    else
                                        mes += JsonConvert.SerializeObject((Spell)c);

                                }
                            }
                            mes += ';' + player1.Energy.ToString();
                            curMana = player1.Energy;
                            msg = System.Text.Encoding.ASCII.GetBytes("Your step" + mes);
                            stream2.Write(msg, 0, msg.Length);
                            msg = System.Text.Encoding.ASCII.GetBytes("DYour step" + mes);
                            stream1.Write(msg, 0, msg.Length);
                            Step(stream2, stream1, player2, player1, curMana);
                            return;
                        }
                        else
                        {
                            try
                            {
                                byte[] msg;
                                string mes1 = "";
                                string mes2 = "";
                                string[] counts = data.Split(';');
                                if (data == "End game")
                                {
                                    msg = System.Text.Encoding.ASCII.GetBytes("You win");
                                    stream2.Write(msg, 0, msg.Length);
                                    msg = System.Text.Encoding.ASCII.GetBytes("You lose");
                                    stream1.Write(msg, 0, msg.Length);
                                    client1.Close();
                                    client2.Close();
                                    stream1.Close();
                                    stream2.Close();
                                }
                                if (int.TryParse(counts[1], out count2))
                                {
                                    count1 = int.Parse(counts[0]);
                                    count2 = int.Parse(counts[1]);
                                    ///
                                    ///исправить ошибкуx
                                    ///
                                    if (counts.Length == 3)
                                    {
                                        if (player1.CardsInMyHand[count1] is TargetSpell target)
                                        {
                                            curMana -= target.Cost;
                                            Minion minion;
                                            if (counts[2] == "you")
                                            {
                                                minion = (Minion)player1.MyCardsOnBord[count2];
                                                if (target.Feature == "heal")
                                                    minion.Health += target.Points;
                                                if (target.Feature == "damage")
                                                    minion.Health -= target.Points;
                                                if (minion.Health > 0)
                                                    player1.MyCardsOnBord[count2] = minion;
                                                else
                                                    player1.MyCardsOnBord.RemoveAt(count2);
                                            }
                                            else
                                            {
                                                minion = (Minion)player2.MyCardsOnBord[count2];
                                                if (target.Feature == "heal")
                                                    minion.Health += target.Points;
                                                if (target.Feature == "damage")
                                                    minion.Health -= target.Points;
                                                if (minion.Health > 0)
                                                    player2.MyCardsOnBord[count2] = minion;
                                                else
                                                    player2.MyCardsOnBord.RemoveAt(count2);
                                            }
                                            mes1 = "Spell1";
                                            mes2 = "Spell2";
                                            foreach (Card c in player1.MyCardsOnBord)
                                            {
                                                mes1 += ';';
                                                if (c is Minion)
                                                    mes1 += JsonConvert.SerializeObject((Minion)c);
                                                else
                                                    mes1 += JsonConvert.SerializeObject((Spell)c);

                                            }
                                            mes1 += ";next";

                                            foreach (Card c in player2.MyCardsOnBord)
                                            {
                                                mes1 += ';';
                                                if (c is Minion)
                                                    mes1 += JsonConvert.SerializeObject((Minion)c);
                                                else
                                                    mes1 += JsonConvert.SerializeObject((Spell)c);

                                            }

                                            foreach (Card c in player2.MyCardsOnBord)
                                            {
                                                mes2 += ';';
                                                if (c is Minion)
                                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                                else
                                                    mes2 += JsonConvert.SerializeObject((Spell)c);

                                            }
                                            mes2 += ";next";
                                            foreach (Card c in player1.MyCardsOnBord)
                                            {
                                                mes2 += ';';
                                                if (c is Minion)
                                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                                else
                                                    mes2 += JsonConvert.SerializeObject((Spell)c);

                                            }
                                            player1.CardsInMyHand.RemoveAt(count2);
                                            mes1 += ";hand";
                                            foreach (Card c in player1.CardsInMyHand)
                                            {
                                                mes1 += ';';
                                                if (c is Minion)
                                                    mes1 += JsonConvert.SerializeObject((Minion)c);
                                                if (c is MassSpell mas)
                                                    mes1 += JsonConvert.SerializeObject(mas);
                                                if (c is TargetSpell t)
                                                    mes1 += JsonConvert.SerializeObject(t);
                                            }
                                            mes2 += ";hand";
                                            foreach (Card c in player1.CardsInMyHand)
                                            {
                                                mes2 += ';';
                                                if (c is Minion)
                                                    mes2 += JsonConvert.SerializeObject((Minion)c);
                                                if (c is MassSpell mas)
                                                    mes2 += JsonConvert.SerializeObject(mas);
                                                if (c is TargetSpell t)
                                                    mes2 += JsonConvert.SerializeObject(t);
                                            }
                                            
                                            mes2 += ';' + curMana.ToString(); mes2 += ';' + player1.MyDeck.Count.ToString();
                                            mes1 += ';' + curMana.ToString(); ; mes1 += ';' + player1.MyDeck.Count.ToString();
                                            msg = System.Text.Encoding.ASCII.GetBytes(mes1);
                                            stream1.Write(msg, 0, msg.Length);
                                            msg = System.Text.Encoding.ASCII.GetBytes(mes2);
                                            stream2.Write(msg, 0, msg.Length);
                                        }
                                    }
                                    else
                                    {
                                        Minion m1 = (Minion)player1.MyCardsOnBord[count1];
                                        Minion m2 = (Minion)player2.MyCardsOnBord[count2];
                                        if (m1.CanAttack)
                                        {
                                            if (player2.HaveTaunt == 0)
                                            {
                                                m1.Health -= m2.Damage;
                                                m2.Health -= m1.Damage;
                                                if (m1.Health > 0)
                                                    player1.MyCardsOnBord[count1] = m1;
                                                else
                                                    player1.MyCardsOnBord.RemoveAt(count1);
                                                if (m2.Health > 0)
                                                    player2.MyCardsOnBord[count2] = m2;
                                                else
                                                    player2.MyCardsOnBord.RemoveAt(count2);
                                                m1.CanAttack = false;
                                                mes1 = "Attac";
                                                mes2 = "Attac";

                                                foreach (Card c in player1.MyCardsOnBord)
                                                {
                                                    mes1 += ';';
                                                    if (c is Minion)
                                                        mes1 += JsonConvert.SerializeObject((Minion)c);
                                                    else
                                                        mes1 += JsonConvert.SerializeObject((Spell)c);

                                                }
                                                mes1 += ";next";

                                                foreach (Card c in player2.MyCardsOnBord)
                                                {
                                                    mes1 += ';';
                                                    if (c is Minion)
                                                        mes1 += JsonConvert.SerializeObject((Minion)c);
                                                    else
                                                        mes1 += JsonConvert.SerializeObject((Spell)c);

                                                }

                                                foreach (Card c in player2.MyCardsOnBord)
                                                {
                                                    mes2 += ';';
                                                    if (c is Minion)
                                                        mes2 += JsonConvert.SerializeObject((Minion)c);
                                                    else
                                                        mes2 += JsonConvert.SerializeObject((Spell)c);

                                                }
                                                mes2 += ";next";
                                                foreach (Card c in player1.MyCardsOnBord)
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
                                                if (m2.IsTaunt)
                                                {
                                                    m1.Health -= m2.Damage;
                                                    m2.Health -= m1.Damage;
                                                    if (m1.Health > 0)
                                                        player1.MyCardsOnBord[count1] = m1;
                                                    else
                                                        player1.MyCardsOnBord.RemoveAt(count1);
                                                    if (m2.Health > 0)
                                                        player2.MyCardsOnBord[count2] = m2;
                                                    else
                                                    {
                                                        player2.MyCardsOnBord.RemoveAt(count2);
                                                        player2.HaveTaunt--;
                                                    }
                                                    m1.CanAttack = false;
                                                    mes1 += "Attac";
                                                    mes2 += "Attac";

                                                    foreach (Card c in player1.MyCardsOnBord)
                                                    {
                                                        mes1 += ';';
                                                        if (c is Minion)
                                                            mes1 += JsonConvert.SerializeObject((Minion)c);
                                                        else
                                                            mes1 += JsonConvert.SerializeObject((Spell)c);

                                                    }
                                                    mes1 += ";next";

                                                    foreach (Card c in player2.MyCardsOnBord)
                                                    {
                                                        mes1 += ';';
                                                        if (c is Minion)
                                                            mes1 += JsonConvert.SerializeObject((Minion)c);
                                                        else
                                                            mes1 += JsonConvert.SerializeObject((Spell)c);

                                                    }

                                                    foreach (Card c in player2.MyCardsOnBord)
                                                    {
                                                        mes2 += ';';
                                                        if (c is Minion)
                                                            mes2 += JsonConvert.SerializeObject((Minion)c);
                                                        else
                                                            mes2 += JsonConvert.SerializeObject((Spell)c);

                                                    }
                                                    mes2 += ";next";
                                                    foreach (Card c in player1.MyCardsOnBord)
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
                                            }
                                        }
                                        else
                                        {
                                            msg = System.Text.Encoding.ASCII.GetBytes("Card can not attack.");
                                            stream1.Write(msg, 0, msg.Length);
                                        }
                                    }
                                }
                                else
                                {
                                    count1 = int.Parse(counts[0]);
                                    Minion m1 = (Minion)player1.MyCardsOnBord[count1];
                                    if (m1.CanAttack)
                                    {
                                        if (player2.HaveTaunt == 0)
                                        {
                                            player2.Health -= m1.Damage;

                                            m1.CanAttack = false;
                                            mes1 = "AttacPlayer;";
                                            mes2 = "AttacPlayer;";

                                            mes1 += player1.Health;
                                            mes1 += ";";
                                            mes1 += player2.Health;

                                            mes2 += player2.Health;
                                            mes2 += ";";
                                            mes2 += player1.Health;

                                            if (player1.Health <= 0)
                                            {
                                                msg = System.Text.Encoding.ASCII.GetBytes("Player2Win");
                                                stream1.Write(msg, 0, msg.Length);
                                                stream2.Write(msg, 0, msg.Length);
                                                i = stream1.Read(bytes, 0, bytes.Length);
                                                client1.Close();
                                                client2.Close();
                                                return;
                                            }
                                            if (player2.Health <= 0)
                                            {
                                                msg = System.Text.Encoding.ASCII.GetBytes("Player1Win");
                                                stream1.Write(msg, 0, msg.Length);
                                                stream2.Write(msg, 0, msg.Length);
                                                i = stream1.Read(bytes, 0, bytes.Length);
                                                client1.Close();
                                                client2.Close();
                                                return;
                                            }
                                            if (player1.Health > 0 && player2.Health > 0)
                                            {
                                                msg = System.Text.Encoding.ASCII.GetBytes(mes1);
                                                stream1.Write(msg, 0, msg.Length);
                                                msg = System.Text.Encoding.ASCII.GetBytes(mes2);
                                                stream2.Write(msg, 0, msg.Length);
                                            }
                                        }
                                        else
                                        {
                                            msg = System.Text.Encoding.ASCII.GetBytes("Card can not attack.");
                                            stream1.Write(msg, 0, msg.Length);
                                        }
                                    }
                                    else
                                    {
                                        msg = System.Text.Encoding.ASCII.GetBytes("Card can not attack.");
                                        stream1.Write(msg, 0, msg.Length);
                                    }
                                }
                                
                                Step(stream1, stream2, player1, player2, curMana);
                                return;
                            }
                            catch
                            {
                                try
                                {
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("You win");
                                    stream1.Write(msg, 0, msg.Length);
                                    msg = System.Text.Encoding.ASCII.GetBytes("You lose");
                                    stream2.Write(msg, 0, msg.Length);
                                }
                                catch
                                {

                                }
                                try
                                {
                                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("You win");
                                    stream2.Write(msg, 0, msg.Length);
                                    msg = System.Text.Encoding.ASCII.GetBytes("You lose");
                                    stream1.Write(msg, 0, msg.Length);
                                }
                                catch
                                {

                                }
                            }
                            finally
                            {
                                client1.Close();
                                client2.Close();
                                stream1.Close();
                                stream2.Close();

                            }
                        }
                    }
                }
            }
            catch(ObjectDisposedException)
            {
                try
                {
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("You win");
                    stream1.Write(msg, 0, msg.Length);
                    msg = System.Text.Encoding.ASCII.GetBytes("You lose");
                    stream2.Write(msg, 0, msg.Length);
                }
                catch
                {

                }
                try
                {
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("You win");
                    stream2.Write(msg, 0, msg.Length);
                    msg = System.Text.Encoding.ASCII.GetBytes("You lose");
                    stream1.Write(msg, 0, msg.Length);
                }
                catch
                {

                }
            }
        }
        public void AOEDamage(ref Player player, int damage)
        {
            foreach (Card c in player.MyCardsOnBord)
            {
                if (c is Minion minion)
                {
                    minion.Health -= damage;
                }
            }
        }
    }
}
