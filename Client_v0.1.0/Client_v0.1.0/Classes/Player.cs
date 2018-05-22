using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Client_v0._1._0
{
    class Player
    {
        int health;
        int energy;
        List<Card> myDeck = new List<Card>();
        List<Card> cardsInMyHand = new List<Card>();
        List<Card> myCardsOnBord = new List<Card>();

        public int Health { get => health; set => health = value; }
        public int Energy { get => energy; set => energy = value; }
        public List<Card> MyDeck { get => myDeck; set => myDeck = value; }
        public List<Card> CardsInMyHand { get => cardsInMyHand; set => cardsInMyHand = value; }
        public List<Card> MyCardsOnBord { get => myCardsOnBord; set => myCardsOnBord = value; }

        public Player(int health, int energy)
        {
            this.health = health;
            this.energy = energy;
        }
    }
}
