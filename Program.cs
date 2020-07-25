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
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();

            Console.Clear();

            // Würfelbecher mit sechs Würfeln füllen
            List<Dice> dices = Dice.FillDiceCupWithAllDices();

            Console.WriteLine("Augenzahlen sind...");
            foreach (var item in dices)
            {
                Console.Write("{0,2}", item.Eyes);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();

            Game.GetPoints(dices);

            Console.ReadKey();
        }
    }

    class Player
    {
        public string Name { get; set; }
        public int Eyes { get; set; }
    }

    class Dice
    {
        static Random random = new Random();
        public int Eyes { get; set; }

        private static int DiceOneDice()
        {
            return random.Next(1, 7);
        }

        public static int GetEyes()
        {
            int eyes = random.Next(1, 7);

            return eyes;
        }

        public static List<Dice> FillDiceCupWithAllDices()
        {
            List<Dice> dices = new List<Dice>();

            for (int i = 0; i < 6; i++)
            {
                dices.Add(new Dice() { Eyes = DiceOneDice() });
            }

            return dices;
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
                var eyes = Dice.GetEyes();
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

        public static int GetPoints(List<Dice> dices)
        {
            int points = 0;
            Dictionary<int, Dice> toDict = dices.Select((s, i) => new { s, i })
             .ToDictionary(x => x.i, x => x.s);

            foreach (var item in toDict)
            {
                Console.Write(item.Key + " " + item.Value.Eyes);
            }

            return points;
        }
    }
}