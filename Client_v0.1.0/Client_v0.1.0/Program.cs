using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Client_v0._1._0
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new MainMenu());
            //Application.Run(new Gameform());
            //Application.Run(new DecSettings());
        }
        
    }
}
