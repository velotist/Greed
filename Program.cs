using System;
using System.Collections;
using System.Collections.Generic;

namespace Greed
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Game zehntausend = new Game();
            Dice diceCup = new Dice();

            Console.WriteLine("Welcome to Greed");
            Console.WriteLine();

            Console.WriteLine("How many players do you want to create? ");
            int numberOfPlayers = zehntausend.GetNumberOfPlayers();

            Console.Clear();

            Console.WriteLine("Name the players now...");
            Console.WriteLine();
            List<Player> players = zehntausend.NamePlayers(numberOfPlayers);

            Console.Clear();

            // determine player, who starts the game
            Player player;
            player = zehntausend.DeterminePlayerToStartGame(players);
            int playerIndexInListOfPlayers = players.IndexOf(player);
            Console.WriteLine();
            Console.WriteLine("{0} starts...", player.Name);

            UserInteraction.AwaitKeyAndClearConsole("Press any key to continue...");

            int points;
            int totalPoints = 0;
            bool playAgain = false;

            do
            {
                if (playAgain)
                {
                    points = 0;
                    totalPoints += points;
                }
                else
                {
                    points = 0;
                    totalPoints = 0;
                }
                zehntausend.ShowPoints(players);
                Console.Write("Player {0} throws...", player.Name);

                int[] numberOfDice = diceCup.FillDiceCupWithDice(zehntausend.NumberOfDice);
                foreach (var dice in numberOfDice)
                {
                    Console.Write("{0,3}", dice);
                }

                // save occurrences of each eye
                int[,] occurrenceOfEyes = zehntausend.FindOccurrenceOfEyes(numberOfDice);

                // calculate points
                Console.WriteLine();
                Console.WriteLine();
                points += zehntausend.GetPoints(occurrenceOfEyes);

                if (points == 0 && player.Points < 10000)
                {
                    playAgain = false;
                    zehntausend.NumberOfDice = 6;
                    player.Points += 0;
                    playerIndexInListOfPlayers = zehntausend.GetPlayerIndex(playerIndexInListOfPlayers);
                    player = players[playerIndexInListOfPlayers];

                    Console.WriteLine("Oh no, you have no points at least.");
                    Console.WriteLine();

                    UserInteraction.AwaitKeyAndClearConsole("Press key...");

                    continue;
                }

                totalPoints += points;

                // if same player plays again
                if (playAgain)
                {
                    Console.WriteLine("You now have {0} points, {1}", totalPoints, player.Name);
                    Console.WriteLine();
                    Console.WriteLine("You now have {0} dice left to dice with...", zehntausend.NumberOfDice);
                }
                else
                {
                    Console.WriteLine("You have {0} points, {1}", points, player.Name);
                    Console.WriteLine();
                    Console.WriteLine("You have {0} dice left to dice with...", zehntausend.NumberOfDice);
                }
                Console.WriteLine();


                if (zehntausend.NumberOfDice > 0)
                {
                    Console.Write("Continue playing (y/n)? ");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        playAgain = true;

                        continue;
                    }
                    else
                    {
                        player.Points += totalPoints;
                        if (player.Points >= 10000)
                        {
                            Console.Clear();

                            Console.WriteLine();
                            Console.WriteLine("Player {0} wins with {1} points.", player.Name, player.Points);
                            Console.WriteLine();

                            Console.Write("Press key to show scores...");
                            Console.ReadKey();
                            
                            zehntausend.WinningGame(player);
                        }
                        else
                        {
                            playAgain = false;
                            zehntausend.NumberOfDice = 6;

                            playerIndexInListOfPlayers = zehntausend.GetPlayerIndex(playerIndexInListOfPlayers);
                            player = players[playerIndexInListOfPlayers];
                        }
                    }
                }
            } while (player.Points < 10000);

            Console.Write("Press key...");
            Console.ReadKey();
            player.Points += totalPoints;
            zehntausend.WinningGame(player);
        }
    }
}