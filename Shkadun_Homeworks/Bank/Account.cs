﻿using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public abstract class Account
    {
        public const int AccountNumberLength = 20;

        public string Id { get; protected set; }

        public int Money { get; set; }

        public virtual List<Card> Cards { get; set; }

        abstract public void ShowCards();

        abstract public void AddNewCard();

        abstract public void DeleteCard();

        abstract public void ManageAccount();

        public void AddCashOnCart()
        {
            int money = ConsoleProvider.InputIntegerValue();
            
            if (Money < money)
            {
                ConsoleProvider.ShowMessage(ConsoleProvider.LackingMoney);
            }
            else
            {
                ShowCards();

                int chooseCard = ConsoleProvider.ReadChooseAction(Cards.Count - 1, ConsoleProvider.ChooseCard);
                Cards[chooseCard].Balance += money;

                ConsoleProvider.ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
        }
    }
}