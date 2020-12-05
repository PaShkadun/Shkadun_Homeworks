using System;

namespace Shkadun_Princess
{
    public class Game
    {
        private const string win = "Win";
        private const string bomb = "Bomb";
        private const string gameInfo = "The Princess Game\nHP: ";
        private const string emptyCell = " ";
        private const string playerCell = "Y";
        private const string bombCell = "X";
        private const string splitCell = "|";

        public const int fieldVertical = 10;
        public const int fieldHorizontal = 10;
        private const int nullDamage = 0;

        private string[][] gameField;
        private int countMines = 10;
        private Mine[] listMines;
        public Player player;

        public int CheckCell()
        {
            int damage = nullDamage;

            if ((player.positionHorizontal == fieldHorizontal - 1) && 
                (player.positionVertical == fieldVertical - 1)) 
            {
                player.gameOver = win;
                damage = nullDamage;
            }
            else if (gameField[player.positionVertical][player.positionHorizontal] == null) 
            {
                damage = nullDamage;
            }
            else if (gameField[player.positionVertical][player.positionHorizontal] == bomb)
            {
                foreach(Mine mine in listMines)
                {
                    if ((mine.PositionHorizontal == player.positionHorizontal) &&
                        (mine.PositionVertical == player.positionVertical))
                    {
                        if(mine.Status == StatusBomb.Active)
                        {
                            mine.InactiveMine();
                            damage = mine.Damage;
                        }
                        else
                        {
                            damage = nullDamage;
                        }
                    }
                }
            }
            else 
            {
                damage = nullDamage;
            }

            return damage;
        }

        public void DrowMap()
        {
            Console.Clear();

            for (int row = 0; row < fieldVertical; row++)
            {  
                for (int column = 0; column < fieldHorizontal; column++)
                {
                    if (row == player.positionVertical && column == player.positionHorizontal) 
                    {
                        Console.Write(playerCell); 
                    }
                    else if (gameField[row][column] == null) 
                    {
                        Console.Write(emptyCell);
                    }
                    else if (gameField[row][column] == bomb) 
                    { 
                        foreach(Mine mine in listMines)
                        {
                            if(mine.PositionVertical == row && mine.PositionHorizontal == column)
                            {
                                if(mine.Status != StatusBomb.Inactive)
                                {
                                    Console.Write(bombCell);
                                }
                                else
                                {
                                    Console.Write(emptyCell);
                                }

                                break;
                            }
                        }
                    }

                    Console.Write(splitCell);
                }

                Console.WriteLine();
            }

            Console.WriteLine(gameInfo + player.HP);
        }

        public Game()
        {
            player = new Player();
            gameField = new string[fieldVertical][];

            for (int fieldRow = 0; fieldRow < fieldVertical; fieldRow++)
            {
                gameField[fieldRow] = new string[fieldVertical];
            }

            listMines = new Mine[countMines];

            for(int bombs = 0; bombs < countMines; bombs++)
            {
                listMines[bombs] = new Mine();
            }

            foreach(Mine mine in listMines)
            {
                gameField[mine.PositionVertical][mine.PositionHorizontal] = bomb;
            }
        }
    }
}
