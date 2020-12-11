using System.Text.RegularExpressions;

namespace Shkadun_Bank
{
    public class DebetCard : Card
    {
        public override void PullCash()
        {
            int howManyPull = consoleProvider.HowManyTransfer(ConsoleProvider.pullCash);

            if (howManyPull > Balance)
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);
            }
            else
            {
                Balance -= howManyPull;

                consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
            }
        }

        public void Transfer(DebetCard card)
        {
            int howManyTransfer = consoleProvider.HowManyTransfer(ConsoleProvider.transferCash, ConsoleProvider.transferOnCard);

            if (howManyTransfer > Balance)  //Если средств недостаточно
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);
            }
            else
            {
                Balance -= howManyTransfer;
                card.Balance += howManyTransfer;

                consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
            }
        }

        public override void Transfer(string numberAccount, int howManyTransfer)
        {
            if (numberAccount.Length != 20)
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_INPUT);

                return;
            }

            //Разрешаем только цифры и буквы
            Regex regex = new Regex(@"\w");
            //Считаем кол-во совпадений
            MatchCollection match = regex.Matches(numberAccount);   

            if (match.Count != 20)
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_INPUT);
            }
            else
            {
                if (howManyTransfer > Balance)
                {
                    consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);
                }
                else
                {
                    Balance -= howManyTransfer;

                    consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
                }
            }
        }

        public override void Transfer(CreditCard card)  //Перевод на кредитную карту
        {
            int howManyTransfer = consoleProvider.HowManyTransfer(ConsoleProvider.transferCash, ConsoleProvider.transferOnCard);

            if (howManyTransfer > Balance)
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);
            }
            else
            {
                Balance -= howManyTransfer;
                card.Balance += howManyTransfer;

                consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
            }
        }

        public DebetCard()
        {
            Balance = 0;

            CardNumber = NewRandom.RandomCardNumber();
            consoleProvider = new ConsoleProvider();
        }
    }
}
