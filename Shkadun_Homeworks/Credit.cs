namespace Shkadun_Bank
{
    public class Credit
    {
        private const int PercentCreditRate = 20;
        private const int Percents = 100;

        public int Monthes { get; set; }

        public readonly int CreditSum;
        public int MonthesDebt;

        public Credit(int monthes, int sum)
        {
            MonthesDebt = 0;
            Monthes = monthes;
            CreditSum = ((sum * (Percents + PercentCreditRate)) / Percents) / monthes;
        }

        public void PayCredit()
        {
            Monthes -= MonthesDebt;
            MonthesDebt = 0;
        }
    }
}