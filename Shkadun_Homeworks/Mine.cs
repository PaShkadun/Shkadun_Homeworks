
using System;

namespace Shkadun_Princess
{
    public class Mine
    {
        private const string ACTIVE = "Active";
        private const string INACTIVE = "Inctive";

        public string Status { get; private set; }
        public int Damage { get; private set; }
        public int PositionHorizontal { get; private set; }
        public int PositionVertical { get; private set; }

        public void InactiveMine()
        {
            Status = INACTIVE;
        }

        public Mine()
        {
            Random random = new Random();

            int positionBomb = random.Next(1, (Game.FieldVertical * Game.FieldHorizontal) - 2);
            PositionVertical = positionBomb / 10;
            PositionHorizontal = positionBomb % 10;
            Damage = random.Next(1, 10);
            Status = ACTIVE;
        }
    }
}
