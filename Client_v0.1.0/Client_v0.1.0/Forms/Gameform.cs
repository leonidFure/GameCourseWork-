using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Client_v0._1._0
{
    public partial class Gameform : Form
    {
        
        string sAttac = "";//Строка для отправки серверу сообщения об атаке
        bool bSelectedCard = false;//Переменная, показывающая была ли выбрана карта игрока, которой он будет атакавать
        bool sStep = false;//Переменная, показывающая чей сейчас ход
        int cardX = 11, cardX2 = 11;//Координаты карт по X относительно панели
        string path, hero;//Строки получаемые при инициализации формы с путем к файлу с колодой и названием персонажа
        Controller controller;
        public UserPlayer userPlayer1;
        public UserPlayer userPlayer2;

        public Gameform(string path, string hero)
        {
            InitializeComponent();
            this.path = path;
            this.hero = hero;
            controller = new Controller(path, hero,this);
        }
        /// <summary>
        /// Сообщает контролеру об завершении хода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bStep_Click(object sender, EventArgs e)
        {
            Thread clientThread = new Thread(new ParameterizedThreadStart(controller.SendMSG));
            clientThread.Start("End step");
        }

        private void lBCrads1_MouseDown(object sender, MouseEventArgs e)
        {
            if (lBCrads1.Items.Count > 0)
            {
                ListBox list = (ListBox)sender;
                lBCrads1.DoDragDrop(list.Text, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        /// <summary>
        /// Выбор атакующей карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseClickNew(object sender, EventArgs e)
        {
            if (sStep == true)
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    Carde carde = (Carde)sender;
                    if (carde.BackColor == Color.Gray)
                    {
                        foreach (Control c in YourPanel.Controls)
                        {
                            if (c is Carde)
                            {
                                c.BackColor = Color.Gray;
                                c.BackgroundImage = Picture.Card;
                            }
                        }
                        carde.BackColor = Color.Green;
                        carde.BackgroundImage = Picture.SelectedCard;
                        sAttac = carde.Index.ToString() + ";";
                        bSelectedCard = true;
                    }
                    else
                    {
                        foreach (Control c in YourPanel.Controls)
                        {
                            if (c is Carde)
                            {
                                c.BackColor = Color.Gray;
                                c.BackgroundImage = Picture.Card;
                            }
                        }
                        bSelectedCard = false;

                    }
                }));
            }
        }
        /// <summary>
        /// Выбор атакуемой карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseEnemyClickNew(object sender, EventArgs e)
        {
            if (bSelectedCard)
            {
                if (sStep == true)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Carde carde = (Carde)sender;
                        foreach (Control c in YourPanel.Controls)
                        {
                            if (c is Carde)
                            {
                                c.BackColor = Color.Gray;
                                c.BackgroundImage = Picture.Card;
                            }
                        }
                        bSelectedCard = false;
                        sAttac += carde.EnIndex.ToString();


                    }));

                    Thread clientThread = new Thread(new ParameterizedThreadStart(controller.SendMSG));
                    clientThread.Start(sAttac);
                    sAttac = "";
                }
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
            if (sStep)
            {
                    string a;
                    a = e.Data.GetData(DataFormats.Text).ToString();
                    int count = lBCrads1.SelectedIndex;
                    Thread clientThread = new Thread(new ParameterizedThreadStart(controller.SendMSG));
                    clientThread.Start(count);
                //SoundPlayer sp2 = new SoundPlayer(@"DropOnBoard");
                //sp2.Load();
                //sp2.Play();
                //sp2.Stop();

            }
        }

        private void Gameform_Load(object sender, EventArgs e)
        {
            Thread clientThread = new Thread(new ThreadStart(controller.Connect));
            clientThread.Start();
            this.ControlBox = false;
            List<Card> MyDeck = new List<Card>();
            bStep.Enabled = false;

            userPlayer1 = new UserPlayer
            {
                Location = new Point(393, 510),
                Size = new Size(100, 100)
            };
            YourPanel.Controls.Add(userPlayer1);

            userPlayer2 = new UserPlayer
            {
                Location = new Point(393, 0),
                Size = new Size(100, 100)
            };
            YourPanel.Controls.Add(userPlayer2);
            userPlayer2.Click += new System.EventHandler(this.userPlayer2Click);
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Thread clientThread = new Thread(new ParameterizedThreadStart(controller.SendMSG));
            clientThread.Start("End game");
        }

        /// <summary>
        /// Сообщает контролеру об атаке картой персонажа соперника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userPlayer2Click(object sender, EventArgs e)
        {
            if (bSelectedCard)
            {
                if (sStep == true)
                {
                    Invoke(new MethodInvoker(delegate () { sAttac += "Player"; }));
                    bSelectedCard = false;
                    
                    Thread clientThread = new Thread(new ParameterizedThreadStart(controller.SendMSG));
                    clientThread.Start(sAttac);
                    sAttac = "";
                }
            }
        }

        private void YourPanel_Click(object sender, EventArgs e)
        {
            bSelectedCard = false;
            foreach (Control c in YourPanel.Controls)
                c.BackColor = Color.Gray;
        }

        /// <summary>
        /// Метод, вызываемый для изменения списка карт 
        /// находящихся в руке одного из игроков
        /// </summary>
        /// <param name="cards">Изменённый список карт</param>
        /// <param name="player">Строка, обозначающая колода кокого играка подается в метод</param>
        /// <param name="energy">Количество енергии игрока</param>
        /// <param name="countCards">Количество карт оставшихся в колоде</param>
        public void ChangeHandDeck(List<Card> cards, string player,int energy, int countCards)
        {
            if (player == "You")
            {
                lBCrads1.Items.Clear();
                foreach (Card c in cards)
                {
                    if (c is Minion)
                    {
                        Minion a = (Minion)c;
                        lBCrads1.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                    }
                    else
                    {
                        //Spell a = (Spell)c;
                        //lBCards2.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                    }
                }
                lOffCard1.Text = "Cards: " + countCards.ToString();
                userPlayer1.Energy = energy;
            }
            else
            {
                lBCards2.Items.Clear();
                foreach (Card c in cards)
                {
                    if (c is Minion)
                    {
                        Minion a = (Minion)c;
                        lBCards2.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                    }
                    else
                    {
                        //Spell a = (Spell)c;
                        //lBCards2.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                    }
                }
                lOffCard2.Text = "Cards: " + countCards.ToString();
                userPlayer2.Energy = energy;
            }
        }

        /// <summary>
        /// Метод, вызываемый для изменения списка карт
        /// находящихся в руке обоих игроков
        /// </summary>
        /// <param name="cards1">Изменённый список карт первого игрока</param>
        /// <param name="cards2">Изменённый список карт второго игрока</param>
        /// <param name="CountCards1">Количество карт в колоде игроков</param>
        public void ChangeHandDeck(List<Card> cards1, List<Card> cards2, string CountCards1)
        {
            foreach (Card c in cards1)
            {
                if (c is Minion)
                {
                    Minion a = (Minion)c;
                    lBCrads1.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                }
                else
                {
                    //Spell a = (Spell)c;
                    //lBCrads1.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                }
            }
            foreach (Card c in cards2)
            {
                if (c is Minion)
                {
                    Minion a = (Minion)c;
                    lBCards2.Items.Add(a.Name + " (HP:" + a.Health + ", DMG:" + a.Damage + ", Cost:" + a.Cost + ")" + " MINION");
                }
                else
                {
                    //Spell a = (Spell)c;
                    //lBCards2.Items.Add(a.Name + " (DMG:" + a.MagicDamage + ", Cost:" + a.Cost + ")" + " SPELL");
                }
            }
            lOffCard1.Text = "Cards: " + CountCards1;
            lOffCard2.Text = "Cards: " + CountCards1;
        }

        /// <summary>
        /// Метод добавления карты на стол
        /// </summary>
        /// <param name="card">Карта, которую необходимо добавить</param>
        /// <param name="index">Индекс карты</param>
        /// <param name="energy">Текущая енергия игрока</param>
        /// <param name="player">Строка, обозначающая карта кокого играка подается в метод</param>
        public void AddCardOnBord(Card card, int index,int energy, string player)
        {
            Minion m = (Minion)card;
            if (player == "You")
            {
                Carde c = new Carde
                {
                    Location = new Point(cardX, 330),
                    Size = new Size(114, 163),
                    Namee = m.Name,
                    Damage = m.Damage,
                    Health = m.Health,
                    image = (Image)Picture.ResourceManager.GetObject(m.Name),
                    Index = index,
                    EnIndex = -1
                };

                YourPanel.Controls.Add(c);
                c.Click += new System.EventHandler(this.MouseClickNew);
                cardX += 125;
                userPlayer1.Energy = energy;
            }
            else
            {
                Carde c = new Carde
                {
                    Location = new Point(cardX2, 117),
                    Namee = m.Name,
                    Damage = m.Damage,
                    Health = m.Health,
                    image = (Image)Picture.ResourceManager.GetObject(m.Name),
                    Index = -1,
                    EnIndex = index,
                    Tag = "Enemy"
                };
                cardX2 += 125;
                YourPanel.Controls.Add(c);
                c.Click += new System.EventHandler(this.MouseEnemyClickNew);
                userPlayer2.Energy = energy;
            }
        }

        /// <summary>
        /// Метод добавления карт на стол
        /// </summary>
        /// <param name="cards">Список карт, которые необходимо добавить на стол</param>
        /// <param name="bordNotEmpty">Переменная определяющая, присутсявуют ли карты на столе</param>
        /// <param name="player">Строка, обозначающая карты кокого играка подается в метод</param>
        public void AddCardsOnBord(List<Card> cards, bool bordNotEmpty,string player)
        {
            if (player == "You")
            {
                if (bordNotEmpty)
                {
                    cardX = 11;
                    for (int j = 0; j < cards.Count; j++)
                    {
                        Minion m = (Minion)cards[j];
                        Carde c = new Carde
                        {
                            Location = new Point(cardX, 330),
                            Namee = m.Name,
                            Damage = m.Damage,
                            Health = m.Health,
                            image = (Image)Picture.ResourceManager.GetObject(m.Name),
                            Index = j,
                            EnIndex = -1
                        };
                        YourPanel.Controls.Add(c);
                        c.Click += new System.EventHandler(this.MouseClickNew);
                        cardX += 125;
                    }
                }
                else
                    cardX = 11;
            }
            else
            {
                if (bordNotEmpty)
                {
                    cardX2 = 11;
                    for (int j = 0; j < cards.Count; j++)
                    {
                        Minion m = (Minion)cards[j];
                        Carde c = new Carde
                        {
                            Location = new Point(cardX2, 117),
                            Namee = m.Name,
                            Damage = m.Damage,
                            Health = m.Health,
                            image = (Image)Picture.ResourceManager.GetObject(m.Name),
                            Index = -1,
                            EnIndex = j,
                            Tag = "Enemy"
                        };
                        YourPanel.Controls.Add(c);
                        c.Click += new System.EventHandler(this.MouseEnemyClickNew);
                        cardX2 += 125;
                    }
                }
                else
                    cardX2 = 11;
            }
        }
        
        /// <summary>
        /// Метод завершающий игру
        /// </summary>
        /// <param name="msg">Сообщение, показывающее какой из игроков выграл</param>
        public void EndGame(string msg)
        {
            MessageBox.Show(msg, "End game", MessageBoxButtons.OK);
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        /// <summary>
        /// Метод передачи хода
        /// </summary>
        /// <param name="step">Переменная, показывающая чей сейчас ход</param>
        /// <param name="turn">Текст отображаемый на кнопке хода</param>
        public void ChangeTurn(bool step, string turn)
        {
            sStep = step;
            bStep.Enabled = step;
            bStep.Text = turn;
        }
        /// <summary>
        /// Метод добавления персонажей на игровое поле
        /// </summary>
        public void SetUserPlayers()
        {
            YourPanel.Controls.Clear();
            YourPanel.Controls.Add(userPlayer2);
            YourPanel.Controls.Add(userPlayer1);
        }
    }
}
