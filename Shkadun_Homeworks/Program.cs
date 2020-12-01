using System;
using System.Threading;

namespace Shkadun_Princess
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleWork consoleWork = new ConsoleWork();
            Player player = new Player();
            Game game = new Game();

            game.GenerationBombCells();
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

                    default: break;
                }
                if (player.GameOver != null)
                {
                    if (consoleWork.GameOver(player.GameOver) == "new game")
                    {
                        player = new Player();
                        game = new Game();
                        game.GenerationBombCells();
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
