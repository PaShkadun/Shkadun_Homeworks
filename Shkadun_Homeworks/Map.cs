using System;

namespace Shkadun_Princess
{
    class Map
    {
        object[][] cell;            //карта
        ConsoleWriteAndRead cwar;   //Объект класса для консольного ввода-вывода сообщений

        //Создание карты
        public void StartNewGame(Player player)
        {
            cell = null;
            cell = new object[10][];
            for (int i = 0; i < 10; i++)
            {
                cell[i] = new object[10];
            }
            player.StartPlayerPosition();   //Ставит юзера на клетку 0-0
            GenerationMines();              //Генерирует мины
            DrowMap(player);                //Рисует карту, игрока на поле, мины(взорванные)
        }

        //Генерация мин
        public void GenerationMines()
        {
            Random random = new Random();
            int[] position = new int[10];
            int damage;
            for (int i = 0; i < 10; i++)
            {
                position[i] = random.Next(1, 99);
                for (int o = 0; o < i; o++)
                {
                    if (position[i] == position[o]) { i--; }
                }
                damage = random.Next(1, 10);
                cell[position[i] / 10][position[i] % 10] = new Mine(damage);    //Положение мины
            }
        }

        //Проверка мины на клетке, на которую стал юзер
        public int CheckMine(Player player)
        {
            Mine mine = new Mine(0);
            //Если юзер на клетке 9-9 - победа
            if (player.PlayerX == 9 && player.PlayerY == 9) { player.setHpWin(); return 0; }
            //Если клетка пустая, то ничего не происходит
            if (cell[player.PlayerX][player.PlayerY] == null) { return 0; }
            //Если на клетке мина
            if (cell[player.PlayerX][player.PlayerY].GetType() == mine.GetType())
            {
                mine = (Mine)cell[player.PlayerX][player.PlayerY];
                if (mine.Status == "Active") { mine.InactiveMine(); return mine.Damage; } //Если активная, снимает HP
                else { return 0; }
            }
            else { return 0; }
        }

        //Рисует карту
        public void DrowMap(Player player)
        {
            Mine mine = new Mine(0);
            Console.Clear();
            cwar.WriteGameName();   //Выводит название игры
            cwar.WriteHP(player);   //Выводит HP юзера
            for (int i = 0; i < cell.Length; i++)
            {
                cwar.DrowLine(i);   //Рисует разделительную полосу(--------)
                for (int o = 0; o < cell[i].Length; o++)
                {
                    //Если положение юзера, выводим Y
                    if (i == player.PlayerX && o == player.PlayerY) { cwar.DrowPlayer(); }
                    //Если пустая, то пустую ячейку
                    else if (cell[i][o] == null) { cwar.DrowEmptyCell(); }
                    //Если мина, то проверяем её статус
                    else if (cell[i][o].GetType() == mine.GetType()) { cwar.DrowMineCell((Mine)cell[i][o]); }
                }
                Console.WriteLine();
            }
            cwar.DrowLine(10);
        }

        public Map()
        {
            cwar = new ConsoleWriteAndRead();
            cell = new object[10][];
            for (int i = 0; i < 10; i++)
            {
                cell[i] = new object[10];
            }
        }
    }
}
