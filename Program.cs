using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.Write("How many players do you want to create? ");
            int numberOfPlayers = Game.GetNumberOfPlayers();

            Console.Clear();

            // Spielernamen vergeben
            Console.WriteLine("Gib die Namen der Spieler ein...");
            Console.WriteLine();
            List<Player> players = Game.NamePlayers(numberOfPlayers);

            Console.Clear();

            // Startspieler ermitteln
            Player firstPlayer;
            firstPlayer = Game.DeterminePlayerToStartGame(players);
            Console.WriteLine();
            Console.WriteLine("{0} beginnt...", firstPlayer.Name);

            ControlGame.AwaitKeyAndClearConsole();

            Console.Clear();

            // Würfelbecher mit sechs Würfeln füllen
            int[] dices = Dice.FillDiceCupWithAllDices(6);

            Console.WriteLine("Augenzahlen sind...");
            foreach (var dice in dices)
            {
                Console.Write("{0,2}", dice);
            }

            ControlGame.AwaitKeyAndClearConsole();

            int[,] occurrenceOfEyes = Game.FindOccurrenceOfEyes(dices);

            for (int i = 0; i < 6; i++)
            {
                Console.Write("Augenzahl: {0}   ", occurrenceOfEyes[i, 0]);
                Console.WriteLine("Häufigkeit: {0}", occurrenceOfEyes[i, 1]);
            }

            Console.WriteLine("Du hast bis jetzt {0} Punkte.", Game.GetPoints(occurrenceOfEyes));
            Console.Write("Willst Du weiterspielen (j/n)? ");
            

            Console.ReadKey();

            ControlGame.AwaitKeyAndClearConsole();
        }
    }

    class Player
    {
        public string Name { get; set; }
        public int Eyes { get; set; }
        public int Points { get; set; }
    }

    class Dice
    {
        static Random random = new Random();
        public int Eyes { get; set; }

        public static int DiceEyes()
        {
            int eyes = random.Next(1, 7);

            return eyes;
        }

        public static int[] FillDiceCupWithAllDices(int amountOfDices)
        {
            int[] dices = new int[amountOfDices];

            for (int i = 0; i < amountOfDices; i++)
            {
                dices[i] = DiceEyes();
            }

            return dices;
        }
    }

    class ControlGame
    {
        public static void AwaitKeyAndClearConsole()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
        }
    }

    class Game
    {
        public static int GetNumberOfPlayers()
        {
            int numberOfPlayers;

            do
            {
                Console.Write("Bitte Spieleranzahl eingeben (min 2 / max 5): ");
                _ = int.TryParse(Console.ReadLine(), out numberOfPlayers);
            } while (numberOfPlayers < 2 || numberOfPlayers > 5);

            return numberOfPlayers;
        }

        public static List<Player> NamePlayers(int numberOfPlayers)
        {
            List<Player> players = new List<Player>();
            Player player = new Player();
            string name;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                bool isLengthOkay, isNameUnique = true;

                do
                {
                    Console.Write("Name des Spielers {0}: ", i + 1);
                    name = Console.ReadLine();
                    if (name.Length < 2 || name.Length > 20)
                    {
                        isLengthOkay = false;
                        Console.WriteLine("Bitte zwischen 2 und 20 Zeichen verwenden.");
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
                            Console.WriteLine("Name bereits vergeben. Bitte anderen Namen angeben.");
                            Console.WriteLine();
                            break;
                        }
                    }
                } while (isLengthOkay == false || isNameUnique == false);

                players.Add(new Player() { Name = name });

                Console.WriteLine();
            }

            return players;
        }

        public static Player DeterminePlayerToStartGame(List<Player> players)
        {
            foreach (var item in players)
            {
                var eyes = Dice.DiceEyes();
                item.Eyes = eyes;
            }

            foreach (var item in players)
            {
                Console.WriteLine("{0,21} würfelte {1}", item.Name, item.Eyes);
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
                Console.WriteLine("Stechen...");
                Console.WriteLine("Computer ermittelt den Startspieler zufällig...");
                Random randomStartPlayer = new Random();
                indexOfStartPlayer = randomStartPlayer.Next(0, startPlayers.Count);
            }
            else
                return firstPlayer;

            return firstPlayer = startPlayers[indexOfStartPlayer];
        }

        public static int[,] FindOccurrenceOfEyes(int[] dices)
        {
            int[,] occurrences = new int[6, 2];

            int numbersOccurrenceOf1 = dices.Count(s => s == 1);
            int numbersOccurrenceOf2 = dices.Count(s => s == 2);
            int numbersOccurrenceOf3 = dices.Count(s => s == 3);
            int numbersOccurrenceOf4 = dices.Count(s => s == 4);
            int numbersOccurrenceOf5 = dices.Count(s => s == 5);
            int numbersOccurrenceOf6 = dices.Count(s => s == 6);
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

        public static int GetPoints(int[,] occurrenceOfEyes)
        {
            int points = 0;

            // Punkte für Augenzahl 1
            if (occurrenceOfEyes[0, 1] <= 2)
                points += occurrenceOfEyes[0, 1] * 100;
            // Punkte bei Dreierpasch für Augenzahl 1
            if (occurrenceOfEyes[0, 1] == 3)
                points += 1000;
            // Punkte bei Viererpasch für Augenzahl 1
            if (occurrenceOfEyes[0,1] == 4)
                points += 10000;
            // Punkte für Augenzahl 5
            if (occurrenceOfEyes[4, 1] <= 2)
                points += occurrenceOfEyes[4, 1] * 50;
            // Punkte für eine Straße (1,2,3,4,5 oder 2,3,4,5,6)
            if ((occurrenceOfEyes[0, 1] == 1 && occurrenceOfEyes[1, 1] == 1 && occurrenceOfEyes[2, 1] == 1 && occurrenceOfEyes[3, 1] == 1 && occurrenceOfEyes[4, 1] == 1) ||
                (occurrenceOfEyes[1, 1] == 1 && occurrenceOfEyes[2, 1] == 1 && occurrenceOfEyes[3, 1] == 1 && occurrenceOfEyes[4, 1] == 1 && occurrenceOfEyes[5, 1] == 1))
                points += 10000;

            // Punkte für Dreier- und Viererpasch aller Augenzahlen außer Augenzahl 1
            for (int i = 1; i < 6; i++)
            {
                if (occurrenceOfEyes[i, 1] == 3)
                {
                    points += occurrenceOfEyes[i, 0] * 100;
                }
                if (occurrenceOfEyes[i, 1] == 4)
                {
                    points += occurrenceOfEyes[i, 0] * 1000;
                }
            }

            return points;
        }
    }
}