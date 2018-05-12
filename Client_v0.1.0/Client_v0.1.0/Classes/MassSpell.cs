using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    class MassSpell: Spell
    {
        string Feature { set; get; }
        int Points { set; get; }
        public MassSpell(string name, int cost, string feature, int points) : base(name, cost)
        {
            Feature = feature;
            Points = points;
        }
    }
}
