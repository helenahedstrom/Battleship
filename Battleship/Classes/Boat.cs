using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Classes
{
    public class Boat
    {
        public List<Coordinate> Coordinates { get; set; }

        public string Name { get; set; }

        public string StatusCodeIsHit { get; set; }

        public string StatusCodeIsSunk { get; set; }

    }
}
