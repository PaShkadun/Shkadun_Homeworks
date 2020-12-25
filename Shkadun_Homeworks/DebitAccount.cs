using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public class DebitAccount : Account
    {
        private const int PossibleCountOfCardActions = 3;

        public DebitAccount()
        {
            Cards = new List<Card>();
            Id = CustomRandom.CreateNumberAccount();
            Money = 0;
        }

        public override void DeleteCard()
        {
            int chooseCard = ConsoleProvider.ReadChooseAction(Cards.Count - 1, ConsoleProvider.ChooseCard);

            Money += Cards[chooseCard].Balance;

            Cards.RemoveAt(chooseCard);

            Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
        }

        public override void AddNewCard()
        {
            Cards.Add(new DebetCard());

            Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
        }

        public override void ShowCards()
        {
            if (Cards.Count == 0)
            {
                Bank.ShowMessage(ConsoleProvider.NoneCards);

                return;
            }

            int countCard = 0;

            foreach (DebetCard card in Cards)
            {
                Console.WriteLine(countCard++ + " " + card.CardNumber);
            }
        }

        public override void ManageAccount()
        {
            switch (ConsoleProvider.ReadChooseAction(PossibleCountOfCardActions, ConsoleProvider.ActionsDebitAccount))
            {
                case (int)DebitAccountOperation.AddNewCard:
                    AddNewCard();
                    break;

                case (int)DebitAccountOperation.DeleteCard:
                    ShowCards();

                    if (Cards.Count != 0)
                    {
                        DeleteCard();
                    }

                    break;

                case (int)DebitAccountOperation.ChooseOperation:
                    ShowCards();

                    if (Cards.Count != 0)
                    {
                        Cards[ConsoleProvider.ReadChooseAction(Cards.Count - 1, ConsoleProvider.ChooseCard)].ChooseOperation();
                    }

                    break;

                case (int)DebitAccountOperation.AddCashOnCart:
                    AddCashOnCart();
                    break;
            }
        }
    }
}
