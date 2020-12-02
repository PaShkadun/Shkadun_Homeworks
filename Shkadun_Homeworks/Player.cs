
namespace Shkadun_Princess
{
    public class Player
    {
        private const int PlayerDead = 0;
        private const string Lose = "LOSE";

        public int HP { get; private set; } = 10;
        public int PositionHorizontal { get; private set; } = 0;
        public int PositionVertical { get; private set; } = 0;
        public string GameOver { get; set; }

        public void Move(Game game, int horizontal = 0, int vertical = 0)
        {
            if((PositionHorizontal + horizontal) >= 0 && 
               (PositionHorizontal + horizontal) <= (Game.FieldHorizontal - 1))
            {
                PositionHorizontal += horizontal;
            }

            if((PositionVertical + vertical) >= 0 &&
               (PositionVertical + vertical) <= (Game.FieldVertical - 1))
            {
                PositionVertical += vertical;
            }

            int damage = game.CheckCell(this);
            HP -= damage;                     

            if(HP <= PlayerDead)
            {
                GameOver = Lose;
            }

            game.DrowMap(this);
        }
    }
}
