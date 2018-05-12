using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_v0._1._0
{
    public abstract class Card
    {
        
        string name;
        int cost;
        public string Name { get => name; set => name = value; }
        public int Cost { get => cost; set => cost = value; }
        

        public Card(string name, int cost) 
        {
            this.name = name;
            this.cost = cost;
        }
        
    }
}
