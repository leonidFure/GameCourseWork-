using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace Client_v0._1._0
{
    public partial class Carde : UserControl
    {
        Font myFont;
        PrivateFontCollection private_fonts = new PrivateFontCollection();
        public Carde()
        {
            InitializeComponent();
            LoadFont();
            lName.Font = new Font(private_fonts.Families[0], 22);
            lName.UseCompatibleTextRendering = true;
        }
        private void LoadFont()
        {

            using (MemoryStream fontStream = new MemoryStream(Fonts.PixelFont))
            {
                // create an unsafe memory block for the font data
                System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
                // create a buffer to read in to
                byte[] fontdata = new byte[fontStream.Length];
                // read the font data from the resource
                fontStream.Read(fontdata, 0, (int)fontStream.Length);
                // copy the bytes to the unsafe memory block
                Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);
                // pass the font to the font collection
                private_fonts.AddMemoryFont(data, (int)fontStream.Length);
                // close the resource stream
                fontStream.Close();
                // free the unsafe memory
                Marshal.FreeCoTaskMem(data);

            }

        }

        int index;
        int enIndex;
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

        public int Index { get => index; set => index = value; }
        public int EnIndex { get => enIndex; set => enIndex = value; }
    }
}
