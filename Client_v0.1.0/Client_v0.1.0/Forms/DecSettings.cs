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
            int count = lBAllCard.SelectedIndex;
            if (a != "")
            {
                lBYourDeck.Items.Add(a);
                MyDeck.Add(AllCards[count]);
                lMyDeck.Text = "Cards: " + MyDeck.Count;
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

            AllCards.Add(new Minion("Earthworm_Jim",7,8,7,false,false));
            AllCards.Add(new Minion("Sonic", 3, 4, 2,false,true));
            AllCards.Add(new Minion("Scorpion", 5, 5, 5,false,false));
            AllCards.Add(new Minion("RoboСop", 6,7, 6,false,false));
            AllCards.Add(new Minion("Obelix", 8, 12, 4,true,false));
            AllCards.Add(new Minion("Red_Octopus", 2, 3, 2,false,false));
            AllCards.Add(new Minion("Michael_Jackson", 4, 4, 5,true,false));
            AllCards.Add(new Minion("Knuckles", 4, 5, 4,false,true));
            AllCards.Add(new Minion("Power_Ranger", 6, 8,5,false,false));
            AllCards.Add(new Minion("Bugs_Bunny", 3, 4, 3,false,false));
            AllCards.Add(new Minion("Aladdin", 5, 3, 6,false,true));
            AllCards.Add(new Minion("Vectorman", 1, 2, 1,true,false));
            AllCards.Add(new Minion("Ecco_the_Dolphin", 2, 4, 1,true,false));
            AllCards.Add(new Minion("Altered_Beast", 5, 8, 2,false,false));
            AllCards.Add(new Minion("Chip", 1, 1, 2,false,false));
            AllCards.Add(new Minion("Wily", 1, 3, 1,false,false));

            AllCards.Add(new TargetSpell("Fireball", 4, "damage",3));
            AllCards.Add(new TargetSpell("Slash", 2, "damage",2));
            AllCards.Add(new TargetSpell("Puk", 1, "damage",1));
            AllCards.Add(new TargetSpell("Pushka", 10, "damage",12));
            AllCards.Add(new TargetSpell("Healing", 3, "heal",3));
            AllCards.Add(new TargetSpell("Beam", 2, "heal",4));
            AllCards.Add(new TargetSpell("Sun", 1, "heal",2));

            AllCards.Add(new MassSpell("Flamestrike", 3, "damage"));

            foreach (Card c in AllCards)
            {
                if (c is Minion)
                {
                    Minion a = (Minion)c;
                    lBAllCard.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                }
                if (c is MassSpell massSpell)
                {
                    lBAllCard.Items.Add(massSpell.Name + " (Cost:" + massSpell.Cost + ", Feat:" + massSpell.Feature + ")");
                }
                if (c is TargetSpell targetSpell)
                {
                    lBAllCard.Items.Add(targetSpell.Name + " (Cost:" + targetSpell.Cost + ", Feat:" + targetSpell.Feature + ", Points:" + targetSpell.Points + ")");
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
                    if (l[2] == 'P')
                        MyDeck.Add(JsonConvert.DeserializeObject<TargetSpell>(l));
                    if (l[2] == 'F')
                        MyDeck.Add(JsonConvert.DeserializeObject<TargetSpell>(l));
                }
                foreach (Card c in MyDeck)
                {
                    if (c is Minion)
                    {
                        Minion a = (Minion)c;
                        lBYourDeck.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")"+ " MINION");
                    }
                    if (c is MassSpell massSpell)
                    {
                        lBYourDeck.Items.Add(massSpell.Name + " (Cost:" + massSpell.Cost + ", Feat:" + massSpell.Feature + ")");
                    }
                    if (c is TargetSpell targetSpell)
                    {
                        lBYourDeck.Items.Add(targetSpell.Name + " (Cost:" + targetSpell.Cost + ", Feat:" + targetSpell.Feature+ ", Points:" +targetSpell.Points +")");
                    }
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
                    lMyDeck.Text = "Cards: " + MyDeck.Count;
                }
            }
        }
    }
}
