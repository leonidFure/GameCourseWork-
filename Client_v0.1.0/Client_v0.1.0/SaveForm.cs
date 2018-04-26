﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client_v0._1._0
{
    public partial class SaveForm : Form
    {
        
        public SaveForm()
        {
            InitializeComponent();
            
        }
        private int desiredStartLocationX;
        private int desiredStartLocationY;
        private List<Card> MyDeck;
        public SaveForm(int x, int y, List<Card> MyDeck, string name=null): this()
        {
            this.desiredStartLocationX = x;
            this.desiredStartLocationY = y;
            this.MyDeck = MyDeck;
            if(name!=null)
                textBox1.Text = name;
            Load += new EventHandler(SaveForm_Load);
        }

        private void SaveForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.SetDesktopLocation(desiredStartLocationX, desiredStartLocationY);
        }

        private void bCansel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(@"Decks\" + textBox1.Text + ".txt"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    foreach (Minion a in MyDeck)
                    {
                        serializer.Serialize(writer, a);
                        sw.WriteLine();
                    }
                }
                this.Close();
            }
            
        }
    }
}
