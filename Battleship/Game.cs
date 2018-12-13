using Battleship.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.SetCursorPosition(0, 2);
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            foreach (var boat in player.Boats)
            {
                Console.WriteLine("\n" + boat.Name + " ");
                foreach (var coordinate in boat.Coordinates)
                {
                    if (coordinate.IsHit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        //Console.WriteLine(splitCommand[1]);
                        Console.Write(coordinate.Name + " ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else if (splitCommand[1] == coordinate.Name)
                    {
                        coordinate.IsHit = true;
                        hit = true;

                        Console.ForegroundColor = ConsoleColor.Red;
                        //Console.WriteLine(splitCommand[1]);
                        Console.Write(coordinate.Name + " ");
                        // Restore previous position
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        var IsSunk = CheckIfSunk(boat);
                        var gameWon = CheckIfGameWon(player);
                        if (IsSunk)
                        {
                            if (gameWon)
                            {
                                return boat.StatusCodeIsHit + " You hit my " + boat.Name + "\n" + boat.StatusCodeIsSunk + " You Sunk my " + boat.Name + "\n260 " +  player.Opponent + " Win!";
                            }
                            return boat.StatusCodeIsHit + " You hit my " + boat.Name + "\n" + boat.StatusCodeIsSunk + " You Sunk my " + boat.Name;
                        }
                        return boat.StatusCodeIsHit + " You hit my " + boat.Name;
                    }
                    else
                    {
                        Console.Write(coordinate.Name + " ");
                    }
                  

                }

            }
            if (!hit)
            {
                Console.SetCursorPosition(x, y);
                return "230 Miss!";
             
            }            
            Console.SetCursorPosition(x, y);
            return "";
        }

        public bool CheckIfSunk(Boat boat)
        {
            var HitCoordinates = boat.Coordinates.Where(x => x.IsHit == true).ToList();

            if (boat.Coordinates.Count == HitCoordinates.Count)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfGameWon(Player player)
        {
            var unHitCoordinates = player.Boats.SelectMany(x => x.Coordinates).Where(y => y.IsHit == false).ToList();

            if (unHitCoordinates.Any())
            {
                return false;
            }
            return true;
        }


    }
}
