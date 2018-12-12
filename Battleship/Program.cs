using Battleship.Classes;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("BattleShip 1.0");
            var shipGen = new ShipGenerator();
            var player = shipGen.GenerateShips();
            foreach (var boat in player.Boats)
            {
                Console.WriteLine("\n" + boat.Name + " ");

                foreach (var boatCoordinates in boat.Coordinates)
                {
                    Console.Write(boatCoordinates.Name + " ");
                }
            }

            Console.WriteLine("\n\nDina Försök(rött är miss, grönt är träff): \n");

            Console.WriteLine("\nAnge host: ");

            var host = Console.ReadLine();

            Console.WriteLine("Ange port: ");

            var port = int.Parse(Console.ReadLine());

            Console.WriteLine("Namn: ");

            player.Name = Console.ReadLine();

   
            //int x = Console.CursorLeft;
            //int y = Console.CursorTop;
            //Console.SetCursorPosition(0, 15);
            //Console.SetCursorPosition(0, Console.CursorTop - 1);
            //Console.WriteLine("a5");
            //// Restore previous position
            //Console.SetCursorPosition(x, y);

            if (string.IsNullOrEmpty(host))
            {
                var server = new BattleshipServer();
                server.StartListening(port, player);
            }
            else
            {
                var client = new BattleshipClient();
                client.ConnectingToServer(host, port, player);
            }

        }
    }
}
