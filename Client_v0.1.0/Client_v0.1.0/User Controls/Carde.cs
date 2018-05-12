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
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont1;
        Font myFont2;
        public Carde()
        {
            InitializeComponent();
            
            byte[] fontData = Fonts._8_BIT_WONDER;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;

            fonts.AddMemoryFont(fontPtr, Fonts._8_BIT_WONDER.Length);
            AddFontMemResourceEx(fontPtr, (uint)Fonts._8_BIT_WONDER.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont1 = new Font(fonts.Families[0], 7.0F);
            myFont2 = new Font(fonts.Families[0], 12.0F);
            lName.Font = myFont1;
            lDamage.Font = myFont2;
            lHealth.Font = myFont2;
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

        public Image image
        {
            set { pictureBox1.BackgroundImage = value; }
        }

        public string Namee
        {
            get { return lName.Text; }
            set { lName.Text = value; }
        }

        public int Index { get => index; set => index = value; }
        public int EnIndex { get => enIndex; set => enIndex = value; }

        private void Carde_Load(object sender, EventArgs e)
        {
            lDamage.BringToFront();
            lHealth.BringToFront();
        }
    }
}
