using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Client_v0._1._0
{
    public partial class SaveForm : Form
    {
        private int desiredStartLocationX;
        private int desiredStartLocationY;
        private List<Card> MyDeck;

        public SaveForm()
        {
            InitializeComponent();
        }
        
        public SaveForm(int x, int y, List<Card> MyDeck, string name=null): this()
        {
            desiredStartLocationX = x;
            desiredStartLocationY = y;
            this.MyDeck = MyDeck;
            if(name!=null)
                textBox1.Text = name;
            Load += new EventHandler(SaveForm_Load);
        }

        private void SaveForm_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            SetDesktopLocation(desiredStartLocationX, desiredStartLocationY);
        }

        private void bCansel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(@"Decks\" + textBox1.Text + ".txt"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    foreach (Card a in MyDeck)
                    {
                        if (a is Minion)
                            serializer.Serialize(writer, (Minion)a);
                        else
                            serializer.Serialize(writer, (MassSpell)a);
                        sw.Write(';');
                    }
                }
                Close();
            }
        }
    }
}
