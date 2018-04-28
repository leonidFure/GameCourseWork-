using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Client_v0._1._0
{
    public partial class Carde : UserControl
    {
        
        public Carde()
        {
            
            InitializeComponent();
            
        }
        public int Health 
        { 
            get { return int.Parse(lHealth.Text); }
            set { lHealth.Text = value.ToString(); } 
        }
        public int Damage 
        { 
            get { return int.Parse(lDamage.Text); }
            set { lDamage.Text = value.ToString(); } 
        }
        public string Namee 
        {
            get { return lName.Text; }
            set { lName.Text = value; } 
        }
        
    }
}
