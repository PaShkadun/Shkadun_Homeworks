using System;
using System.Collections.Generic;
using System.Text;

namespace Shkadun_Bank
{
    public class DebitAccount : Account
    {
        private const int PossibleCountOfCardActions = 3;

        public DebitAccount(TypeCardOrAccount type)
        {
            Cards = new List<Card>();
            Id = CustomRandom.CreateNumberAccount();
            Type = type;
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
                    ADdCashOnCart();
                    break;
            }
        }
    }
}
