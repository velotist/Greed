using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greed
{
    class ApplicationController
    {
        private Dice dice;
        private Player player;
        private Game game;
        private int numberOfPlayers;
        private List<Player> players;
        private int playerIndexInListOfPlayers;

        public ApplicationController(Dice dice, Player player, Game game)
        {
            this.dice = dice;
            this.player = player;
            this.game = game;
        }

        public void StartApplication()
        {
            // Anzahl Spieler ermitteln
            Console.WriteLine("Welcome to Chicago");
            Console.WriteLine();
            Console.WriteLine("How many players do you want to create? ");
            numberOfPlayers = ReturnNumberOfPlayers();
        }

        private int ReturnNumberOfPlayers()
        {
            int numberOfPlayers = game.GetNumberOfPlayers();

            return numberOfPlayers;
        }

        public List<Player> ReturnListOfPlayers()
        {
            // Spielernamen vergeben
            Console.WriteLine("Name the players now...");
            Console.WriteLine();
            List<Player> players = game.NamePlayers(numberOfPlayers);

            return players;
        }

        public Player GetPlayerToStartGame()
        {
            // Startspieler ermitteln
            player = game.DeterminePlayerToStartGame(players);
            
            Console.WriteLine();
            Console.WriteLine("{0} starts...", player.Name);

            return player;
        }

        public int ReturnPlayerIndexInList()
        {
            playerIndexInListOfPlayers = players.IndexOf(player);

            return playerIndexInListOfPlayers;
        }
    }
}