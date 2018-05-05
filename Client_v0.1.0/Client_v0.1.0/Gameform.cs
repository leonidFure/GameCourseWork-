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
        static Int32 port = 103;
        static TcpClient client = new TcpClient("127.0.0.1", port);
        NetworkStream stream = client.GetStream();
        String responseData = String.Empty;
        Player You = new Player(0, 30, 0, "Maxurik");
        Player Enemy = new Player(0, 30, 0, "Lewa");
        int cardX = 11,cardX2=11;
        string path;
        public Gameform(string path)
        {
            InitializeComponent();
            this.path = path;
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
                            c.BackColor = Color.Gray;
                        carde.BackColor = Color.Green;
                        
                        sAttac = carde.Index.ToString()+";";
                    }
                    else
                    {
                        foreach (Control c in YourPanel.Controls)
                            c.BackColor = Color.Gray;
                        bSelectedCard = false;

                    }
                }));
                bSelectedCard = true;
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
                        if (carde.BackColor == Color.Gray)
                        {
                            foreach (Control c in YourPanel.Controls)
                                if (c.Tag == "Enemy")
                                    c.BackColor = Color.Gray;
                            carde.BackColor = Color.Red;
                            
                            sAttac += carde.EnIndex.ToString();
                        }
                        else
                            carde.BackColor = Color.Gray;
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
                    Carde c = new Carde();
                    c.Location = new Point(cardX, 400);
                    c.Size = new Size(114, 163);
                    if (a[a.Length - 1] == 'N')
                    {
                        int count = 0;
                        do
                        {
                            if (You.CardsInMyHand[count].Name == a.Substring(0, a.LastIndexOf('H') - 2))
                            {
                                Minion m = (Minion)You.CardsInMyHand[count];
                                c.Namee = m.Name;
                                c.Damage = m.Damage;
                                c.Health = m.Health;
                                c.image = (Image)Picture.ResourceManager.GetObject(m.Name);
                                c.Index = CountCarde;
                                c.EnIndex = -1;
                                CountCarde++;
                                You.MyCardsOnBord.Add(m);
                            }
                            count++;
                        } while (You.CardsInMyHand[count - 1].Name != a.Substring(0, a.LastIndexOf('H') - 2));
                        YourPanel.Controls.Add(c);
                        c.Click += new System.EventHandler(this.MouseClickNew);
                        cardX += 125;
                        Thread clientThread = new Thread(new ParameterizedThreadStart(DropCard));
                        clientThread.Start(count - 1);
                    }
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
            Application.Exit();
        }

        public void Connect()
        {
            try
            {
                int i;
                Int32 bytes = stream.Read(d, 0, d.Length);
                responseData = System.Text.Encoding.ASCII.GetString(d, 0, bytes);
                this.Invoke((MethodInvoker)delegate ()
                {
                    this.lHeroHeath.Text = "Health: " + responseData;
                });
                string message;
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader file = new StreamReader("Decks" + (char)92 + path + ".txt"))
                {
                    message = file.ReadLine();
                }
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                
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
                if (responseData[0] =='{' )
                {
                    if (responseData[2] == 'H')
                        Enemy.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Minion>(responseData));
                    else
                        Enemy.MyCardsOnBord.Add(JsonConvert.DeserializeObject<Spell>(responseData));
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        Carde c = new Carde();
                        c.Location = new Point(cardX2, 50);
                        c.Size = new Size(114, 163);
                        if (Enemy.MyCardsOnBord[Enemy.MyCardsOnBord.Count - 1] is Minion)
                        {
                            Minion m = (Minion)Enemy.MyCardsOnBord[Enemy.MyCardsOnBord.Count - 1];
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
                    });
                }
                else
                {
                    if(responseData=="Your step")
                    {
                        sStep = true;
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            bStep.Enabled = true;
                        });
                    }
                        
                    if (responseData == "DYour step")
                    {
                        sStep = false;
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            bStep.Enabled = false;
                        });
                    }
                    if (responseData[0] == 'A')
                    {
                        int n=0;
                        responseData = responseData.Substring(0, responseData.Length - 1);
                        string[] lines = responseData.Split(';');
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
                                    Carde c = new Carde();
                                    c.Location = new Point(cardX, 400);
                                    c.Size = new Size(114, 163);
                                    c.Namee = m.Name;
                                    c.Damage = m.Damage;
                                    c.Health = m.Health;
                                    c.image = (Image)Picture.ResourceManager.GetObject(m.Name);
                                    c.Index = j;
                                    c.EnIndex = -1;
                                    YourPanel.Controls.Add(c);
                                    c.Click += new System.EventHandler(this.MouseClickNew);
                                    cardX += 125;
                                }
                                CountCarde = You.MyCardsOnBord.Count;
                            });

                        }
                        else
                        { CountCarde = 0; cardX = 11; }
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
                                    Carde c = new Carde();
                                    c.Location = new Point(cardX2, 50);
                                    c.Size = new Size(114, 163);
                                    c.Namee = m.Name;
                                    c.Damage = m.Damage;
                                    c.Health = m.Health;
                                    c.image = (Image)Picture.ResourceManager.GetObject(m.Name);
                                    c.Index = -1;
                                    c.EnIndex = j;
                                    c.Tag = "Enemy";
                                    YourPanel.Controls.Add(c);
                                    c.Click += new System.EventHandler(this.MouseEnemyClickNew);
                                    cardX2 += 125;
                                }

                                CountEnemyCarde = Enemy.MyCardsOnBord.Count;

                            });
                        }
                        else { CountEnemyCarde = 0; cardX2 = 11; }
                    }
                    if (responseData == "Card can not attack")
                    {
                        this.Invoke((MethodInvoker)delegate () { lExeption.Text = responseData; });
                    }
                }
            }
            Step();
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
