using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    class TargetSpell: Spell
    {
        int damage;
        int heal;
        string Feature { set; get; }
        int Points { set; get; }
        public int Damage { get => damage; set => damage = value; }
        public int Heal { get => heal; set => heal = value; }
        public TargetSpell(string name, int cost,string feature, int points,/*int heal,*/ int damage) : base(name, cost)
        {
            Feature = feature;
            Points = points;
            this.damage = damage;
            // this.heal = heal;
        }
    }
}
