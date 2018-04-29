using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Client_v0._1._0
{
    class Player
    {
        int damage;
        int armor;
        int health;
        string name;

        List<Card> myDeck;
        List<Card> cardsInMyHand;
        List<Card> cardsInEnemyHand;
        List<Card> myCardsOnBord = new List<Card>();
        List<Card> enemyCardsOnBord;

        public Player(int damage, int health, int armor, string name,List<Card> myDeck) 
        {
            this.armor = armor;
            this.damage = damage;
            this.health = health;
            this.name = name;
            this.myDeck = myDeck;
        }

        public int Damage { get => damage; set => damage = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Health { get => health; set => health = value; }
        public List<Card> MyDeck { get => myDeck; set => myDeck = value; }
        public List<Card> CardsInMyHand { get => cardsInMyHand; set => cardsInMyHand = value; }
        public List<Card> CardsInEnemyHand { get => cardsInEnemyHand; set => cardsInEnemyHand = value; }
        public List<Card> MyCardsOnBord { get => myCardsOnBord; set => myCardsOnBord = value; }
        public List<Card> EnemyCardsOnBord { get => enemyCardsOnBord; set => enemyCardsOnBord = value; }
    }
}
