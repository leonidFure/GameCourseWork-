﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_v0._1._0
{
    public class Player
    {
        int health;
        int energy;
        
        List<Card> myDeck = new List<Card>();
        List<Card> cardsInMyHand = new List<Card>();
        List<Card> myCardsOnBord = new List<Card>();
        public int HaveTaunt { get; set; }
        public int Health { get => health; set => health = value; }
        public int Energy { get => energy; set => energy = value; }
        public List<Card> MyDeck { get => myDeck; set => myDeck = value; }
        public List<Card> CardsInMyHand { get => cardsInMyHand; set => cardsInMyHand = value; }
        public List<Card> MyCardsOnBord { get => myCardsOnBord; set => myCardsOnBord = value; }

        public Player(int health, int energy)
        {
            HaveTaunt = 0;
            this.health = health;
            this.energy = energy;
        }
    }
}
