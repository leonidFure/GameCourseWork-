using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Client_v0._1._0
{

    public partial class DecSettings : Form
    {
        int _cost = 0;
        int _health = 0;
        int _damage = 0;
        int ind = 0;
        List<Card> AllCards = new List<Card>();
        List<Card> MyDeck = new List<Card>();
        private MainMenu _m;

        public DecSettings(MainMenu m)
        {
            _m = m;
            InitializeComponent();
        }

        private void lBAllCard_MouseDown(object sender, MouseEventArgs e)
        {
            if (lBAllCard.Items.Count > 0)
            {
                ListBox list = (ListBox)sender;
                lBAllCard.DoDragDrop(list.Text, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        
        private void lBYourDeck_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void lBYourDeck_DragDrop(object sender, DragEventArgs e)
        {

            string a = e.Data.GetData(DataFormats.Text).ToString();
            if (a != "")
            {
                lBYourDeck.Items.Add(a);
                if (a[a.Length-1] == 'N')
                {
                    int count = 0;
                    do
                    {
                        if (AllCards[count].Name == a.Substring(0, a.LastIndexOf('H') - 2))
                        {
                            Minion m = (Minion)AllCards[count];
                            _cost = m.Cost;
                            _damage = m.Damage;
                            _health = m.Health;
                            MyDeck.Add(new Minion(AllCards[count].Name, _cost, _health, _damage));
                        }
                        count++;
                    } while (AllCards[count-1].Name != a.Substring(0, a.LastIndexOf('H') - 2)) ;
                }
                else
                {
                    int count = 0;
                    do
                    {
                        if (AllCards[count].Name == a.Substring(0, a.LastIndexOf('D')-2))
                        {
                            Spell m = (Spell)AllCards[count];
                            _cost = m.Cost;
                            _damage = m.MagicDamage;
                            MyDeck.Add(new Spell(AllCards[count].Name, _cost, _damage));
                        }
                        count++;
                    } while (AllCards[count-1].Name != a.Substring(0, a.LastIndexOf('D') - 2));
                    
                        
                    
                }
                if (lBYourDeck.SelectedIndex != -1)
                    if (lBYourDeck.Items[lBYourDeck.SelectedIndex] == a)
                        lBYourDeck.Items.RemoveAt(lBYourDeck.SelectedIndex);
            }
        }
        private void bBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            _m.Visible = true;
        }

        private void DecSettings_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            AllCards.Add(new Minion("Earthworm_Jim",7,7,8));
            AllCards.Add(new Minion("Sonic", 3, 2, 4));
            AllCards.Add(new Minion("Scorpion", 5, 5, 5));
            AllCards.Add(new Minion("RoboСop", 6, 6, 7));
            AllCards.Add(new Minion("Obelix", 8, 4, 12));
            AllCards.Add(new Minion("Red_Octopus", 2, 2, 3));
            AllCards.Add(new Minion("Michael_Jackson", 4, 5, 4));
            AllCards.Add(new Minion("Knuckles", 4, 4, 5));
            AllCards.Add(new Minion("Power_Ranger", 6, 5, 8));
            AllCards.Add(new Minion("Bugs_Bunny", 3, 3, 4));
            AllCards.Add(new Minion("Aladdin", 5, 6, 3));
            AllCards.Add(new Minion("Vectorman", 1, 1, 2));
            AllCards.Add(new Minion("Ecco_the_Dolphin", 2, 1, 4));
            AllCards.Add(new Minion("Altered_Beast", 5, 2, 8));
            AllCards.Add(new Minion("Chip", 1, 2, 1));
            AllCards.Add(new Minion("Wily", 1, 1, 3));
            


            foreach (Card c in AllCards)
            {
                if (c is Minion)
                {
                    Minion a = (Minion)c;
                    lBAllCard.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                }
                else
                {
                    Spell a = (Spell)c;
                    lBAllCard.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                }
            }
            string[] dirs = Directory.GetFiles(@"Decks");
            foreach (string dir in dirs)
            {
                cBDecks.Items.Add(dir.Substring(dir.LastIndexOf((char)92)+1, dir.LastIndexOf('.')- dir.LastIndexOf((char)92)-1));
            }
        }

        private void bSaveDeck_Click(object sender, EventArgs e)
        {
            if (MyDeck.Count > 0)
            {
                if (cBDecks.SelectedIndex != -1)
                {
                    SaveForm frm2 = new SaveForm(MousePosition.X - 100, MousePosition.Y - 50, MyDeck, cBDecks.Items[cBDecks.SelectedIndex].ToString());
                    frm2.ShowDialog();
                }
                else
                {
                    SaveForm frm2 = new SaveForm(MousePosition.X - 100, MousePosition.Y - 50, MyDeck);
                    frm2.ShowDialog();
                }
                MyDeck.Clear();
                lBYourDeck.Items.Clear();
                
                string[] dirs = Directory.GetFiles(@"Decks");
                cBDecks.Items.Clear();
                foreach (string dir in dirs)
                {
                    cBDecks.Items.Add(dir.Substring(dir.LastIndexOf((char)92) + 1, dir.LastIndexOf('.') - dir.LastIndexOf((char)92) - 1));
                }
                cBDecks.SelectedIndex = -1;
                cBDecks.Text = null;
            }
        }

        private void cBDecks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] lines;
            string line;
            lBYourDeck.Items.Clear();
            MyDeck.Clear();
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader file = new StreamReader("Decks" + (char)92 + cBDecks.SelectedItem.ToString() + ".txt"))
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
                foreach (Card c in MyDeck)
                {
                    if (c is Minion)
                    {
                        Minion a = (Minion)c;
                        lBYourDeck.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")"+ " MINION");
                    }
                    else
                    {
                        Spell a = (Spell)c;
                        lBYourDeck.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")"+ " SPELL");
                    }
                }
            }
        }

        private void lBYourDeck_Click(object sender, EventArgs e)
        {
            if (lBYourDeck.SelectedIndex != -1)
            {
                if (MyDeck[lBYourDeck.SelectedIndex] is Minion)
                {
                    Minion minion = (Minion)MyDeck[lBYourDeck.SelectedIndex];
                    carde1.Health = minion.Health;
                    carde1.Namee = minion.Name;
                    carde1.Damage = minion.Damage;
                    carde1.image = (Image)Picture.ResourceManager.GetObject(minion.Name);
                }
                else
                {

                }
            }
             
        }

        private void lBYourDeck_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int y = e.Y / lBYourDeck.ItemHeight;
                if (y < lBYourDeck.Items.Count)
                    lBYourDeck.SelectedIndex = y;
                if (lBYourDeck.SelectedIndex != -1)
                {
                    MyDeck.RemoveAt(lBYourDeck.SelectedIndex);
                    lBYourDeck.Items.RemoveAt(lBYourDeck.SelectedIndex);
                }
            }
        }
    }
}
