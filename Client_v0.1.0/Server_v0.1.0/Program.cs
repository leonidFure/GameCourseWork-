using System;
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
        static Int32 port = 103;
        static void Main(string[] args)
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
    }
    public class Game
    {
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
                            int a = rndCard.Next(20 - i);
                            int b = rndCard.Next(20 - i);
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
                        mes1 += deckPlayer1.Count.ToString();
                        mes2 += deckPlayer2.Count.ToString();
                        msg = System.Text.Encoding.ASCII.GetBytes(mes1);
                        stream1.Write(msg, 0, msg.Length);
                        msg = System.Text.Encoding.ASCII.GetBytes(mes2);
                        stream2.Write(msg, 0, msg.Length);
                        Step(stream1, stream2, deckPlayer1InHand, deckPlayer2InHand,deckPlayer1OnBord,deckPlayer2OnBord);
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
        public void Step(NetworkStream stream1, NetworkStream stream2, List<Card> cards1, List<Card> cards2, List<Card> cardsbord1, List<Card> cardsbord2)
        {
            int count1;
            int i;
            Byte[] bytes = new Byte[4096];
            while ((i = stream1.Read(bytes, 0, bytes.Length)) != 0)
            {
                String data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                if(int.TryParse(data, out count1))
                {
                    String mes = "";
                    if (cards1[count1] is Minion)
                        mes = JsonConvert.SerializeObject((Minion)cards1[count1]);
                    else
                        mes = JsonConvert.SerializeObject((Spell)cards1[count1]);
                    cardsbord1.Add(cards1[count1]);
                    //deckPlayer1InHand.RemoveAt(count1);
                    
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mes);
                    stream2.Write(msg, 0, msg.Length);
                    Step(stream2, stream1,cards2,cards1, cardsbord2, cardsbord1);
                }
                
            }
        }
    }
}
