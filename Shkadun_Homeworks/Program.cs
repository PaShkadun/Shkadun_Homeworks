using System;

namespace Shkadun_Princess
{
    public class Program
    {
        private const string newGame = "new game";
        private const string startNewGame = "Start new game? y/n";

        static void Main(string[] args)
        {
            Game game = new Game();

            game.DrowMap();

            bool inGame = true;

            while (inGame)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow: 
                        game.player.Move(game, vertical: -1); 
                        break;

                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow: 
                        game.player.Move(game, vertical: +1); 
                        break;

                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow: 
                        game.player.Move(game, horizontal: +1); 
                        break;

                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow: 
                        game.player.Move(game, horizontal: -1); 
                        break;

                    default: 
                        break;
                }

                if (game.player.gameOver != null)
                {
                    Console.WriteLine($"You {game.player.gameOver}. {startNewGame}");

                    if (Console.ReadLine() == newGame)
                    {
                        game = new Game();
                        game.DrowMap();
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
