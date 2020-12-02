
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

            PositionVertical = random.Next(0, Game.FieldVertical);
            PositionHorizontal = random.Next(Game.FieldHorizontal);
            Damage = random.Next(1, 10);
            Status = ACTIVE;
        }
    }
}
