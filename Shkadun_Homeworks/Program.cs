using System;
using System.Threading;

namespace Shkadun_Princess
{
    public class Program
    {
        private const string NewGame = "new game";
        private const string StartNewGame = "Start new game? y/n";

        static void Main(string[] args)
        {
            Player player = new Player();
            Game game = new Game();

            game.DrowMap(player);

            bool inGame = true;

            while (inGame)
            {

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow: 
                        player.Move(game, vertical: -1); 
                        break;

                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow: 
                        player.Move(game, vertical: +1); 
                        break;

                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow: 
                        player.Move(game, horizontal: +1); 
                        break;

                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow: 
                        player.Move(game, horizontal: -1); 
                        break;

                    default: 
                        break;
                }

                if (player.GameOver != null)
                {

                    Console.WriteLine($"You {player.GameOver}. {StartNewGame}");

                    if (Console.ReadLine() == NewGame)
                    {
                        player = new Player();
                        game = new Game();
                        game.DrowMap(player);
                    }
                    else
                    {
                        inGame = false;
                    }
                }
            }
        }
    }
}
