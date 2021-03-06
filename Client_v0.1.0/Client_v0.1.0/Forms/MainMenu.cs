﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Media;

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
       // SoundPlayer sp = new SoundPlayer(@"MainMuzika");
        private void MainMenu_Load(object sender, EventArgs e)
        {
           // sp.Load();
            //sp.Play();
            //sp.Stop();
            this.ControlBox = false;
            string[] dirs = Directory.GetFiles(@"Decks");
            foreach (string dir in dirs)
            {
                cBSetdecks.Items.Add(dir.Substring(dir.LastIndexOf((char)92) + 1, dir.LastIndexOf('.') - dir.LastIndexOf((char)92) - 1));
            }
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bPlay_Click(object sender, EventArgs e)
        {
            if (cBSetdecks.SelectedIndex!=-1)
            { 
                Gameform gf = new Gameform(cBSetdecks.Text,comboBoxHeros.Text);
                gf.Show();
                this.Hide();
            }
        }
    }
}
