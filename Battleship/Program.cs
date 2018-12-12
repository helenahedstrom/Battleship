using Battleship.Classes;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.CheckCoordinateOnBoard("A5g");
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

            Console.WriteLine("Försök");

            //foreach (var item in collection)
            //{

            //}
            
            Console.WriteLine("\nAnge host: ");

            var host = Console.ReadLine();

            Console.WriteLine("Ange port: ");

            var port = int.Parse(Console.ReadLine());

            Console.WriteLine("Namn: ");

            player.Name = Console.ReadLine();

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
