
namespace Shkadun_Princess
{
    public class Mine
    {
        public string Status { get; private set; }
        public int Damage { get; private set; }
        public int PositionHorizontal { get; private set; }
        public int PositionVertical { get; private set; }

        public void InactiveMine()
        {
            Status = "Inactive";
        }

        public Mine(int damage, int positionVertical, int positionHorizontal)
        {
            Status = "Active";
            Damage = damage;
            PositionHorizontal = positionHorizontal;
            PositionVertical = positionVertical;
        }
    }
}
