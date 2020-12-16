using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public class CreditCard : Card
    {
        private const string addCreditInfo = "Input credit info(monthes, sum)";
        private const string chargeCredit = "Charge sum ";
        private const string chargeNumberCredit = " | credit number #";

        private const int possibleCountActions = 6;

        public override TypeCard Type { get; }

        public List<Credit> credits;

        public CreditCard()
        {
            credits = new List<Credit>();
            Balance = 0;
            Type = TypeCard.Credit;
            CardNumber = CustomRandom.RandomCardNumber();
        }

        public void ChargeCredit()
        {
            if (credits.Count != 0)
            {
                int numberCredit = 0;

                foreach (Credit credit in credits)
                {
                    if (credit.Monthes != 0)
                    {
                        credit.monthesDebt++;
                        credit.Monthes--;

                        Bank.ShowMessage(chargeCredit + credit.creditSum + chargeNumberCredit + numberCredit++);
                    }
                }
            }
        }

        public override void ChooseOperation()
        {
            Bank.ShowMessage(ConsoleProvider.OperationsCreditCard);

            switch (ConsoleProvider.ReadChoose(possibleCountActions - 1))
            {
                case 1:
                    Bank.ShowAccounts();
                    TransferMoneyToCard();
                    break;

                case 2:
                    string numberAccount = ConsoleProvider.InputStringValue(ConsoleProvider.InputRecepientAccounts);

                    if (numberAccount != ConsoleProvider.IncorrectInput)
                    {
                        TransferMoneyToAccount(numberAccount, ConsoleProvider.InputIntegerValue());
                    }
                    break;

                case 3:
                    AddCredit();
                    break;

                case 4:
                    if (CheckDebtCredits())
                    {
                        Bank.ShowMessage(ConsoleProvider.HaveNotNegativeCredit);
                    }
                    else
                    {
                        ShowAllCredit();

                        if (credits.Count != 0)
                        {
                            PayCredit(credits[ConsoleProvider.ReadChoose(credits.Count - 1)]);
                        }
                    }
                    break;

                case 5:
                    ShowAllCredit();
                    break;

                case 6:
                    SpendMoney();
                    break;
            }
        }

        public override void TransferMoneyToCard()
        {
            Bank.ShowAccounts();

            int chooseAccount = ConsoleProvider.ReadChoose(Bank.Accounts.Count - 1);

            if (Bank.Accounts[chooseAccount].Cards.Count == 0)
            {
                Bank.ShowMessage(ConsoleProvider.NoneCardOnAccount);

                return;
            }

            Bank.Accounts[chooseAccount].ShowCards();

            int chooseCard = ConsoleProvider.ReadChoose(Bank.Accounts[chooseAccount].Cards.Count);
            Card card = Bank.Accounts[chooseAccount].Cards[chooseCard];

            if (card.Type == TypeCard.Debit)
            {
                Bank.ShowMessage(ConsoleProvider.IncorrectOperation);
            }
            else
            {
                int money = ConsoleProvider.InputIntegerValue();
                
                if (money > card.Balance)
                {
                    Bank.ShowMessage(ConsoleProvider.LackingMoney);
                }
                else
                {
                    if (card == this)
                    {
                        Bank.ShowMessage(ConsoleProvider.IncorrectOperation);
                    }

                    card.Balance += money;
                    Balance -= money;

                    Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
                }
            }
        }

        public void PayCredit(Credit credit)
        {
            if (credit.creditSum > Balance)
            {
                Bank.ShowMessage(ConsoleProvider.LackingMoney);
            }
            else
            {
                Balance -= credit.creditSum;
                credit.monthesDebt = 0;

                if (credit.monthesDebt == 0 && credit.Monthes == 0)
                {
                    Bank.ShowMessage(ConsoleProvider.CreditPaid);

                    for (var numberCredit = 0; numberCredit < credits.Count; numberCredit++)
                    {
                        if (credit == credits[numberCredit])
                        {
                            credits.RemoveAt(numberCredit);
                            break;
                        }
                    }
                }
                else
                {
                    Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
                }
            }
        }

        public void AddCredit()
        {
            if (CheckDebtCredits())
            {
                Bank.ShowMessage(addCreditInfo);
                credits.Add(new Credit(ConsoleProvider.InputIntegerValue(), ConsoleProvider.InputIntegerValue()));
                Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
            else
            {
                Bank.ShowMessage(ConsoleProvider.HaveNegativeCredit);
            }
        }

        public void ShowAllCredit()
        {
            if (credits.Count == 0)
            {
                Bank.ShowMessage(ConsoleProvider.HaveNotCredit);
            }
            else
            {
                int countCredit = 0;

                foreach (Credit credit in credits)
                {
                    Console.WriteLine($"{countCredit++}. {credit.creditSum * credit.monthesDebt}");
                }
            }
        }

        public bool CheckDebtCredits()
        {
            if (credits.Count == 0)
            {
                return true;
            }
            else
            {
                bool haveDebt = false;

                foreach (Credit credit in credits)
                {
                    if (credit.monthesDebt > 0)
                    {
                        haveDebt = true;
                        break;
                    }
                }

                return !haveDebt;
            }
        }

        public bool CheckCredits()
        {
            if (credits.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}