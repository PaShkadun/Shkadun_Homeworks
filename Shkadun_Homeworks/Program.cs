using System;
using System.Threading;

namespace Shkadun_Princess
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleWork consoleWrok = new ConsoleWork();
            Player player = new Player();
            Map map = new Map();

            map.GenerationMines();
            map.DrowMap(player);

            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow: player.PlayerRunUp(map); break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow: player.PlayerRunDown(map); break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow: player.PlayerRunRight(map); break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow: player.PlayerRunLeft(map); break;
                    default: break;
                }
                if (player.HP <= 0)
                {
                    if (consoleWrok.GameOver(1) == "new game") { break; }
                    map.StartNewGame(player);
                }
                else if (player.HP > 10)
                {
                    if (consoleWrok.GameOver(0) == "exit") { break; }
                    map.StartNewGame(player);
                }
                Thread.Sleep(200);
            }
        }
    }
}
