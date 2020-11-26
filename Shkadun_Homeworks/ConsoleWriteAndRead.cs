using System;

namespace Shkadun_Princess
{
    class ConsoleWriteAndRead
    {
        public int GameOver(int result)
        {
            if (result == 0) { Console.WriteLine("Вы выиграли."); }
            else { Console.WriteLine("Вы проиграли. Ещё раз?"); }
            Console.WriteLine("Ещё раз? y/n");
            if (Console.ReadLine() == "y") { return 1; }
            else { return 0; }
        }

        public void DrowLine(int line)
        {
            Console.WriteLine("----------------------");
            if (line < 10) { Console.Write("|"); }
        }

        public void DrowPlayer()
        {
            Console.Write("Y|");
        }

        public void DrowEmptyCell()
        {
            Console.Write(" |");
        }

        public void DrowMineCell(Mine mine)
        {
            if (mine.Status == "Active") { Console.Write(" |"); }
            else { Console.Write("X|"); }
        }

        public void WriteHP(Player player)
        {
            Console.WriteLine($"HP {player.Hp}");
        }

        public void WriteGameName()
        {
            Console.WriteLine("The Princess game");
        }
    }
}
