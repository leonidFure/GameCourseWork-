using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    public class Spell : Card
    {
        int magicdamage;
        public Spell(string name, int cost, int magicdamage): base(name,cost) 
        {
            this.magicdamage = magicdamage;
        }
        public int MagicDamage
        {
            get { return magicdamage; }
            set { magicdamage = value; }
        }
    }
}
