using Battleship.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Battleship
{
    public class BattleshipClient
    {
        public void ConnectingToServer(string host, int port, Player player)
        {
            var game = new Game();
            var counter = 0;
            using (var client = new TcpClient(host, port))
            using (var networkStream = client.GetStream())
            using (StreamReader reader = new StreamReader(networkStream, Encoding.UTF8))
            using (var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true })
            {
                Console.WriteLine($"Ansluten till {client.Client.RemoteEndPoint}");
                while (client.Connected)
                {
                    if (counter == 0)
                    {
                        var line = reader.ReadLine();
                        if (!line.Contains("210"))
                        {
                            writer.WriteLine("501 Sequence Error");
                            Console.WriteLine("501 Sequence Error");
                            client.Dispose();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Svar: {line}");
                            counter = 1;
                        }
                    }

                    var data = networkStream.DataAvailable;
                    if (!data)
                    {
                        Console.WriteLine("Ange text att skicka: (Skriv QUIT för att avsluta)");
                        var text = Console.ReadLine();

                        // Skicka text
                        writer.WriteLine(text);
                    }




                    if (!client.Connected) break;

                    // Läs minst en rad
                    do
                    {
                        var line = reader.ReadLine();
                        if (line.Contains("270"))
                        {
                            client.Dispose();
                            break;
                        }
                        if (line.Contains("222"))
                        {
                            Console.WriteLine(line);
                            line = reader.ReadLine();
                            Console.WriteLine(line);                            
                        }
                        if (line.Contains("Fire", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var answer = "";
                            var commands = line.Split(" ");
                            var IsOnBoard = game.CheckCoordinateOnBoard(commands[1]);
                            if (IsOnBoard)
                            {
                            answer = game.ExecuteFireCommand(line, player);
                            }
                            else
                            {
                                Console.WriteLine("500 Syntax Error");
                                writer.WriteLine("500 Syntax Error");
                            }
           
                            writer.WriteLine(answer);
                            Console.WriteLine(answer);
                        }
                        Console.WriteLine($"Svar: {line}");

                    } while (networkStream.DataAvailable);

                };
            }
            }
        }
}
