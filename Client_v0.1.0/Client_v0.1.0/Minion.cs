using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    class Minion : Card
    {
        int health;
        int damage;
        public Minion( string name,int cost,int health, int damage): base(name,cost)
        {
            this.health = health;
            this.damage = damage;
        }
        public int Health 
        {
            get { return health; }
            set { health = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        
    }
}
