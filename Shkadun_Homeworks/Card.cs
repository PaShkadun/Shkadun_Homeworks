using System.Text.RegularExpressions;

namespace Shkadun_Bank
{
    public abstract class Card
    {
        public const int lengthNumberCard = 16;

        public int Balance { get; set; }
        public string CardNumber { get; protected set; }
        abstract public TypeCard Type { get; }

        public abstract void TransferMoneyToCard();

        public void TransferMoneyToAccount(string numberAccount, int sum)
        {
            Regex regex = new Regex(@"\w");
            MatchCollection matchCollection = regex.Matches(numberAccount);

            if (matchCollection.Count == Account.lengthNumberAccount)
            {
                if (sum > Balance)
                {
                    Bank.ShowMessage(ConsoleProvider.LackingMoney);
                }
                else
                {
                    Balance -= sum;

                    Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
                }
            }
            else
            {
                Bank.ShowMessage(ConsoleProvider.IncorrectInput);
            }
        }

        public void SpendMoney()
        {
            int money = ConsoleProvider.InputIntegerValue();

            if (money > Balance)
            {
                Bank.ShowMessage(ConsoleProvider.LackingMoney);
            }
            else
            {
                Balance -= money;
                Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
        }

        public abstract void ChooseOperation();
    }
}