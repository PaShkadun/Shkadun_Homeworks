using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shkadun_Bank
{
    public class Credit
    {
        private const int PercentCreditRate = 20;
        private const int Percents = 100;

        public string Id { get; }

        public int Monthes { get; set; }

        public readonly int CreditSum;
        public int MonthesDebt;

        public Credit(int monthes, int sum)
        {
            MonthesDebt = 0;
            Monthes = monthes;
            CreditSum = ((sum * (Percents + PercentCreditRate)) / Percents) / monthes;
            Id = Guid.NewGuid().ToString();

            CreateTimer();
        }

        public void PayCredit()
        {
            Monthes -= MonthesDebt;
            MonthesDebt = 0;
        }

        public void CreateTimer()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                while (Monthes > 0)
                {
                    Thread.Sleep(1000);
                    MonthesDebt++;
                    Monthes--;

                    ConsoleProvider.ShowMessage(ConsoleProvider.CreditCharge + Id);
                }
            });
        }
    }
}