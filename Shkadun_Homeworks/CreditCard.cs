using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public class CreditCard : Card
    {
        private const int PossibleCountOfCreditCardActions = 7;

        public List<Credit> Credits;

        public CreditCard()
        {
            Credits = new List<Credit>();
            Balance = 0;
            CardNumber = CustomRandom.RandomCardNumber();
        }

        public void ChargeCredit()
        {
            if (Credits.Count != 0)
            {
                var numberCredit = 0;

                foreach (Credit credit in Credits)
                {
                    if (credit.Monthes != 0)
                    {
                        credit.MonthesDebt++;
                        credit.Monthes--;

                        Bank.ShowMessage(ConsoleProvider.ChargeCredit + credit.CreditSum + ConsoleProvider.ChargeNumberCredit + numberCredit++);
                    }
                }
            }
        }

        public override void ChooseOperation()
        {
            switch (ConsoleProvider.ReadChooseAction(PossibleCountOfCreditCardActions - 1, ConsoleProvider.OperationsCreditCard))
            {
                case 1:
                    TransferMoneyToCard();
                    break;

                case 2:
                    string numberAccount = ConsoleProvider.InputStringValue(ConsoleProvider.InputRecepientAccounts);

                    if (numberAccount.Length != 0)
                    {
                        TransferMoneyToAccount(numberAccount, ConsoleProvider.InputIntegerValue());
                    }
                    else
                    {
                        Bank.ShowMessage(ConsoleProvider.IncorrectInput);
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

                        if (Credits.Count != 0)
                        {
                            PayCredit(Credits[ConsoleProvider.ReadChooseAction(Credits.Count - 1)]);
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

            int chooseAccount = ConsoleProvider.ReadChooseAction(Bank.Accounts.Count - 1);

            if (Bank.Accounts[chooseAccount].Cards.Count == 0)
            {
                Bank.ShowMessage(ConsoleProvider.NoneCardOnAccount);

                return;
            }

            Bank.Accounts[chooseAccount].ShowCards();

            int chooseCard = ConsoleProvider.ReadChooseAction(Bank.Accounts[chooseAccount].Cards.Count);
            Card card = Bank.Accounts[chooseAccount].Cards[chooseCard];

            if (card as DebetCard != null)
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

                        return;
                    }

                    card.Balance += money;
                    Balance -= money;

                    Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
                }
            }
        }

        public void PayCredit(Credit credit)
        {
            if (credit.CreditSum > Balance)
            {
                Bank.ShowMessage(ConsoleProvider.LackingMoney);
            }
            else
            {
                Balance -= credit.CreditSum;
                credit.MonthesDebt = 0;

                if (credit.MonthesDebt == 0 && credit.Monthes == 0)
                {
                    Bank.ShowMessage(ConsoleProvider.CreditPaid);

                    for (var numberCredit = 0; numberCredit < Credits.Count; numberCredit++)
                    {
                        if (credit == Credits[numberCredit])
                        {
                            Credits.RemoveAt(numberCredit);
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
                Bank.ShowMessage(ConsoleProvider.AddCreditInfo);
                Credits.Add(new Credit(ConsoleProvider.InputIntegerValue(), ConsoleProvider.InputIntegerValue()));
                Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
            else
            {
                Bank.ShowMessage(ConsoleProvider.HaveNegativeCredit);
            }
        }

        public void ShowAllCredit()
        {
            if (Credits.Count == 0)
            {
                Bank.ShowMessage(ConsoleProvider.HaveNotCredit);
            }
            else
            {
                int countCredit = 0;

                foreach (Credit credit in Credits)
                {
                    Console.WriteLine($"{countCredit++}. {credit.CreditSum * credit.MonthesDebt}");
                }
            }
        }

        public bool CheckDebtCredits()
        {
            if (Credits.Count == 0)
            {
                return true;
            }
            else
            {
                bool haveDebt = false;

                foreach (Credit credit in Credits)
                {
                    if (credit.MonthesDebt > 0)
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
            return Credits.Count == 0 ? true : false;
        }
    }
}