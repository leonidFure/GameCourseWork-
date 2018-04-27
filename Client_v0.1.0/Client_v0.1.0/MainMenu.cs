using System;
using System.IO;
using System.Windows.Forms;

namespace Client_v0._1._0
{

    public partial class MainMenu : Form
    {
        DecSettings decSettings;
        public MainMenu()
        {
            InitializeComponent();
            decSettings = new DecSettings(this) { Visible = false };
            //создаем директорию для хранения колод
            //папка Decks создается в корневой папке проекта
            string path = @"Decks";
            try
            {
                if (Directory.Exists(path))
                    return;
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            finally { }
        }

        private void bDeckSettings_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            decSettings.Visible = true;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
