using System;
using System.Collections.Generic;

namespace Shkadun_Princess
{
    public class Game
    {
        private const string WIN = "Win";
        private const string BOMB = "Bomb";
        private const string ACTIVE = "Active";
        private const string PLAYER = "Player";

        // Массив ячеек
        string[][] gameField;
        // Объект класса для консольного ввода-вывода сообщений
        ConsoleWork consoleWrok;   
        private int CountMines = 10;
        public int FieldVertical { get; private set; } = 10;
        public int FieldHorizontal { get; private set; } = 10;
        private List<Mine> ListMines;

        // Генерация мин
        public void GenerationBombCells()
        {
            Random random = new Random();

            int[] positionMines = new int[10];
            int damage;

            for (int generationMine = 0; generationMine < CountMines; generationMine++)
            {
                positionMines[generationMine] = random.Next(1, (FieldVertical * FieldHorizontal - 1));

                for (int mine = 0; mine < generationMine; mine++)
                {
                    if (positionMines[mine] == positionMines[generationMine]) 
                    { 
                        generationMine--; 
                    }
                }

                damage = random.Next(1, 10);
                // Положение мины
                gameField[positionMines[generationMine] / 10][positionMines[generationMine] % 10] = BOMB;    
                ListMines.Add(new Mine(damage, positionMines[generationMine] / 10, positionMines[generationMine] % 10));
            }
        }

        // Проверка мины на клетке, на которую стал юзер
        public int CheckBomb(Player player)
        {
            // Если юзер на клетке 9-9 - победа
            if ((player.PositionHorizontal == FieldHorizontal - 1) && 
                (player.PositionVertical == FieldVertical - 1)) 
            {
                player.GameOver = WIN;
                return 0; 
            }

            // Если клетка пустая, то ничего не происходит
            if (gameField[player.PositionVertical][player.PositionHorizontal] == null) 
            { 
                return 0;
            }

            // Если на клетке мина
            if (gameField[player.PositionVertical][player.PositionHorizontal] == BOMB)
            {
                foreach(Mine mine in ListMines)
                {
                    if ((mine.PositionHorizontal == player.PositionHorizontal) &&
                        (mine.PositionVertical == player.PositionVertical))
                    {
                        if(mine.Status == ACTIVE)
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
                    if (i == player.PositionVertical && o == player.PositionHorizontal) 
                    { 
                        consoleWrok.DrowCell(PLAYER); 
                    }

                    // Если пустая, то пустую ячейку
                    else if (gameField[i][o] == null) 
                    {
                        consoleWrok.DrowCell(null); 
                    }

                    // Если мина, то проверяем её статус
                    else if (gameField[i][o] == BOMB) 
                    { 
                        foreach(Mine mine in ListMines)
                        {
                            if(mine.PositionVertical == i && mine.PositionHorizontal == o)
                            {
                                consoleWrok.DrowCell(BOMB, mine);
                                break;
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public Game()
        {
            consoleWrok = new ConsoleWork();
            ListMines = new List<Mine>();
            gameField = new string[FieldVertical][];
            for (int i = 0; i < 10; i++)
            {
                gameField[i] = new string[FieldHorizontal];
            }
        }
    }
}
