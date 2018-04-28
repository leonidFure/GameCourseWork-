using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace Client_v0._1._0
{
    public partial class Gameform : Form
    {
        Player You = new Player(0, 30, 0, "Maxurik", null);

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
            VZVPanel.Controls.Add(c);
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
            string a;
            a = e.Data.GetData(DataFormats.Text).ToString();
            Point p = MousePosition;
            Carde c = new Carde();
            p =YourPanel.PointToClient(new Point(e.X,e.Y-15));
            c.Location = p;
            c.Size = new Size(114, 173);
            c.Health = 13;
            c.Damage = 13;
            c.Namee = a;
            YourPanel.Controls.Add(c);
        }

        private void Gameform_Load(object sender, EventArgs e)
        {
            lBCrads1.Items.Add("Lewa privet");
        }
    }
}
