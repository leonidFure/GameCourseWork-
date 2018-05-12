using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Client_v0._1._0
{
    public partial class UserPlayer : UserControl
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont1;
        public UserPlayer()
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
            labelEnergy.Font = myFont1;
            labelHealth.Font = myFont1;
        }
        public int Health
        {
            get { return int.Parse(labelHealth.Text); }
            set { labelHealth.Text = value.ToString(); }
        }
        
        public int Energy
        {
            get { return int.Parse(labelEnergy.Text); }
            set { labelEnergy.Text = value.ToString(); }
        }

        public Image HeroImage
        {
            set { pictureBox1.BackgroundImage = value; }
        }
    }
}
