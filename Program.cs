using System;
using System.Collections;
using System.Collections.Generic;

namespace Greed
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Game zehntausend = new Game();
            Dice diceCup = new Dice();

            // Anzahl Spieler ermitteln
            Console.WriteLine("Welcome to Chicago");
            Console.WriteLine();
            Console.WriteLine("How many players do you want to create? ");
            int numberOfPlayers = zehntausend.GetNumberOfPlayers();

            Console.Clear();

            // Spielernamen vergeben
            Console.WriteLine("Name the players now...");
            Console.WriteLine();
            List<Player> players = zehntausend.NamePlayers(numberOfPlayers);

            Console.Clear();

            // Startspieler ermitteln
            Player player;
            player = zehntausend.DeterminePlayerToStartGame(players);
            int playerIndexInListOfPlayers = players.IndexOf(player);
            Console.WriteLine();
            Console.WriteLine("{0} starts...", player.Name);

            UserInteraction.AwaitKeyAndClearConsole();

            // Spiele Spiel bis einer der Spieler mehr als 10000 Punkte hat
            int points = 0;
            do
            {
                zehntausend.ShowPoints(players);
                // Würfelbecher mit sechs Würfeln füllen
                int[] dices = diceCup.FillDiceCupWithDices(6);
                ArrayList collectedDices = new ArrayList();
                Console.Write("Eyes are: ");
                foreach (var dice in dices)
                {
                    Console.Write("{0,3}", dice);
                }

                // Speichere Häufigkeit einer Augenzahl
                int[,] occurrenceOfEyes = zehntausend.FindOccurrenceOfEyes(dices);

                // Berechne Punkte
                Console.WriteLine();
                Console.WriteLine();
                points = zehntausend.GetPoints(occurrenceOfEyes);
                Console.WriteLine("You now have {0} points, {1}", points, player.Name);
                Console.WriteLine();

                if(points==0)
                {
                    zehntausend.CheckForNextPlayer(players, playerIndexInListOfPlayers);
                    break;
                }

                Console.Write("Continue playing (y/n)? ");
                if (Console.ReadKey().Equals("y") || Console.ReadKey().Equals("Y"))
                {
                    Console.WriteLine("Play on...");
                    player.Points = points;

                    Environment.Exit(0);
                }
                else
                {
                    if (points >= 10000)
                    {
                        zehntausend.ShowPoints(players);
                        Console.Clear();
                        Console.WriteLine("And the winner is...");
                        Console.WriteLine();
                        Console.WriteLine("***********   {0}    ************", player.Name);
                        Console.WriteLine();
                        Console.WriteLine("With {0} points.", points);
                        Console.WriteLine();

                        UserInteraction.AwaitKeyAndClearConsole();

                        Environment.Exit(0);
                    }

                    player = zehntausend.CheckForNextPlayer(players, playerIndexInListOfPlayers);
                }
            } while (player.Points <= 10000);
        }
    }
}