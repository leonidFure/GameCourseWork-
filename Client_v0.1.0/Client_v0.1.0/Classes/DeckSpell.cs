using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    class DeckSpell: Spell
    {
        int draw;//кол-во карт
        string Feature { set; get; }
        public int Draw { get => draw; set =>draw = value; }
        public DeckSpell(string name, int cost, string feature,int draw) : base(name, cost)
        {
            Feature = feature;
            this.draw = draw;
        }
    }
}
