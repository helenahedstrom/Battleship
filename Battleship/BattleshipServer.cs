using Battleship.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Battleship
{
    public class BattleshipServer
    {
        TcpListener listener;

        public void StartListening(int port, Player player)
        {
            var game = new Game();
            var greeting = false;
            var startedGame = false;

            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                Console.WriteLine($"Starts listening on port: {port}");

                while (true)
                {
                    Console.WriteLine("Väntar på att någon ska ansluta sig...");

                    using (var client = listener.AcceptTcpClient())
                    using (var networkStream = client.GetStream())
                    using (StreamReader reader = new StreamReader(networkStream, Encoding.UTF8))
                    using (var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true })
                    {
                        Console.WriteLine($"Klient har anslutit sig {client.Client.RemoteEndPoint}!");
                        writer.WriteLine("210 BATTLESHIP/1.0");
                        Console.WriteLine("210 BATTLESHIP/1.0");


                        while (client.Connected)
                        {

                            var command = reader.ReadLine();
                            Console.WriteLine($"Mottaget: {command}");

                            if (!greeting && command.Contains("HELO", StringComparison.InvariantCultureIgnoreCase) || command.Contains("HELLO", StringComparison.InvariantCultureIgnoreCase))
                            {
                                var commands = command.Split(" ");
                                if (commands.Length == 1)
                                {
                                    writer.WriteLine("500 Syntax Error");
                                }
                                else
                                {
                                    var opponentName = commands[1];
                                    Console.WriteLine("220 " + player.Name);
                                    writer.WriteLine("220 " + player.Name);
                                    greeting = true;
                                }

                            }

                            else if (string.Equals(command, "QUIT", StringComparison.InvariantCultureIgnoreCase))
                            {
                                writer.WriteLine("270 BYE BYE");
                                break;
                            }

                            else if (greeting && !startedGame && string.Equals(command, "START", StringComparison.InvariantCultureIgnoreCase))
                            {
                                var rnd = new Random();
                                var startPlayer = rnd.Next(0, 2) == 0;
                                startPlayer = true;
                                if (startPlayer)
                                {
                                    writer.WriteLine("222 Host Starts");
                                    Console.WriteLine("222 Host Starts");
                                    var line = Console.ReadLine();
                                    writer.WriteLine(line);
                                    

                                }
                                else
                                {
                                    writer.WriteLine("221 Client Starts");
                                    Console.WriteLine("221 Client Starts");
                                }                             
                                startedGame = true;
                            }
                            

                            else if (greeting && startedGame)
                            {

                                if (string.Equals(command, "HELP", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    var helpText = "First command before game starts : HELO <SP> <playername> <CRLF> ";
                                    helpText += "Start game : START <CRLF> ";
                                    helpText += "Fire : FIRE <SP> <coordinate> [ <SP> <message> ] <CRLF> ";
                                    helpText += "Quit game : QUIT <CRLF>";
                                    writer.WriteLine(helpText);

                                    //break;
                                }

                                else if (string.Equals(command, "FIRE", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    var IsOnBoard = game.CheckCoordinateOnBoard(command);
                                    if (IsOnBoard)
                                    {
                                        var answer = game.ExecuteFireCommand(command, player);
                                    }
                                    else
                                    {
                                        Console.WriteLine("500 Syntax Error");
                                        writer.WriteLine("500 Syntax Error");
                                    }

                                        Console.WriteLine("Ange text att skicka: (Skriv QUIT för att avsluta)");
                                        var text = Console.ReadLine();

                                        // Skicka text
                                        writer.WriteLine(text);
                                    
                                }

                                else
                                {
                                    writer.WriteLine($"UNKNOWN COMMAND: {command}");
                                }

                            }

                            else if(command.Contains("501", StringComparison.InvariantCultureIgnoreCase))
                            {
                                break;
                            }

                            else
                            {
                                writer.WriteLine($"500 syntax error: {command}");
                            }

                        }
                    }

                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Misslyckades att öppna socket. Troligtvis upptagen.");
                Environment.Exit(1);
            }
        }
    }
}
