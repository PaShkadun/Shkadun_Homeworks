
namespace Shkadun_Princess
{
    public class Player
    {
        private const int PLAYER_DEAD = 0;
        public int HP { get; private set; }
        public int PositionHorizontal { get; private set; }
        public int PositionVertical { get; private set; }
        public string GameOver { get; set; }

        public void Move(Game game, int horizontal = 0, int vertical = 0)
        {
            // Если юзер находится на верхней ячейке
            if((PositionHorizontal + horizontal) >= 0 && 
               (PositionHorizontal + horizontal) <= (game.FieldHorizontal - 1))
            {
                PositionHorizontal += horizontal;
            }

            if((PositionVertical + vertical) >= 0 &&
               (PositionVertical + vertical) <= (game.FieldVertical - 1))
            {
                PositionVertical += vertical;
            }

            // Проверяет, стал ли юзер на мину. 
            int damage = game.CheckBomb(this);
            // Если нет снимает 0 HP
            HP -= damage;                     

            if(HP <= PLAYER_DEAD)
            {
                GameOver = "Lose";
            }

            game.DrowMap(this);
        }

        public Player()
        {
            HP = 10;
            PositionHorizontal = 0;
            PositionVertical = 0;
        }
    }
}
