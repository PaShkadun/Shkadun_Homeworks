using System;

namespace Shkadun_Princess
{
    public class ConsoleWork
    {
        private const string NAME_GAME = "The princess game";
        private const string PLAYER_HP = "HP";
        private const string HORIZONT_LINE = "----------------------";
        private const string REPLAY_GAME = "Ещё раз? y - да, n - нет";

        public string GameOver(int result)
        {
            if (result == 0) { Console.WriteLine("Вы выиграли."); }
            else { Console.WriteLine("Вы проиграли."); }

            Console.WriteLine(REPLAY_GAME);

            //ReadLine() = y = new game, ReadLine != y = exit
            if (Console.ReadLine() == "y") { return "new game"; }
            else { return "exit"; }
        }

        public void DrowLine(int line)
        {
            Console.WriteLine(HORIZONT_LINE);
        }

        public void DrowCell(string contentCell, Mine mine = null)
        {
            if(contentCell == "Player") { Console.Write("P|"); }
            else if(contentCell == "Mine" && mine.Status == "Active") { Console.Write("Z|"); }
            else if (contentCell == "Mine" && mine.Status == "Inactive") { Console.Write("X|"); }
            else { Console.Write(" |"); }
        }

        public void WriteGameInfo(Player player)
        {
            Console.WriteLine($"{PLAYER_HP} {player.HP}");
            Console.WriteLine(NAME_GAME);
        }
    }
}
