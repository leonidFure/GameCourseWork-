using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void SaveForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void bCansel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
