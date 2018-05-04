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

        Font myFont;
        public Carde()
        {
            InitializeComponent();
            byte[] fontData = Fonts.PixelFont;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Fonts.PixelFont.Length);
            AddFontMemResourceEx(fontPtr, (uint)Fonts.PixelFont.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 9.0F);
            lName.Font = myFont;
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
