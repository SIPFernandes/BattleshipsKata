using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsKata
{
    public class Player
    {
        public Board? Board { get; set; }
        public string Name { get; }

        public Player(string name)
        {
            Name = name;
        }        
    }
}
