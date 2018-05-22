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
        bool isTaunt;
        bool isCharge;

        public int Health { get => health; set => health = value; }
        public int Damage { get => damage; set => damage = value; }
        public bool CanAttack { get => canAttack; set => canAttack = value; }
        public bool IsTaunt { get => isTaunt; set => isTaunt = value; }
        public bool IsCharge { get => isCharge; set => isCharge = value; }

        public Minion(string name, int cost, int health, int damage, bool isTaunt, bool isCharge, bool canAttack = false) : base(name, cost)
        {
            this.health = health;
            this.damage = damage;
            this.canAttack = canAttack;
            this.isTaunt = isTaunt;
            this.isCharge = isCharge;
        }
    }
}
