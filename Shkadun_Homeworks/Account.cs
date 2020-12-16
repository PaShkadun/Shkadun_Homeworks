using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public class Account
    {
        private const int possibleCountActionsCard = 3;
        private const int typesOfCard = 2;
        public const int lengthNumberAccount = 20;

        public string Id { get; }
        public int Money { get; set; }

        public List<Card> Cards;

        public Account()
        {
            Cards = new List<Card>();
            Id = CustomRandom.CreateNumberAccount();

            Money = 0;
        }

        public void ShowCards()
        {
            if (Cards.Count == 0)
            {
                Bank.ShowMessage(ConsoleProvider.NoneCards);

                return;
            }

            int countCard = 0;

            foreach (Card card in Cards)
            {
                Console.WriteLine(countCard++ + " " + card.CardNumber);
            }
        }

        public void DeleteCard()
        {
            int chooseCard = ConsoleProvider.ReadChoose(Cards.Count - 1, ConsoleProvider.ChooseCard);

            if (Cards[chooseCard].Type != TypeCard.Credit)
            {
                Money += Cards[chooseCard].Balance;

                Cards.RemoveAt(chooseCard);
                Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
            else
            {
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
        }

        public void ManageCards()
        {
            switch (ConsoleProvider.ChooseActions(ConsoleProvider.ActionsCards, possibleCountActionsCard))
            {
                case 1:
                    Bank.ShowMessage(ConsoleProvider.TypesCard);

                    if (ConsoleProvider.ReadChoose(typesOfCard) == (int)TypeCard.Credit)
                    {
                        Cards.Add(new CreditCard());
                    }
                    else
                    {
                        Cards.Add(new DebetCard());
                    }

                    Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
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
                        Cards[ConsoleProvider.ReadChoose(Cards.Count, ConsoleProvider.ChooseCard)].ChooseOperation();
                    }
                    break;
            }
        }
    }
}