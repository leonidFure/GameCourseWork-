﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    class MassSpell: Spell
    {
        public int heal;
        public string Feature { set; get; }
        public int Points { set; get; }
        public MassSpell(string name, int cost, string feature, int points): base(name, cost)
        {
            Feature = feature;
            Points = points;
        }
    }
}
