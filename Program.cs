using System;
using System.Collections.Generic;
using System.Linq;

namespace Greed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Chicago");
            Console.WriteLine();
            Console.Write("How many players do you want to create? ");
            int numberOfPlayers = Game.GetNumberOfPlayers();

            Console.Clear();

            Console.WriteLine("Gib die Namen der Spieler ein...");
            Console.WriteLine();
            List<Player> players = Game.NamePlayers(numberOfPlayers);

            Console.Clear();

            // Startspieler ermitteln
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
            string firstPlayer = "";
            foreach (var item in players)
            {
                if (item.Eyes == max)
                {
                    firstPlayer = item.Name;
                }
            }
            Console.WriteLine();
            Console.WriteLine("{0} beginnt...", firstPlayer);
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();

            Console.Clear();

            int points = 0;
            List<int> dices = new List<int>();
            //for (int i = 0; i < 5; i++)
            //{
            //    int diceEyes = Dice.GetEyes();
            //    dices.Add(diceEyes);
            //}

            //Console.WriteLine("Spieler {0} würfelte", players);
            //Console.WriteLine();
            //foreach (var item in dices)
            //{
            //    Console.Write(item + "  ");
            //}
            //Console.WriteLine();

            Dictionary<int, int> dict = Game.GetOccurenceOfEyes(dices);
            foreach (var item in dict)
            {
                Console.WriteLine("Zahl: {0} Häufigkeit: {1}", item.Key, item.Value);
                Console.WriteLine();
                if (item.Key == 1)
                {
                    points += item.Value * 100;
                    dices.RemoveAt(item.Key);
                }
                if (item.Key == 5)
                {
                    points += item.Value * 50;
                    dices.Remove(item.Key);
                }
                if (item.Value == 3)
                    if (item.Key > 1)
                    {
                        points = item.Key * 100;
                        dices.Remove(item.Key);
                    }
                if (item.Value == 4)
                    if (item.Key > 1)
                    {
                        points = item.Key * 1000;
                        dices.Remove(item.Key);
                    }
            }

            Console.WriteLine("Spieler {0} hat {1} Punkte.", players[0], points);

            foreach (var item in dices)
            {
                Console.WriteLine(item);
            }

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

        public static int GetEyes()
        {
            int eyes = random.Next(1, 7);

            return eyes;
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

        public static Dictionary<int, int> GetOccurenceOfEyes(List<int> list)
        {
            list.Sort();

            Dictionary<int, int> numbersOccurence = list
                                        .GroupBy(item => item)
                                        .ToDictionary(item => item.Key, item => item.Count());

            return numbersOccurence;
        }
    }
}