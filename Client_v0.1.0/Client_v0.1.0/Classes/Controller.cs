using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;


namespace Client_v0._1._0
{
    class Controller
    {
        public Controller(string path, string hero, Gameform gameform)
        {
            this.path = path;
            this.hero = hero;
            this.gameform = gameform;
        }
        int CountCarde = 0;
        int CountEnemyCarde = 0;
        string path, hero;
        Gameform gameform;
        Byte[] d = new Byte[4096];
        static Int32 port = 22222;
        static TcpClient client;
        
        NetworkStream stream;
        String responseData = String.Empty;
        Player You, Enemy;
        public void Connect()
        {
            
            try
            {
                client = new TcpClient();
                client.Connect("127.0.0.1", port);
                stream = client.GetStream();
                Int32 bytes = stream.Read(d, 0, d.Length);
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(hero);
                data = System.Text.Encoding.ASCII.GetBytes(hero);
                stream.Write(data, 0, data.Length);
                int i;
                bytes = stream.Read(d, 0, d.Length);
                responseData = System.Text.Encoding.ASCII.GetString(d, 0, bytes);
                string[] vs = responseData.Split(';');
                You = new Player(int.Parse(vs[0]),1);
                Enemy = new Player(int.Parse(vs[0]), 1);

                gameform.Invoke((MethodInvoker)delegate ()
                {
                    gameform.userPlayer1.Health = int.Parse(vs[0]);
                    gameform.userPlayer1.HeroImage = (Image)Picture.ResourceManager.GetObject(vs[1]);
                    gameform.userPlayer2.HeroImage = (Image)Picture.ResourceManager.GetObject(vs[2]);
                });
                string message;
                JsonSerializer serializer = new JsonSerializer();

                using (StreamReader file = new StreamReader("Decks" + (char)92 + path + ".txt"))
                {
                    message = file.ReadLine();
                }

                data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                responseData = String.Empty;
                i = stream.Read(d, 0, d.Length);
                responseData = System.Text.Encoding.ASCII.GetString(d, 0, i);
                string[] lines = responseData.Split(';');

                for (int i1 = 0; i1 < 7; i1++)
                {
                    if (lines[i1][2] == 'H')
                        You.CardsInMyHand.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                    else
                        You.CardsInMyHand.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                }

                for (int i1 = 7; i1 < 14; i1++)
                {
                    if (lines[i1][2] == 'H')
                        Enemy.CardsInMyHand.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                    else
                        Enemy.CardsInMyHand.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                }

                gameform.Invoke((MethodInvoker)delegate () { gameform.ChangeHandDeck(You.CardsInMyHand, Enemy.CardsInMyHand, lines[14]); });

                if (lines[15] == "Your step")
                {
                    gameform.Invoke((MethodInvoker)delegate ()
                    {
                        gameform.ChangeTurn(true, "Your Turn");
                    });
                }

                Step();

            }
            catch (ArgumentNullException e)
            {
            }
            catch (SocketException e)
            {
            }
            catch (StackOverflowException)
            {
                Console.WriteLine();
            }
            finally
            {
                client.Close();
                stream.Close();
                gameform.Invoke((MethodInvoker)delegate () { gameform.EndGame("sorry, server dead :("); });
            }
        }

        public void Step()
        {
            
                int i;
                while ((i = stream.Read(d, 0, d.Length)) != 0)
                {
                try
                {
                    responseData = System.Text.Encoding.ASCII.GetString(d, 0, i);
                    string[] lines = responseData.Split(';');
                    if (responseData[0] == '{')
                    {

                        if (lines[0][2] == 'H')
                        {
                            Minion minion = JsonConvert.DeserializeObject<Minion>(lines[0]);
                            Enemy.MyCardsOnBord.Add(minion);
                        }
                        else
                        {
                            Spell spell = JsonConvert.DeserializeObject<Spell>(lines[0]);
                            Enemy.MyCardsOnBord.Add(spell);
                        }
                        gameform.Invoke((MethodInvoker)delegate ()
                        {

                            if (Enemy.MyCardsOnBord[Enemy.MyCardsOnBord.Count - 1] is Minion m)
                            {

                                gameform.AddCardOnBord(m, Enemy.MyCardsOnBord.Count - 1, int.Parse(lines[lines.Length - 1]), "Enemy");

                            }

                        });
                        Enemy.CardsInMyHand.Clear();
                        for (int i1 = 1; i1 < lines.Length - 1; i1++)
                        {
                            if (lines[i1][2] == 'H')
                                Enemy.CardsInMyHand.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                            else
                                Enemy.CardsInMyHand.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                        }
                        gameform.Invoke((MethodInvoker)delegate () { gameform.ChangeHandDeck(Enemy.CardsInMyHand, "Enemy", Enemy.Energy, Enemy.MyDeck.Count); });
                    }

                    if (lines[0] == "Your step")
                    {

                        gameform.Invoke((MethodInvoker)delegate ()
                        {
                            gameform.ChangeTurn(true, "Your Turn");

                        });
                        if (lines.Length > 2)
                        {
                            int next = 0;
                            for (int i1 = 0; i1 < lines.Length; i1++)
                            {
                                if (lines[i1] == "next")
                                    next = i1;
                            }
                            You.CardsInMyHand.Clear();
                            for (int i1 = 1; i1 < next; i1++)
                            {
                                if (lines[i1][2] == 'H')
                                    You.CardsInMyHand.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                                else
                                    You.CardsInMyHand.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                            }
                            gameform.Invoke((MethodInvoker)delegate ()
                            {
                                int countCards = lines.Length - next - 2;
                                You.Energy = int.Parse(lines[lines.Length - 1]);
                                gameform.ChangeHandDeck(You.CardsInMyHand, "You", int.Parse(lines[lines.Length - 1]), countCards);
                            });
                        }
                    }

                    if (lines[0] == "DYour step")
                    {
                        gameform.Invoke((MethodInvoker)delegate ()
                        {
                            gameform.ChangeTurn(false, "Enemy Turn");

                        });
                        int next = 0;
                        if (lines.Length > 2)
                        {
                            for (int i1 = 0; i1 < lines.Length; i1++)
                            {
                                if (lines[i1] == "next")
                                    next = i1;
                            }
                            Enemy.CardsInMyHand.Clear();
                            for (int i1 = 1; i1 < next; i1++)
                            {
                                if (lines[i1][2] == 'H')
                                    Enemy.CardsInMyHand.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                                else
                                    Enemy.CardsInMyHand.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                            }
                            gameform.Invoke((MethodInvoker)delegate ()
                            {
                                int countCards = lines.Length - next - 2;
                                Enemy.Energy = int.Parse(lines[lines.Length - 1]);
                                gameform.ChangeHandDeck(Enemy.CardsInMyHand, "Enemy", int.Parse(lines[lines.Length - 1]), countCards);

                            });
                        }
                    }

                    if (lines[0] == "Attac")
                    {
                        int n = 0;
                        responseData = responseData.Substring(0, responseData.Length - 1);
                        You.MyCardsOnBord.Clear();
                        Enemy.MyCardsOnBord.Clear();
                        for (int i1 = 0; i1 < lines.Length; i1++)
                        {
                            if (lines[i1] == "next")
                                n = i1;
                        }
                        gameform.Invoke((MethodInvoker)delegate ()
                        {
                            gameform.SetUserPlayers();

                        });
                        if (n != 1)
                        {
                            for (int i1 = 1; i1 < n; i1++)
                            {
                                if (lines[i1][2] == 'H')
                                    You.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                                else
                                    You.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                            }
                            gameform.Invoke((MethodInvoker)delegate ()
                            {
                                gameform.AddCardsOnBord(You.MyCardsOnBord, true, "You");

                            });
                            CountCarde = You.MyCardsOnBord.Count;
                        }
                        else
                        {
                            CountCarde = 0;
                            gameform.Invoke((MethodInvoker)delegate ()
                            {
                                gameform.AddCardsOnBord(You.MyCardsOnBord, false, "You");
                            });
                        }

                        if (n != lines.Length - 1)
                        {
                            for (int i1 = n + 1; i1 < lines.Length; i1++)
                            {
                                if (lines[i1][2] == 'H')
                                    Enemy.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                                else
                                    Enemy.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                            }

                            gameform.Invoke((MethodInvoker)delegate ()
                            {
                                gameform.AddCardsOnBord(Enemy.MyCardsOnBord, true, "Enemy");
                                CountEnemyCarde = Enemy.MyCardsOnBord.Count;
                            });
                        }
                        else
                        {
                            CountEnemyCarde = 0; gameform.Invoke((MethodInvoker)delegate ()
                            {
                                gameform.AddCardsOnBord(Enemy.MyCardsOnBord, false, "Enemy");
                            });
                        }
                    }
                    if (lines[0] == "AttacPlayer")
                    {
                        You.Health = int.Parse(lines[1]);
                        Enemy.Health = int.Parse(lines[2]);
                        gameform.Invoke((MethodInvoker)delegate () { gameform.userPlayer1.Health = You.Health; gameform.userPlayer2.Health = Enemy.Health; });
                    }
                    if (responseData == "Player1Win")
                    {
                        client.Close();
                        stream.Close();
                        gameform.Invoke((MethodInvoker)delegate () { gameform.EndGame(responseData); });
                        return;
                    }
                    if (responseData == "Player2Win")
                    {
                        client.Close();
                        stream.Close();
                        gameform.Invoke((MethodInvoker)delegate () { gameform.EndGame(responseData); });
                        return;
                    }
                    if (responseData == "Card can not attack.")
                    {
                        gameform.Invoke((MethodInvoker)delegate () { MessageBox.Show(responseData); });
                    }

                    if (responseData == "Not enough energy.")
                    {
                        gameform.Invoke((MethodInvoker)delegate () { MessageBox.Show(responseData); });
                    }
                    if (responseData == "Bord is full.")
                    {
                        gameform.Invoke((MethodInvoker)delegate () { MessageBox.Show(responseData); });
                    }

                    if (lines[0][0] == 'H')
                    {
                        You.CardsInMyHand.Clear();
                        for (int i1 = 2; i1 < lines.Length - 1; i1++)
                        {
                            if (lines[i1][2] == 'H')
                                You.CardsInMyHand.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                            else
                                You.CardsInMyHand.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                        }
                        You.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Minion>(lines[1]));
                        gameform.Invoke((MethodInvoker)delegate ()
                        {

                            if (You.MyCardsOnBord[You.MyCardsOnBord.Count - 1] is Minion m)
                            {
                                gameform.AddCardOnBord(m, CountCarde, int.Parse(lines[lines.Length - 1]), "You");

                                You.Energy = int.Parse(lines[lines.Length - 1]);
                                CountCarde++;
                            }
                            gameform.ChangeHandDeck(You.CardsInMyHand, "You", You.Energy, You.MyDeck.Count);
                        });
                    }
                }
                catch (System.IO.IOException)
                {
                }
                catch (ArgumentNullException)
                {
                }
                catch (SocketException)
                {
                }
                catch (StackOverflowException)
                {
                    Console.WriteLine();
                }
                finally
                {
                    client.Close();
                    stream.Close();
                    gameform.Invoke((MethodInvoker)delegate () { gameform.EndGame("sorry, server dead :("); });
                }
            }
            Step();
            return;


        }

        public void SendMSG(object count)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(count.ToString());
            stream.Write(data, 0, data.Length);
        }
    }
}
