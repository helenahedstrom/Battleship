using Battleship.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class ShipGenerator
    {
        public Player GenerateShips()
        {
            var player = new Player()
            {
                Boats = new List<Boat>()
              {
               new Boat()
            {
                Name = "Destroyer",
                StatusCodeIsHit = "243",
                StatusCodeIsSunk = "253",

                Coordinates = new List<Coordinate>()
                {
                    new Coordinate{Name= "A5"},
                    new Coordinate {Name = "A6"},
                     new Coordinate {Name = "A7"},
                }
               }
            //},

            //new Boat()
            //{
            //    Name = "Battleship",
            //    StatusCodeIsHit = "242",
            //    StatusCodeIsSunk = "252",

            //    Coordinates = new List<Coordinate>()
            //    {
            //        new Coordinate{Name= "B1"},
            //        new Coordinate {Name = "C1"},
            //         new Coordinate {Name = "D1"},
            //         new Coordinate {Name = "E1"}
            //    }
            //},

            //new Boat()
            //{
            //    Name = "Carrier",
            //    StatusCodeIsHit = "241",
            //    StatusCodeIsSunk = "251",

            //    Coordinates = new List<Coordinate>()
            //    {
            //        new Coordinate{Name= "H4"},
            //        new Coordinate {Name = "H5"},
            //         new Coordinate {Name = "H6"},
            //         new Coordinate {Name = "H7"},
            //         new Coordinate {Name = "H8"}
            //    }
            //},

            //new Boat()
            //{
            //    Name = "Patrol boat",
            //    StatusCodeIsHit = "245",
            //    StatusCodeIsSunk = "255",

            //    Coordinates = new List<Coordinate>()
            //    {
            //        new Coordinate{Name= "F8"},
            //        new Coordinate {Name = "F9"}
            //    }
            //},

            //new Boat()
            //{
            //    Name = "Submarine",
            //    StatusCodeIsHit = "244",
            //    StatusCodeIsSunk = "254",

            //    Coordinates = new List<Coordinate>()
            //    {
            //        new Coordinate{Name= "B9"},
            //        new Coordinate {Name = "C9"},
            //         new Coordinate {Name = "D9"}
            //    }
            //}
        }
            };

            return player;

           
        }
    }
}
