using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public class CreditAccount : Account
    {
        private const int PossibleCountOfCardActions = 6;

        public List<Credit> Credits;

        public CreditAccount()
        {
            Cards = new List<Card>();
            Credits = new List<Credit>();
            Id = CustomRandom.CreateNumberAccount();
            Money = 0;
        }

        public void CheckCards()
        {
            foreach (CreditCard card in Cards)
            {
                card.ChargeCredit();
            }
        }

        public override void AddNewCard()
        {
            Cards.Add(new CreditCard());

            Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
        }

        public override void DeleteCard()
        {
            int chooseCard = ConsoleProvider.ReadChooseAction(Cards.Count - 1, ConsoleProvider.ChooseCard);

            if (((CreditCard)Cards[chooseCard]).CheckCredits())
            {
                Money += Cards[chooseCard].Balance;

                Cards.RemoveAt(chooseCard);
                Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
            else
            {
                Bank.ShowMessage(ConsoleProvider.HaveCredit);
            }
        }

        public override void ShowCards()
        {
            if (Cards.Count == 0)
            {
                Bank.ShowMessage(ConsoleProvider.NoneCards);

                return;
            }

            int countCard = 0;

            foreach (CreditCard card in Cards)
            {
                Console.WriteLine(countCard++ + " " + card.CardNumber);
            }
        }

        public bool CheckCredits()
        {
            bool negativeCredit = false;

            foreach (Credit credit in Credits)
            {
                if (credit.MonthesDebt != 0)
                {
                    negativeCredit = true;
                }
            }

            return negativeCredit;
        }

        public void AddCredit()
        {
            if (Credits.Count == 0 || !CheckCredits())
            {
                Bank.ShowMessage(ConsoleProvider.AddCreditInfo);

                int money = ConsoleProvider.InputIntegerValue();
                int monthes = ConsoleProvider.InputIntegerValue();

                Credits.Add(new Credit(monthes, money));
                Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
            else
            {
                Bank.ShowMessage(ConsoleProvider.HaveNegativeCredit);
            }
        }

        public void PayCredit()
        {
            if (Credits.Count == 0 || !CheckCredits())
            {
                Bank.ShowMessage(ConsoleProvider.HaveNotCredit);
            }
            else
            {
                var countCredit = 0;

                foreach (Credit credit in Credits)
                {
                    Console.WriteLine($"{countCredit++} {credit.CreditSum}");
                }

                int chooseCredit = ConsoleProvider.ReadChooseAction(countCredit - 1, ConsoleProvider.InputValue);

                if (Money < (Credits[chooseCredit].MonthesDebt * Credits[chooseCredit].CreditSum))
                {
                    Bank.ShowMessage(ConsoleProvider.LackingMoney);
                }
                else
                {
                    Money -= (Credits[chooseCredit].MonthesDebt * Credits[chooseCredit].CreditSum);
                    Credits[chooseCredit].PayCredit();
                }
            }
        }

        public override void ManageAccount()
        {
            switch (ConsoleProvider.ReadChooseAction(PossibleCountOfCardActions, ConsoleProvider.ActionsCreditAccount))
            {
                case 1:
                    AddNewCard();
                    break;

                case 2:
                    ShowCards();

                    if (Cards.Count != 0)
                    {
                        DeleteCard();
                    }

                    break;

                case 3:
                    ShowCards();

                    if (Cards.Count != 0)
                    {
                        Cards[ConsoleProvider.ReadChooseAction(Cards.Count - 1, ConsoleProvider.ChooseCard)].ChooseOperation();
                    }

                    break;

                case 4:
                    AddCashOnCart();
                    break;

                case 5:
                    AddCredit();
                    break;

                case 6:
                    PayCredit();
                    break;
            }
        }
    }
}
