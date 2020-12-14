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
            CardNumber = NewRandom.RandomCardNumber();
        }

        public void ChargeCredit()
        {
            if(credits.Count != 0)
            {
                int numberCredit = 0;

                foreach(var credit in credits)
                {
                    if (credit.Monthes != 0)
                    {
                        credit.monthesDebt++;
                        credit.Monthes--;

                        Bank.showMessage(chargeCredit + credit.creditSum + chargeNumberCredit + numberCredit++);
                    }
                }
            }
        }

        public override void ChooseOperation()
        {
            Bank.showMessage(ConsoleProvider.operationsCreditCard);

            switch(ConsoleProvider.ReadChoose(possibleCountActions - 1))
            {
                case 1:
                    Bank.ShowAccounts();
                    TransferToCard();
                    break;

                case 2:
                    string numberAccount = ConsoleProvider.InputNumberAccount();

                    if (numberAccount != ConsoleProvider.incorrectInput)
                    {
                        TransferToAccount(
                                numberAccount,
                                ConsoleProvider.InputValue()
                            );
                    }
                    break;

                case 3:
                    AddCredit();
                    break;

                case 4:
                    if(CheckDebtCredits())
                    {
                        Bank.showMessage(ConsoleProvider.haveNotNegativeCredit);
                    }
                    else
                    {
                        ShowAllCredit();

                        if(credits.Count != 0)
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

        public override void TransferToCard()
        {
            Bank.ShowAccounts();

            int chooseAccount = ConsoleProvider.ReadChoose(Bank.accounts.Count - 1);

            if (Bank.accounts[chooseAccount].cards.Count == 0)
            {
                Bank.showMessage(ConsoleProvider.noneCardOnAccount);

                return;
            }

            Bank.accounts[chooseAccount].Showcards();

            int chooseCard = ConsoleProvider.ReadChoose(Bank.accounts[chooseAccount].cards.Count);
            Card card = Bank.accounts[chooseAccount].cards[chooseCard];

            if (card.Type == TypeCard.Debit)
            {
                Bank.showMessage(ConsoleProvider.incorrectOperation);
            }
            else
            {
                int money = ConsoleProvider.InputValue();
                
                if(money > card.Balance)
                {
                    Bank.showMessage(ConsoleProvider.lackingMoney);
                }
                else
                {
                    if(card == this)
                    {
                        Bank.showMessage(ConsoleProvider.incorrectOperation);
                    }

                    card.Balance += money;
                    Balance -= money;

                    Bank.showMessage(ConsoleProvider.successfullyOperation);
                }
            }
        }

        public void PayCredit(Credit credit)
        {
            if(credit.creditSum > Balance)
            {
                Bank.showMessage(ConsoleProvider.lackingMoney);
            }
            else
            {
                Balance -= credit.creditSum;
                credit.monthesDebt = 0;

                if(credit.monthesDebt == 0 && credit.Monthes == 0)
                {
                    Bank.showMessage(ConsoleProvider.creditPaid);

                    for(int numberCredit = 0; numberCredit < credits.Count; numberCredit++)
                    {
                        if(credit == credits[numberCredit])
                        {
                            credits.RemoveAt(numberCredit);
                            break;
                        }
                    }
                }
                else
                {
                    Bank.showMessage(ConsoleProvider.successfullyOperation);
                }
            }
        }

        public void AddCredit()
        {
            if (CheckDebtCredits())
            {
                Bank.showMessage(addCreditInfo);
                credits.Add(new Credit(ConsoleProvider.InputValue(), ConsoleProvider.InputValue()));
                Bank.showMessage(ConsoleProvider.successfullyOperation);
            }
            else
            {
                Bank.showMessage(ConsoleProvider.haveNegativeCredit);
            }
        }

        public void ShowAllCredit()
        {
            if(credits.Count == 0)
            {
                Bank.showMessage(ConsoleProvider.haveNotCredit);
            }
            else
            {
                int countCredit = 0;

                foreach(var credit in credits)
                {
                    Console.WriteLine($"{countCredit++}. {credit.creditSum * credit.monthesDebt}");
                }
            }
        }

        public bool CheckDebtCredits()
        {
            if(credits.Count == 0)
            {
                return true;
            }
            else
            {
                bool haveDebt = false;

                foreach(var credit in credits)
                {
                    if(credit.monthesDebt > 0)
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