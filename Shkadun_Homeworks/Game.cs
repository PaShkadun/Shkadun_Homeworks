using System;
using System.Collections.Generic;

namespace Shkadun_Princess
{
    public class Game
    {
        private const string Win = "Win";
        private const string Bomb = "Bomb";
        private const string Active = "Active";
        private const string GameInfo = "The Princess Game\nHP: ";
        private const string EmptyCell = " ";
        private const string PlayerCell = "Y";
        private const string BombCell = "X";
        private const string SplitCell = "|";

        public const int FieldVertical = 10;
        public const int FieldHorizontal = 10;

        string[][] gameField;
        private int countMines = 10;
        private List<Mine> ListMines;

        public int CheckCell(Player player)
        {
            int damage = 0;

            if ((player.PositionHorizontal == FieldHorizontal - 1) && 
                (player.PositionVertical == FieldVertical - 1)) 
            {
                player.GameOver = Win;
                damage = 0;
            }
            else if (gameField[player.PositionVertical][player.PositionHorizontal] == null) 
            {
                damage = 0;
            }
            else if (gameField[player.PositionVertical][player.PositionHorizontal] == Bomb)
            {
                foreach(Mine mine in ListMines)
                {
                    if ((mine.PositionHorizontal == player.PositionHorizontal) &&
                        (mine.PositionVertical == player.PositionVertical))
                    {
                        if(mine.Status == Active)
                        {
                            mine.InactiveMine();
                            damage = mine.Damage;
                        }
                        else
                        {
                            damage = 0;
                        }
                    }
                }
            }
            else 
            {
                damage = 0;
            }

            return damage;
        }

        public void DrowMap(Player player)
        {
            Console.Clear();

            for (int row = 0; row < FieldVertical; row++)
            {  
                for (int column = 0; column < FieldHorizontal; column++)
                {

                    if (row == player.PositionVertical && column == player.PositionHorizontal) 
                    {
                        Console.Write(PlayerCell); 
                    }

                    else if (gameField[row][column] == null) 
                    {
                        Console.Write(EmptyCell);
                    }

                    else if (gameField[row][column] == Bomb) 
                    { 
                        foreach(Mine mine in ListMines)
                        {
                            if(mine.PositionVertical == row && mine.PositionHorizontal == column)
                            {
                                if(mine.Status != Active)
                                {
                                    Console.Write(BombCell);
                                }
                                else
                                {
                                    Console.Write(EmptyCell);
                                }

                                break;
                            }
                        }
                    }

                    Console.Write(SplitCell);
                }

                Console.WriteLine();
            }

            Console.WriteLine(GameInfo + player.HP);
        }

        public Game()
        {
            gameField = new string[FieldVertical][];

            for (int fieldRow = 0; fieldRow < FieldVertical; fieldRow++)
            {
                gameField[fieldRow] = new string[FieldVertical];
            }

            ListMines = new List<Mine>();

            for(int bombs = 0; bombs < countMines; bombs++)
            {
                ListMines.Add(new Mine());
            }

            foreach(Mine mine in ListMines)
            {
                gameField[mine.PositionVertical][mine.PositionHorizontal] = Bomb;
            }
        }
    }
}
