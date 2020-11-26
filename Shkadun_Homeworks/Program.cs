using System;
using System.Threading;

namespace Shkadun_Princess
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleWriteAndRead cwar = new ConsoleWriteAndRead();
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
                if (player.Hp < 0)
                {
                    if (cwar.GameOver(1) == 0) { break; }
                    map.StartNewGame(player);
                }
                else if (player.Hp > 10)
                {
                    if (cwar.GameOver(0) == 0) { break; }
                    map.StartNewGame(player);
                }
                Thread.Sleep(200);
            }
        }
    }
}
