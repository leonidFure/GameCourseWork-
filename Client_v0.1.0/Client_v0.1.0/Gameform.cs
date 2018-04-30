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

namespace Client_v0._1._0
{
    public partial class Gameform : Form
    {
        Byte[] d = new Byte[4096];
        static Int32 port = 103;
        static TcpClient client = new TcpClient("127.0.0.1", port);
        NetworkStream stream = client.GetStream();
        String responseData = String.Empty;
        Player You;
        int cardX = 11;
        string path;
        public Gameform(string path)
        {
            InitializeComponent();
            this.path = path;
        }
        
        private void bStep_Click(object sender, EventArgs e)
        {
            //Carde c = new Carde();
            //c.Location = new Point(12, 12);
            //c.Size = new Size(114, 173);
            //c.Health = 13;
            //c.Damage = 13;
            //c.Namee = "ss";
            //YourPanel.Controls.Add(c);
            Thread clientThread = new Thread(new ThreadStart(SendMSG));
            clientThread.Start();
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
            Invoke(new MethodInvoker(delegate () {
                Carde carde = (Carde)sender;
                if (carde.BackColor == Color.Gray)
                {
                    foreach (Control c in YourPanel.Controls)
                        c.BackColor = Color.Gray;
                    carde.BackColor = Color.Green;
                }
                else
                    carde.BackColor = Color.Gray;
            }));
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
            if (You.MyCardsOnBord.Count <= 7)
            {
                string a;
                a = e.Data.GetData(DataFormats.Text).ToString();
                Carde c = new Carde();
                c.Location = new Point(cardX, 400);
                c.Size = new Size(114, 173);
                if (a[a.Length - 1] == 'N')
                {
                    int count = 0;
                    do
                    {
                        if (You.MyDeck[count].Name == a.Substring(0, a.LastIndexOf('H') - 2))
                        {
                            Minion m = (Minion)You.MyDeck[count];
                            c.Namee = m.Name;
                            c.Damage = m.Damage;
                            c.Health = m.Health;
                            You.MyCardsOnBord.Add(m);
                        }
                        count++;
                    } while (You.MyDeck[count - 1].Name != a.Substring(0, a.LastIndexOf('H') - 2));
                }
                YourPanel.Controls.Add(c);
                c.Click += new System.EventHandler(this.MouseClickNew);
                cardX += 125;
            }
        }
         
        private void Gameform_Load(object sender, EventArgs e)
        {
            Thread clientThread = new Thread(new ThreadStart(Connect));
            clientThread.Start();
            this.ControlBox = false;
            List<Card> MyDeck = new List<Card>();
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Connect()
        {
            try
            {
                Int32 bytes = stream.Read(d, 0, d.Length);
                responseData = System.Text.Encoding.ASCII.GetString(d, 0, bytes);
                this.Invoke((MethodInvoker)delegate ()
                {
                    this.lHeroHeath.Text = "Health: " + responseData;
                });
                string message;
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader file = new StreamReader("Decks" + (char)92 + "Deck#4" + ".txt"))
                {
                    message = file.ReadLine();
                }
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                
                stream.Write(data, 0, data.Length);
                
                responseData = String.Empty;
                bytes = stream.Read(d, 0, d.Length);
                responseData = System.Text.Encoding.ASCII.GetString(d, 0, bytes);
                List<Card> MyDeck =new List<Card>();
                responseData = responseData.Substring(0, responseData.Length - 1);
                string[] lines = responseData.Split(';');
                foreach (string l in lines)
                {
                    if (l[2] == 'H')
                        MyDeck.Add(JsonConvert.DeserializeObject<Minion>(l));
                    else
                        MyDeck.Add(JsonConvert.DeserializeObject<Spell>(l));
                }
                Player You = new Player(0, 30, 0, "Maxurik", MyDeck);
                this.Invoke((MethodInvoker)delegate ()
                {
                    foreach (Card c in You.MyDeck)
                    {
                        if (c.IsMinion())
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
            catch (ArgumentNullException e)
            {
            }
            catch (SocketException e)
            {
            }
        }
        public void SendMSG()
        {
            try
            {
                Int32 bytes;
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Hello");
                stream.Write(data, 0, data.Length);
            }
            catch (ArgumentNullException e)
            {
            }
            catch (SocketException e)
            {
            }
        }
    }
}
