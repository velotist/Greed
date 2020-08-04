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

            // Anzahl Spieler ermitteln
            Console.WriteLine("Welcome to Chicago");
            Console.WriteLine();
            Console.WriteLine("How many players do you want to create? ");
            int numberOfPlayers = Game.GetNumberOfPlayers();

            Console.Clear();

            // Spielernamen vergeben
            Console.WriteLine("Name the players now...");
            Console.WriteLine();
            List<Player> players = Game.NamePlayers(numberOfPlayers);

            Console.Clear();

            // Startspieler ermitteln
            Player player;
            player = Game.DeterminePlayerToStartGame(players);
            int playerIndexInListOfPlayers = players.IndexOf(player);
            Console.WriteLine("Index des Spielers in der Liste: {0}", playerIndexInListOfPlayers);
            Console.WriteLine();
            Console.WriteLine("{0} starts...", player.Name);

            UserInteraction.AwaitKeyAndClearConsole();

            // Spiele Spiel bis einer der Spieler mehr als 10000 Punkte hat
            int points = 0;
            do
            {
                // Würfelbecher mit sechs Würfeln füllen
                int[] dices = Dice.FillDiceCupWithDices(6);
                ArrayList collectedDices = new ArrayList();
                Console.WriteLine("Eyes are...");
                foreach (var dice in dices)
                {
                    Console.Write("{0,2}", dice);
                }

                UserInteraction.AwaitKeyAndClearConsole();

                // Speichere Häufigkeit einer Augenzahl
                int[,] occurrenceOfEyes = Game.FindOccurrenceOfEyes(dices);

                // Gebe die Liste der Augenzahlen mit deren Häufigkeit aus
                //for (int i = 0; i < 6; i++)
                //{
                //    Console.Write("Eyes: {0}   ", occurrenceOfEyes[i, 0]);
                //    Console.WriteLine("Occurrence: {0}", occurrenceOfEyes[i, 1]);
                //}

                Console.WriteLine();
                points = Game.GetPoints(occurrenceOfEyes);
                Console.WriteLine("You now have {0} points.", points);
                Console.WriteLine();

                UserInteraction.AwaitKeyAndClearConsole();

                Console.Write("Continue playing (y/n)? ");
                if (Console.ReadLine().Equals("y") || Console.ReadLine().Equals("Y"))
                {
                    Console.WriteLine("Play on...");
                    player.Points = points;

                    Environment.Exit(0);
                }
                else
                {
                    if (points >= 10000)
                    {
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

                    if (playerIndexInListOfPlayers > players.Count)
                        playerIndexInListOfPlayers = -1;
                    player = players[playerIndexInListOfPlayers++];
                    Console.WriteLine("Next player is {0}", player.Name);
                }
            } while (player.Points <= 10000);
        }
    }
}