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

            UserInteraction.AwaitKeyAndClearConsole("Press any key to continue...");

            // Spiele Spiel bis einer der Spieler mehr als 10000 Punkte hat
            int points;
            int totalPoints;
            do
            {
                points = 0;
                totalPoints = 0;
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
                points += zehntausend.GetPoints(occurrenceOfEyes);
                Console.WriteLine("You now have {0} points, {1}", points, player.Name);
                totalPoints += points;
                Console.WriteLine();

                if (points == 0)
                {
                    player.Points += totalPoints;
                    playerIndexInListOfPlayers = zehntausend.GetPlayerIndex(playerIndexInListOfPlayers);
                    player = players[playerIndexInListOfPlayers];
                    UserInteraction.AwaitKeyAndClearConsole("Press key...");
                    continue;
                }
                else if (totalPoints >= 10000)
                {
                    player.Points = totalPoints;
                    zehntausend.CheckTenThousandPoints(player);
                }
                else
                {
                    Console.Write("Continue playing (y/n)? ");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        totalPoints -= points;
                        UserInteraction.AwaitKeyAndClearConsole("Play on...");

                        Environment.Exit(0);
                    }
                    else
                    {
                        player.Points += totalPoints;
                        playerIndexInListOfPlayers = zehntausend.GetPlayerIndex(playerIndexInListOfPlayers);
                        player = players[playerIndexInListOfPlayers];
                    }
                }
            } while (player.Points <= 10000);
        }
    }
}