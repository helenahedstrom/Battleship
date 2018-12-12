using Battleship.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class Game
    {
        public bool CheckCoordinateOnBoard(string coordinates)
        {
            var array = coordinates.ToCharArray();
            if(array.Length > 2 || array.Length < 2)
            {
                return false;
            }
            var coordinateNumber = (int)Char.GetNumericValue(array[1]);

            if (array[0] >= 65 && array[0] <= 75 && coordinateNumber >= 1 && coordinateNumber <= 10)
            {
                return true;
            }

            return false;

        }

        public string ExecuteFireCommand(string line, Player player)
        {
            var splitCommand = line.Split(" ");
            bool hit = false;
      
            foreach (var boat in player.Boats)
            {
                foreach (var coordinate in boat.Coordinates)
                {
                    if (splitCommand[1] == coordinate.Name)
                    {
                        coordinate.IsHit = true;
                        hit = true;
                        return boat.StatusCodeIsHit + " You hit my " + boat.Name;
                    }

                }

            }
            if (!hit)
            {
                return "230 Miss!";
            }

            return "";
        }
    }
}
