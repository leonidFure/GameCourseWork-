using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server_v0._1._0
{
    class TargetSpell: Spell
    {
        int points;
        public int Points { get => points; set => points = value; }
        public TargetSpell(string name, int cost, string feature, int points) : base(name, cost, feature)
        {
            this.points = points;
        }
    }
}
