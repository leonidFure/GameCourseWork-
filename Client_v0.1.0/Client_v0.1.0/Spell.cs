using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    public class Spell : Card
    {
        int magicDamage;

        public int MagicDamage { get => magicDamage; set => magicDamage = value; }

        public Spell(string name, int cost, int magicDamage) : base(name, cost)
        {
            this.magicDamage = magicDamage;
        }

        public override bool IsMinion()
        {
            return base.IsMinion();
        }
    }
}
