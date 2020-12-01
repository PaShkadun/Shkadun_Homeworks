using System;
using System.Collections.Generic;

namespace Shkadun_Princess
{
    public class Map
    {
        // Массив ячеек
        string[][] gameField;
        // Объект класса для консольного ввода-вывода сообщений
        ConsoleWork consoleWrok;   
        private int CountMines = 10;
        public int FieldVertical { get; private set; }
        public int FieldHorizontal { get; private set; }
        private List<Mine> ListMines;

        // Создание игры
        public void StartNewGame(Player player)
        {
            gameField = null;
            gameField = new string[10][];
            for (int i = 0; i < FieldHorizontal; i++)
            {
                gameField[i] = new string[10];
            }
            // Ставит юзера на клетку 0-0
            player.StartPlayerPosition();
            // Генерирует мины
            GenerationMines();
            // Рисует карту, игрока на поле, мины(взорванные)
            DrowMap(player);                
        }

        // Генерация мин
        public void GenerationMines()
        {
            Random random = new Random();

            int[] positionMines = new int[10];
            int damage;

            for (int i = 0; i < CountMines; i++)
            {
                positionMines[i] = random.Next(1, 99);

                for (int o = 0; o < i; o++)
                {
                    if (positionMines[i] == positionMines[o]) 
                    { 
                        i--; 
                    }
                }

                damage = random.Next(1, 10);
                // Положение мины
                gameField[positionMines[i] / 10][positionMines[i] % 10] = "Mine";    
                ListMines.Add(new Mine(damage, positionMines[i] / 10, positionMines[i] % 10));
            }
        }

        // Проверка мины на клетке, на которую стал юзер
        public int CheckMine(Player player)
        {
            // Если юзер на клетке 9-9 - победа
            if ((player.PlayerPositionHorizontal == FieldHorizontal - 1) && 
                (player.PlayerPositionVertical == FieldVertical - 1)) 
            { 
                player.SetHpWin(); 
                return 0; 
            }

            // Если клетка пустая, то ничего не происходит
            if (gameField[player.PlayerPositionVertical][player.PlayerPositionHorizontal] == null) 
            { 
                return 0;
            }

            // Если на клетке мина
            if (gameField[player.PlayerPositionVertical][player.PlayerPositionHorizontal] == "Mine")
            {
                foreach(Mine mine in ListMines)
                {
                    if ((mine.PositionHorizontal == player.PlayerPositionHorizontal) &&
                        (mine.PositionVertical == player.PlayerPositionVertical))
                    {
                        if(mine.Status == "Active")
                        {
                            mine.InactiveMine();

                            return mine.Damage;
                        }
                    }
                }

                return 0; 
            }
            else 
            { 
                return 0; 
            }
        }

        // Рисует карту
        public void DrowMap(Player player)
        {
            Console.Clear();
            // Выводит HP юзера и название игры
            consoleWrok.WriteGameInfo(player);   

            for (int i = 0; i < FieldVertical; i++)
            {
                // Рисует разделительную полосу(--------)
                consoleWrok.DrowLine(i);   
                for (int o = 0; o < FieldHorizontal; o++)
                {
                    // Если положение юзера, выводим P
                    if (i == player.PlayerPositionVertical && o == player.PlayerPositionHorizontal) 
                    { 
                        consoleWrok.DrowCell("Player"); 
                    }

                    // Если пустая, то пустую ячейку
                    else if (gameField[i][o] == null) 
                    {
                        consoleWrok.DrowCell("Empty"); 
                    }

                    // Если мина, то проверяем её статус
                    else if (gameField[i][o] == "Mine") 
                    { 
                        foreach(Mine mine in ListMines)
                        {
                            if(mine.PositionVertical == i && mine.PositionHorizontal == o)
                            {
                                consoleWrok.DrowCell("Mine", mine);
                                break;
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public Map()
        {
            consoleWrok = new ConsoleWork();
            ListMines = new List<Mine>();
            FieldHorizontal = 10;
            FieldVertical = 10;
            gameField = new string[10][];
            for (int i = 0; i < 10; i++)
            {
                gameField[i] = new string[10];
            }
        }
    }
}
