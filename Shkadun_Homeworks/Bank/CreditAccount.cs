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

                int monthes = ConsoleProvider.InputIntegerValue();
                int money = ConsoleProvider.InputIntegerValue();

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
                foreach (Credit credit in Credits)
                {
                    Console.WriteLine($"{credit.Id} {credit.CreditSum * credit.MonthesDebt}");
                }

                int chooseCredit = ConsoleProvider.ReadChooseAction(Credits.Count - 1, ConsoleProvider.InputValue);

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
                case (int)CreditAccountOperation.AddNewCard:
                    AddNewCard();
                    break;

                case (int)CreditAccountOperation.DeleteCard:
                    ShowCards();

                    if (Cards.Count != 0)
                    {
                        DeleteCard();
                    }

                    break;

                case (int)CreditAccountOperation.ChooseOperation:
                    ShowCards();

                    if (Cards.Count != 0)
                    {
                        Cards[ConsoleProvider.ReadChooseAction(Cards.Count - 1, ConsoleProvider.ChooseCard)].ChooseOperation();
                    }

                    break;

                case (int)CreditAccountOperation.AddCashOnCart:
                    AddCashOnCart();
                    break;

                case (int)CreditAccountOperation.AddCredit:
                    AddCredit();
                    break;

                case (int)CreditAccountOperation.PayCredit:
                    PayCredit();
                    break;
            }
        }
    }
}
