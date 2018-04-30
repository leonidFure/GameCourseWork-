using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server_v0._1._0
{
    class Minion : Card
    {
        int health;
        int damage;

        public int Health { get => health; set => health = value; }
        public int Damage { get => damage; set => damage = value; }

        public Minion( string name,int cost,int health, int damage): base(name,cost)
        {
            this.health = health;
            this.damage = damage;
        }

        public override bool IsMinion()
        {
            return true;
        }
    }
}
