using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Shkadun_Bank
{
    public class CreditCard : Card
    {
        public List<Credit> creditList;

        // Снятие средств
        public override void PullCash()     
        {
            // Если неоплаченных кредитов нет и баланс положителен
            if (!Credit.CheckCreditList(creditList) && Balance > 0) 
            {
                int howManyPull = consoleProvider.HowManyTransfer(ConsoleProvider.pullCash);   

                if (howManyPull <= Balance)
                {
                    Balance -= howManyPull;

                    consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
                }
                else
                {
                    consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);
                }
            }
            else
            {
                consoleProvider.SendMessage(ConsoleProvider.NEGATIVE_CREDIT);
            }
        }

        public override void Transfer(CreditCard card) 
        {
            if (!Credit.CheckCreditList(creditList) && Balance > 0)
            {
                int howManyTransfer = consoleProvider.HowManyTransfer(ConsoleProvider.transferCash, ConsoleProvider.transferOnCard);

                if (howManyTransfer <= Balance)
                {
                    Balance -= howManyTransfer;
                    card.Balance += howManyTransfer;

                    consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
                }
                else
                {
                    consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);
                }
            }
            else
            {
                consoleProvider.SendMessage(ConsoleProvider.NEGATIVE_CREDIT);
            }
        }

        public override void Transfer(string numberAccount, int howManyTransfer)
        {
            if (numberAccount.Length != 20)
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_INPUT);

                return;
            }

            //Разрешаем использовтаь только цифры и буквы
            Regex regex = new Regex(@"\w");
            //Проверяем кол-во совпадений в строке
            MatchCollection match = regex.Matches(numberAccount);   

            if (match.Count != 20)
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_INPUT);
            }
            else 
            {
                if (!Credit.CheckCreditList(creditList) && Balance > 0)
                {
                    if (howManyTransfer <= Balance)
                    {
                        Balance -= howManyTransfer;

                        consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
                    }
                    else
                    {
                        consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);
                    }
                }
                else
                {
                    consoleProvider.SendMessage(ConsoleProvider.NEGATIVE_CREDIT);
                }
            }
        }

        public void AddCredit(int creditSum, int months)
        {
            if (!Credit.CheckCreditList(creditList))
            {
                if (Balance >= 0)
                {
                    Balance += creditSum;

                    creditList.Add(new Credit(creditSum, months));
                    consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
                }
                else
                {
                    consoleProvider.SendMessage(ConsoleProvider.NEGATIVE_CREDIT);
                }
            }
            else
            {
                consoleProvider.SendMessage(ConsoleProvider.NEGATIVE_CREDIT);
            }
        }

        public CreditCard()
        {
            Balance = 0;

            consoleProvider = new ConsoleProvider();
            creditList = new List<Credit>();
            CardNumber = NewRandom.RandomCardNumber();
        }
    }
}
