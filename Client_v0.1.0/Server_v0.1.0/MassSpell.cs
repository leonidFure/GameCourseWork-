using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_v0._1._0
{
    
    class MassSpell : Spell
    {
        int drow;

        public MassSpell(string name, int cost, string feature,int drow) : base(name, cost, feature)
        {
            this.drow = drow;
        }

        public int Drow { get => drow; set => drow = value; }
    }
}
