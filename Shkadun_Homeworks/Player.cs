
namespace Shkadun_Princess
{
    public class Player
    {
        private const int playerDead = 0;
        private const string lose = "LOSE";

        public int HP { get; private set; } = 10;
        public int positionHorizontal { get; private set; } = 0;
        public int positionVertical { get; private set; } = 0;
        public string gameOver { get; set; }

        public void Move(Game game, int horizontal = 0, int vertical = 0)
        {
            if((positionHorizontal + horizontal) >= 0 && 
               (positionHorizontal + horizontal) <= (Game.fieldHorizontal - 1))
            {
                positionHorizontal += horizontal;
            }

            if((positionVertical + vertical) >= 0 &&
               (positionVertical + vertical) <= (Game.fieldVertical - 1))
            {
                positionVertical += vertical;
            }

            int damage = game.CheckCell();
            HP -= damage;                     

            if(HP <= playerDead)
            {
                gameOver = lose;
            }

            game.DrawMap();
        }
    }
}
