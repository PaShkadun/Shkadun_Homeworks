
using System;

namespace Shkadun_Princess
{
    public enum StatusBomb
    {
        Active = 0,
        Inactive
    }

    public class Mine
    {
        public StatusBomb Status { get; private set; }
        public int Damage { get; private set; }
        public int PositionHorizontal { get; private set; }
        public int PositionVertical { get; private set; }

        public void InactiveMine()
        {
            Status = StatusBomb.Inactive;
        }

        public Mine()
        {
            Random random = new Random();

            PositionVertical = random.Next(0, Game.fieldVertical);
            PositionHorizontal = random.Next(0, Game.fieldHorizontal);
            Damage = random.Next(1, 10);
            Status = StatusBomb.Active;
        }
    }
}
