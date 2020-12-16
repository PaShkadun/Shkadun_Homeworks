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

        public List<Credit> Credits;

        public CreditCard()
        {
            Credits = new List<Credit>();
            Balance = 0;
            Type = TypeCard.Credit;
            CardNumber = CustomRandom.RandomCardNumber();
        }

        public void ChargeCredit()
        {
            if (Credits.Count != 0)
            {
                int numberCredit = 0;

                foreach (Credit credit in Credits)
                {
                    if (credit.Monthes != 0)
                    {
                        credit.MonthesDebt++;
                        credit.Monthes--;

                        Bank.ShowMessage(chargeCredit + credit.CreditSum + chargeNumberCredit + numberCredit++);
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

                        if (Credits.Count != 0)
                        {
                            PayCredit(Credits[ConsoleProvider.ReadChoose(Credits.Count - 1)]);
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
                Bank.ShowMessage(addCreditInfo);
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