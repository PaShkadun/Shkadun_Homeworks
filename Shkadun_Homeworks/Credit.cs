using System.Collections.Generic;

namespace Shkadun_Bank
{
    public class Credit
    {
        public int CreditRate { get; private set; }
        public int Months { get; set; }
        public int MonthsOfDebt { get; set; }
        public int Sum { get; set; }

        // Проверка кредитов по картам(вызывается при попытке перевода, попытке взять кредит)
        public static bool CheckCreditList(List<Credit> creditList)
        {
            bool negativeCredit = false;

            foreach (Credit credit in creditList)
            {
                if (credit.MonthsOfDebt > 0) 
                { 
                    negativeCredit = true; 
                    break; 
                } 
                // Если нашло непогашенный, преркащает искать
            }

            return negativeCredit;
        }

        public Credit(int creditRate, int months)
        {
            CreditRate = (120 * creditRate / 100) / months;
            Months = months;
            MonthsOfDebt = 0;
        }
    }
}
