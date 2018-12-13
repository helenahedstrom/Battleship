using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Classes
{
    public class Player
    {
        public List<Boat> Boats { get; set; }

        public string Name { get; set; }

        public string Opponent { get; set; }
    }
}
