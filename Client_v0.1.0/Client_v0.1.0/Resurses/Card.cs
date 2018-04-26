using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    public abstract class Card
    {
        protected string name;
        protected int cost;
        public Card(string name, int cost) 
        {
            this.name = name;
            this.cost = cost;
        }
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public virtual bool IsMinion() 
        {
            return false;
        }
    }
}
