using Battleship.Classes;
using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            var success = false;
            var host = "";
            int port = 0;
            Player player;
            do
            {
                Console.Clear();
            Console.WriteLine("BattleShip 1.0");
            var shipGen = new ShipGenerator();
            player = shipGen.GenerateShips();
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

            host = Console.ReadLine();

            Console.WriteLine("Ange port: ");

            success = int.TryParse(Console.ReadLine(), out port);

            Console.WriteLine("Namn: ");

            player.Name = Console.ReadLine();

            
            } while (!success);

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
