using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public class Account
    {
        private const string chooseCard = "Choose card";
        private const string noneCards = "You haven't cards";

        public string ID { get; }
        public int Money { get; set; }

        public List<Card> cards;

        public Account()
        {
            cards = new List<Card>();
            ID = NewRandom.CreateNumberAccount();

            Money = 0;
        }

        public void Showcards()
        {
            if(cards.Count == 0)
            {
                Bank.showMessage(noneCards);

                return;
            }

            int countCard = 0;

            foreach(var card in cards)
            {
                Console.WriteLine(countCard++ + " " + card.CardNumber);
            }
        }

        public void DeleteCard()
        {
            int choose = ConsoleProvider.ReadChoose(cards.Count - 1, chooseCard);

            if (cards[choose].Type != TypeCard.Credit)
            {
                Money += cards[choose].Balance;

                cards.RemoveAt(choose);
                Bank.showMessage(ConsoleProvider.successfullyOperation);
            }
            else
            {
                if (((CreditCard)cards[choose]).CheckCredits())
                {
                    Money += cards[choose].Balance;

                    cards.RemoveAt(choose);
                    Bank.showMessage(ConsoleProvider.successfullyOperation);
                }
                else
                {
                    Bank.showMessage(ConsoleProvider.haveCredit);
                }
            }
        }
    }
}