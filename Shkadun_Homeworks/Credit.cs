namespace Shkadun_Bank
{
    public class Credit
    {
        public const int PercentCreditRate = 20;
        private const int percents = 100;

        public int Monthes { get; set; }

        public readonly int CreditSum;
        public int MonthesDebt;

        public Credit(int monthes, int sum)
        {
            MonthesDebt = 0;
            Monthes = monthes;
            CreditSum = ((sum * (percents + PercentCreditRate)) / percents);
        }

        public void PayCredit()
        {
            Monthes -= MonthesDebt;
            MonthesDebt = 0;
        }
    }
}