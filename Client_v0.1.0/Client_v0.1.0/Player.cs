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
        List<Card> deck;
        public Player(int damage, int health, int armor, string name,List<Card> deck) 
        {
            this.armor = armor;
            this.damage = damage;
            this.health = health;
            this.name = name;
            this.deck = deck;
        }
    }
}
