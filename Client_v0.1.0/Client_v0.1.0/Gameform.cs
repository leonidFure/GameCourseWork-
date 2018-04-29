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

namespace Client_v0._1._0
{
    public partial class Gameform : Form
    {
        Player You;
        int cardX = 11;
        public Gameform()
        {
            InitializeComponent();
        }
        
        private void bStep_Click(object sender, EventArgs e)
        {
            Carde c = new Carde();
            c.Location = new Point(12, 12);
            c.Size = new Size(114, 173);
            c.Health = 13;
            c.Damage = 13;
            c.Namee = "ss";
            YourPanel.Controls.Add(c);
        }

        private void lBCrads1_MouseDown(object sender, MouseEventArgs e)
        {
            if (lBCrads1.Items.Count > 0)
            {
                ListBox list = (ListBox)sender;
                lBCrads1.DoDragDrop(list.Text, DragDropEffects.Copy | DragDropEffects.Move);
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
                cardX += 125;
            }
        }
         
        private void Gameform_Load(object sender, EventArgs e)
        {
            List<Card> MyDeck = new List<Card>();
           // if(YourPanel.Controls[0].MouseClick+= MouseEventHandler.)
            string[] lines;
            string line;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader file = new StreamReader("Decks" + (char)92 + "Deck#3" + ".txt"))
            {
                line = file.ReadLine();
                line = line.Substring(0, line.Length - 1);
                lines = line.Split(';');
                foreach (string l in lines)
                {
                    if (l[2] == 'H')
                        MyDeck.Add(JsonConvert.DeserializeObject<Minion>(l));
                    else
                        MyDeck.Add(JsonConvert.DeserializeObject<Spell>(l));
                }
                You = new Player(0, 30, 0, "Maxurik",MyDeck);
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
            }

        }

        private void Gameform_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
