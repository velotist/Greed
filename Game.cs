using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greed
{
    public class Game
    {
        public List<Player> players = new List<Player>();
        public int NumberOfDice { get; set; }
        public int NumberOfPlayers { get; set; }

        public Game()
        {
            NumberOfDice = 6;
        }

        public int GetNumberOfPlayers()
        {
            int numberOfPlayers;

            do
            {
                Console.WriteLine();
                Console.Write("...give in a number between 2 and 5, please: ");
                _ = int.TryParse(Console.ReadLine(), out numberOfPlayers);
            } while (numberOfPlayers < 2 || numberOfPlayers > 5);
            NumberOfPlayers = numberOfPlayers;

            return NumberOfPlayers;
        }

        public List<Player> NamePlayers(int numberOfPlayers)
        {
            Player player = new Player();
            string name;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                bool isLengthOkay, isNameUnique = true;

                do
                {
                    Console.Write("Name of player {0}: ", i + 1);
                    name = Console.ReadLine();
                    if (name.Length < 2 || name.Length > 20)
                    {
                        isLengthOkay = false;
                        Console.WriteLine("...please fill in between 2 and 20 characters.");
                        Console.WriteLine();
                    }
                    else
                        isLengthOkay = true;

                    player.Name = name;

                    foreach (var item in players)
                    {
                        if (!item.Name.Equals(name))
                        {
                            isNameUnique = true;
                        }
                        else
                        {
                            isNameUnique = false;
                            Console.WriteLine("Name already exists. Please choose another name.");
                            Console.WriteLine();
                            break;
                        }
                    }
                } while (isLengthOkay == false || isNameUnique == false);

                players.Add(new Player() { Name = name, Points = 0 });

                Console.WriteLine();
            }

            return players;
        }

        public Player DeterminePlayerToStartGame(List<Player> players)
        {
            Random random = new Random();
            foreach (var item in players)
            {
                var eyes = random.Next(1, 7);
                item.Eyes = eyes;
            }

            foreach (var item in players)
            {
                Console.WriteLine("{0,21} dice {1}", item.Name, item.Eyes);
            }

            int max = players.Max(x => x.Eyes);
            Player firstPlayer = new Player();
            List<Player> startPlayers = new List<Player>();
            foreach (var item in players)
            {
                if (item.Eyes == max)
                {
                    startPlayers.Add(item);
                    firstPlayer = item;
                }
            }

            int indexOfStartPlayer = 0;
            if (startPlayers.Count >= 2)
            {
                Console.WriteLine("Stitch...");
                Console.WriteLine("Computer determines the starting player randomly...");
                Random randomStartPlayer = new Random();
                indexOfStartPlayer = randomStartPlayer.Next(0, startPlayers.Count);
            }
            else
                return firstPlayer;

            return firstPlayer = startPlayers[indexOfStartPlayer];
        }

        public int[,] FindOccurrenceOfEyes(int[] numberOfDice)
        {
            int[,] occurrences = new int[6, 2];

            int numbersOccurrenceOf1 = numberOfDice.Count(s => s == 1);
            int numbersOccurrenceOf2 = numberOfDice.Count(s => s == 2);
            int numbersOccurrenceOf3 = numberOfDice.Count(s => s == 3);
            int numbersOccurrenceOf4 = numberOfDice.Count(s => s == 4);
            int numbersOccurrenceOf5 = numberOfDice.Count(s => s == 5);
            int numbersOccurrenceOf6 = numberOfDice.Count(s => s == 6);
            occurrences[0, 0] = 1;
            occurrences[0, 1] = numbersOccurrenceOf1;
            occurrences[1, 0] = 2;
            occurrences[1, 1] = numbersOccurrenceOf2;
            occurrences[2, 0] = 3;
            occurrences[2, 1] = numbersOccurrenceOf3;
            occurrences[3, 0] = 4;
            occurrences[3, 1] = numbersOccurrenceOf4;
            occurrences[4, 0] = 5;
            occurrences[4, 1] = numbersOccurrenceOf5;
            occurrences[5, 0] = 6;
            occurrences[5, 1] = numbersOccurrenceOf6;

            return occurrences;
        }

        public int GetPoints(int[,] occurrenceOfEyes)
        {
            int points = 0;

            // Points for eyes 1
            if (occurrenceOfEyes[0, 1] <= 2)
            {
                points += occurrenceOfEyes[0, 1] * 100;
                NumberOfDice -= occurrenceOfEyes[0, 1];
            }
            // Points for triplets eyes 1
            if (occurrenceOfEyes[0, 1] == 3)
            {
                points += 1000;
                NumberOfDice -= 3;
            }
            // Points for quadruple eyes 1
            if (occurrenceOfEyes[0, 1] == 4)
            {
                points += 10000;
                NumberOfDice -= 4;
            }
            // Points for eyes 5
            if (occurrenceOfEyes[4, 1] <= 2)
            {
                points += occurrenceOfEyes[4, 1] * 50;
                NumberOfDice -= occurrenceOfEyes[4, 1];
            }
            // Points for  1, 2, 3, 4, 5, 6
            if ((occurrenceOfEyes[0, 1] == 1 && occurrenceOfEyes[1, 1] == 1 && occurrenceOfEyes[2, 1] == 1 && occurrenceOfEyes[3, 1] == 1 && occurrenceOfEyes[4, 1] == 1 && occurrenceOfEyes[5,1] == 1))
            {
                points += 1000;
                NumberOfDice -= 6;
            }

            // Points for triplets or quadruple of eye 2, 3, 4, 5, 6
            for (int i = 1; i < 6; i++)
            {
                if (occurrenceOfEyes[i, 1] == 3)
                {
                    points += occurrenceOfEyes[i, 0] * 100;
                    NumberOfDice -= 3;
                }
                
                if (occurrenceOfEyes[i, 1] == 4 || occurrenceOfEyes[i, 1] == 5)
                {
                    points += occurrenceOfEyes[i, 0] * 1000;
                    NumberOfDice -= 4;
                }
                if (occurrenceOfEyes[i, 1] == 6)
                {
                    points += occurrenceOfEyes[i, 0] * 100;
                    NumberOfDice -= 6;
                }
            }

            if (NumberOfDice <= 0)
                NumberOfDice = 6;

            return points;
        }

        public void ShowPoints(List<Player> players)
        {
            Console.Clear();
            foreach (var item in players)
            {
                Console.WriteLine(String.Format("Player: {0,-22} Points: {1,-5:D5}", item.Name, item.Points));
            }
            Console.WriteLine();
        }

        public int GetPlayerIndex(int playerIndexInListOfPlayers)
        {
            if (playerIndexInListOfPlayers >= (players.Count - 1))
            {
                return 0;
            }

            playerIndexInListOfPlayers++;

            return playerIndexInListOfPlayers;
        }

        public Player CheckForNextPlayer(List<Player> players, int playerIndexInListOfPlayers)
        {
            Player player;

            player = players[GetPlayerIndex(playerIndexInListOfPlayers)];
            Console.WriteLine("Next player is {0}", player.Name);

            return player;
        }

        public void WinningGame(Player player)
        {
            ShowPoints(players);
            Console.WriteLine();
            Console.WriteLine("And the winner is...");
            Console.WriteLine();
            Console.WriteLine("***********   {0}    ************", player.Name);
            Console.WriteLine();
            Console.WriteLine("With {0} points.", player.Points);
            Console.WriteLine();

            UserInteraction.AwaitKeyAndClearConsole("Press any key to quit game...");

            Environment.Exit(0);

        }
    }
}