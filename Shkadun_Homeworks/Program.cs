using System;
using System.Threading;

namespace Shkadun_Princess
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkWithConsole workWithConsole = new WorkWithConsole();
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
                    if (workWithConsole.GameOver(1) == 0) { break; }
                    map.StartNewGame(player);
                }
                else if (player.HP > 10)
                {
                    if (workWithConsole.GameOver(0) == 0) { break; }
                    map.StartNewGame(player);
                }
                Thread.Sleep(200);
            }
        }
    }
}
