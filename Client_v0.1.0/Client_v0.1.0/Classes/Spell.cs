using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    public class Spell : Card
    {
        string feature;
        public string Feature { get => feature; set => feature = value; }

        public Spell(string name, int cost, string feature) : base(name, cost)
        {
            this.Feature = feature;
        }

    }
}
