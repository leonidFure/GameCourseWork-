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
        bool canAttack;
        public int Health { get => health; set => health = value; }
        public int Damage { get => damage; set => damage = value; }
        public bool CanAttack { get => canAttack; set => canAttack = value; }

        public Minion(string name, int cost, int health, int damage, bool canAttack = false) : base(name, cost)
        {
            this.health = health;
            this.damage = damage;
            this.canAttack = canAttack;
        }
    }
}
