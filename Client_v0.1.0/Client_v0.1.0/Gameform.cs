using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Client_v0._1._0
{
    public partial class Gameform : Form
    {
        int CountCarde = 0;
        int CountEnemyCarde = 0;
        string sAttac = "";
        bool bSelectedCard = false;
        bool sStep =false ;
        Byte[] d = new Byte[4096];
        static Int32 port = 22222;
        static TcpClient client = new TcpClient("127.0.0.1", port);
        NetworkStream stream = client.GetStream();
        String responseData = String.Empty;
        Player You = new Player(0, 30, 0, "Maxurik");
        Player Enemy = new Player(0, 30, 0, "Lewa");
        int cardX = 11,cardX2=11;
        string path, hero;
        public Gameform(string path, string hero)
        {
            InitializeComponent();
            this.path = path;
            this.hero = hero;
        }
        
        private void bStep_Click(object sender, EventArgs e)
        {
            
            Byte[] data = System.Text.Encoding.ASCII.GetBytes("End step");
            stream.Write(data, 0, data.Length);


        }

        private void lBCrads1_MouseDown(object sender, MouseEventArgs e)
        {
            if (lBCrads1.Items.Count > 0)
            {
                ListBox list = (ListBox)sender;
                lBCrads1.DoDragDrop(list.Text, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        public void MouseClickNew(object sender, EventArgs e)
        {
            if (sStep == true)
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    Carde carde = (Carde)sender;
                    if (carde.BackColor == Color.Gray)
                    {
                        foreach (Control c in YourPanel.Controls)
                        {
                            if (c is Carde)
                            {
                                c.BackColor = Color.Gray;
                                c.BackgroundImage = Picture.Card;
                            }
                        }  
                        carde.BackColor = Color.Green;
                        carde.BackgroundImage = Picture.SelectedCard;
                        sAttac = carde.Index.ToString()+";";
                        bSelectedCard = true;
                    }
                    else
                    {
                        foreach (Control c in YourPanel.Controls)
                        {
                            if (c is Carde)
                            {
                                c.BackColor = Color.Gray;
                                c.BackgroundImage = Picture.Card;
                            }
                        }
                        bSelectedCard = false;

                    }
                }));
                
            }
        }

        public void MouseEnemyClickNew(object sender, EventArgs e)
        {
            if (bSelectedCard)
            {
                if (sStep == true)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Carde carde = (Carde)sender;
                        foreach (Control c in YourPanel.Controls)
                        {
                            c.BackColor = Color.Gray;
                            c.BackgroundImage = Picture.Card;
                        }
                        bSelectedCard = false;
                        sAttac += carde.EnIndex.ToString();


                    }));
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(sAttac);
                    sAttac = "";
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        private void YourPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        
        private void YourPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (sStep)
            {
                if (You.MyCardsOnBord.Count <= 7)
                {
                    string a;
                    a = e.Data.GetData(DataFormats.Text).ToString();
                    int count = lBCrads1.SelectedIndex;
                    Thread clientThread = new Thread(new ParameterizedThreadStart(DropCard));
                    clientThread.Start(count);
                }
            }
        }
        
        private void Gameform_Load(object sender, EventArgs e)
        {
            Thread clientThread = new Thread(new ThreadStart(Connect));
            clientThread.Start();
            this.ControlBox = false;
            List<Card> MyDeck = new List<Card>();
            bStep.Enabled = false;
            
        }
        

        private void bExit_Click(object sender, EventArgs e)
        {
            stream.Close();
            client.Close();
            Application.Exit();
        }

        public void Connect()
        {
            try
            {

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(hero);

                stream.Write(data, 0, data.Length);
                int i;
                Int32 bytes = stream.Read(d, 0, d.Length);
                responseData = System.Text.Encoding.ASCII.GetString(d, 0, bytes);
                string[] vs = responseData.Split(';');
                //
                //здесь добавить отображение хп противника
                //
                this.Invoke((MethodInvoker)delegate ()
                {
                    this.userPlayer1.Health =int.Parse(vs[0]);
                    this.userPlayer1.HeroImage = (Image)Picture.ResourceManager.GetObject(vs[1]);
                    this.userPlayer2.HeroImage = (Image)Picture.ResourceManager.GetObject(vs[2]);
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
                while ((i = stream.Read(d, 0, d.Length)) != 0)
                {
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

                    this.Invoke((MethodInvoker)delegate ()
                    {
                        foreach (Card c in You.CardsInMyHand)
                        {
                            if (c is Minion)
                            {
                                Minion a = (Minion)c;
                                lBCrads1.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                            }
                            else
                            {
                                Spell a = (Spell)c;
                                lBCrads1.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                            }
                        }
                        foreach (Card c in Enemy.CardsInMyHand)
                        {
                            if (c is Minion)
                            {
                                Minion a = (Minion)c;
                                lBCards2.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                            }
                            else
                            {
                                Spell a = (Spell)c;
                                lBCards2.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                            }
                        }
                        lOffCard1.Text = "Cards: " + lines[14];
                        lOffCard2.Text = "Cards: " + lines[14];
                        
                    });
                    if (lines[15] == "Your step")
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            bStep.Enabled = true;
                        });
                        sStep = true;
                    }
                    
                    Step();
                }
                
            }
            catch (ArgumentNullException e)
            {
            }
            catch (SocketException e)
            {
            }
        }

        public void Step()
        {
            int i;
            while ((i = stream.Read(d, 0, d.Length)) != 0)
            {
                responseData = System.Text.Encoding.ASCII.GetString(d, 0, i);
                string[] lines = responseData.Split(';');
                if (responseData[0] =='{' )
                {
                    // добавить изменение енергии после сбрасывания карты на стол(изменения также должны быть на сервере)
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
                    this.Invoke((MethodInvoker)delegate ()
                    {
                    Carde c = new Carde
                    {
                        Location = new Point(cardX2, 117)
                    };
                    if (Enemy.MyCardsOnBord[Enemy.MyCardsOnBord.Count - 1] is Minion m)
                    {
                        c.Namee = m.Name;
                        c.Damage = m.Damage;
                        c.Health = m.Health;
                        c.image = (Image)Picture.ResourceManager.GetObject(m.Name);
                        c.Index = -1;
                        c.EnIndex = Enemy.MyCardsOnBord.Count - 1;
                        c.Tag = "Enemy";
                        YourPanel.Controls.Add(c);
                        c.Click += new System.EventHandler(this.MouseEnemyClickNew);
                        cardX2 += 125;
                    }
                    userPlayer2.Energy = int.Parse(lines[lines.Length - 1]);
                    });
                    Enemy.CardsInMyHand.Clear();
                    for (int i1 = 1; i1 < lines.Length-1; i1++)
                    {
                        if (lines[i1][2] == 'H')
                            Enemy.CardsInMyHand.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                        else
                            Enemy.CardsInMyHand.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                    }
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        lBCards2.Items.Clear();
                        foreach (Card c in Enemy.CardsInMyHand)
                        {
                            if (c is Minion)
                            {
                                Minion a = (Minion)c;
                                lBCards2.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                            }
                            else
                            {
                                Spell a = (Spell)c;
                                lBCards2.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                            }
                        }
                    });
                }

                if (lines[0] == "Your step")
                {
                    sStep = true;
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        bStep.Enabled = true;
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
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            lBCrads1.Items.Clear();
                            foreach (Card c in You.CardsInMyHand)
                            {
                                if (c is Minion)
                                {
                                    Minion a = (Minion)c;
                                    lBCrads1.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                                }
                                else
                                {
                                    Spell a = (Spell)c;
                                    lBCrads1.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                                }
                            }
                            int countCards = lines.Length - next - 2;
                            lOffCard1.Text = "Cards: " + countCards.ToString();
                            userPlayer1.Energy = int.Parse(lines[lines.Length - 1]);
                        });
                    }
                }

                if (lines[0] == "DYour step")
                {
                    sStep = false;
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        bStep.Enabled = false;
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
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            lBCards2.Items.Clear();
                            foreach (Card c in Enemy.CardsInMyHand)
                            {
                                if (c is Minion)
                                {
                                    Minion a = (Minion)c;
                                    lBCards2.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                                }
                                else
                                {
                                    Spell a = (Spell)c;
                                    lBCards2.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                                }
                            }
                            int countCards = lines.Length - next - 2;
                            lOffCard2.Text = "Cards: " + countCards.ToString();
                            userPlayer2.Energy = int.Parse(lines[lines.Length - 1]);
                        });
                    }
                }

                if (responseData[0] == 'A')
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
                    this.Invoke((MethodInvoker)delegate () 
                    {
                        YourPanel.Controls.Clear();
                            
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
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            cardX = 11;
                            for (int j = 0; j < You.MyCardsOnBord.Count; j++)
                            {
                                Minion m = (Minion)You.MyCardsOnBord[j];
                                Carde c = new Carde
                                {
                                    Location = new Point(cardX, 330),
                                    Namee = m.Name,
                                    Damage = m.Damage,
                                    Health = m.Health,
                                    image = (Image)Picture.ResourceManager.GetObject(m.Name),
                                    Index = j,
                                    EnIndex = -1
                                };
                                YourPanel.Controls.Add(c);
                                c.Click += new System.EventHandler(this.MouseClickNew);
                                cardX += 125;
                            }
                            CountCarde = You.MyCardsOnBord.Count;
                        });

                    }
                    else { CountCarde = 0; cardX = 11; }

                    if (n != lines.Length - 1)
                    {
                        for (int i1 = n + 1; i1 < lines.Length; i1++)
                        {
                            if (lines[i1][2] == 'H')
                                Enemy.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                            else
                                Enemy.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                        }
                        
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            cardX2 = 11;
                            for (int j = 0; j < Enemy.MyCardsOnBord.Count; j++)
                            {
                                Minion m = (Minion)Enemy.MyCardsOnBord[j];
                                Carde c = new Carde
                                {
                                    Location = new Point(cardX2, 117),
                                    Namee = m.Name,
                                    Damage = m.Damage,
                                    Health = m.Health,
                                    image = (Image)Picture.ResourceManager.GetObject(m.Name),
                                    Index = -1,
                                    EnIndex = j,
                                    Tag = "Enemy"
                                };
                                YourPanel.Controls.Add(c);
                                c.Click += new System.EventHandler(this.MouseEnemyClickNew);
                                cardX2 += 125;
                            }
                            CountEnemyCarde = Enemy.MyCardsOnBord.Count;
                        });
                    }
                    else { CountEnemyCarde = 0; cardX2 = 11; }
                }

                if (responseData == "Card can not attack.")
                {
                    this.Invoke((MethodInvoker)delegate () { MessageBox.Show(responseData); });
                }

                if (responseData == "Not enough energy.")
                {
                    this.Invoke((MethodInvoker)delegate () { MessageBox.Show(responseData); });
                }
                if (responseData == "Bord is full.")
                {
                    this.Invoke((MethodInvoker)delegate () { MessageBox.Show(responseData); });
                }

                if (lines[0][0] == 'H')
                {
                    You.CardsInMyHand.Clear();
                    for (int i1 = 2; i1 < lines.Length-1; i1++)
                    {
                        if (lines[i1][2] == 'H')
                            You.CardsInMyHand.Add(JsonConvert.DeserializeObject<Minion>(lines[i1]));
                        else
                            You.CardsInMyHand.Add(JsonConvert.DeserializeObject<Spell>(lines[i1]));
                    }
                    You.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Minion>(lines[1]));
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        
                        if (You.MyCardsOnBord[You.MyCardsOnBord.Count-1] is Minion m)
                        {
                            Carde c = new Carde
                            {
                                Location = new Point(cardX, 330),
                                Size = new Size(114, 163),
                                Namee = m.Name,
                                Damage = m.Damage,
                                Health = m.Health,
                                image = (Image)Picture.ResourceManager.GetObject(m.Name),
                                Index = CountCarde,
                                EnIndex = -1
                            };
                            CountCarde++;
                            YourPanel.Controls.Add(c);
                            c.Click += new System.EventHandler(this.MouseClickNew);
                            cardX += 125;
                            userPlayer1.Energy = int.Parse(lines[lines.Length - 1]);
                        }
                        lBCrads1.Items.Clear();
                        foreach (Card c in You.CardsInMyHand)
                        {
                            if (c is Minion)
                            {
                                Minion a = (Minion)c;
                                lBCrads1.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                            }
                            else
                            {
                                Spell a = (Spell)c;
                                lBCrads1.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                            }
                        }
                    });
                }
            }
            Step();
        }

        private void userPlayer2_Click(object sender, EventArgs e)
        {
            if (bSelectedCard)
            {
                if (sStep == true)
                {
                    Invoke(new MethodInvoker(delegate () {sAttac += "Player";} ));
                    bSelectedCard = false;
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(sAttac);
                    sAttac = "";
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        private void YourPanel_Click(object sender, EventArgs e)
        {
            bSelectedCard = false;
            foreach (Control c in YourPanel.Controls)
                c.BackColor = Color.Gray;
        }

        public void DropCard(object count)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(count.ToString());
            stream.Write(data, 0, data.Length);
        }
    }
}
