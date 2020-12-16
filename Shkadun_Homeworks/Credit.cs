namespace Shkadun_Bank
{
    public class Credit
    {
        public const int CreditRate = 20;
        private const int percents = 100;

        public int Monthes { get; set; }

        public readonly int creditSum;
        public int monthesDebt;

        public Credit(int monthes, int sum)
        {
            monthesDebt = 0;
            Monthes = monthes;
            creditSum = ((sum * (percents + CreditRate)) / percents);
        }

        public void PayCredit()
        {
            Monthes -= monthesDebt;
            monthesDebt = 0;
        }
    }
}