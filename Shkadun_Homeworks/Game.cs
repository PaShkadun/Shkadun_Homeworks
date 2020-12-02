using System;
using System.Collections.Generic;

namespace Shkadun_Princess
{
    public class Game
    {
        private const string WIN = "Win";
        private const string BOMB = "Bomb";
        private const string ACTIVE = "Active";
        private const string GAME_INFO = "The Princess Game\nHP: ";
        private const string EMPTY_CELL = " ";
        private const string PLAYER_CELL = "Y";
        private const string BOMB_CELL = "X";

        public const int FieldVertical = 10;
        public const int FieldHorizontal = 10;

        string[][] gameField;
        private int countMines = 10;
        private List<Mine> ListMines;

        public int CheckCell(Player player)
        {
            if ((player.PositionHorizontal == FieldHorizontal - 1) && 
                (player.PositionVertical == FieldVertical - 1)) 
            {
                player.GameOver = WIN;

                return 0; 
            }

            else if (gameField[player.PositionVertical][player.PositionHorizontal] == null) 
            { 
                return 0;
            }

            else if (gameField[player.PositionVertical][player.PositionHorizontal] == BOMB)
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
                        else
                        {
                            return 0;
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

        public void DrowMap(Player player)
        {
            Console.Clear();
            Console.WriteLine(GAME_INFO + player.HP); 

            for (int row = 0; row < FieldVertical; row++)
            {  
                for (int column = 0; column < FieldHorizontal; column++)
                {

                    if (row == player.PositionVertical && column == player.PositionHorizontal) 
                    {
                        Console.WriteLine(PLAYER_CELL); 
                    }

                    else if (gameField[row][column] == null) 
                    {
                        Console.WriteLine(EMPTY_CELL);
                    }

                    else if (gameField[row][column] == BOMB) 
                    { 
                        foreach(Mine mine in ListMines)
                        {
                            if(mine.PositionVertical == row && mine.PositionHorizontal == column)
                            {
                                if(mine.Status != ACTIVE)
                                {
                                    Console.WriteLine(BOMB_CELL);
                                }

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
            ListMines = new List<Mine>(new Mine[countMines]);
            gameField = new string[FieldVertical][];

            for (int fieldRow = 0; fieldRow < FieldVertical; fieldRow++)
            {
                gameField[fieldRow] = new string[FieldVertical];
            }
        }
    }
}
