using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shkadun_Bank
{
    public static class Bank
    {
        public delegate void ShowMessage(string message);

        private const string accountBalance = ", sum = ";
        private const string accountNumber = "Number = ";
        private const string noneAccount = "You haven't accounts";
        private const string chooseAccount = "Choose account";
        private const string chooseCard = "Choose card";
        private const string messageBalance = "You balance = ";

        private const int startMoney = 2500;
        private const int possibleCountActions = 7;
        private const int possibleCountActionsCard = 3;
        private const int typesOfCard = 2;

        public static List<Account> accounts;
        public static int money;
        public static ShowMessage showMessage;

        static Bank() {
            accounts = new List<Account>();
            money = startMoney;

            showMessage += ConsoleProvider.ShowMessage;
        }

        public static void CreateTimer()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    CheckCredits();
                }
            });
        }

        public static void CheckCredits()
        {
            foreach (var account in accounts)
            {
                foreach (var card in account.cards)
                {
                    if (card.Type == TypeCard.Credit)
                    {
                        ((CreditCard)card).ChargeCredit();
                    }
                }
            }
        }

        public static void ManageCards(int accountIndex) 
        {
            switch(ConsoleProvider.ChooseActions(ConsoleProvider.actionsCards, possibleCountActionsCard))
            {
                case 1:
                    showMessage(ConsoleProvider.typesCard);

                    if(ConsoleProvider.ReadChoose(typesOfCard) == (int)TypeCard.Credit)
                    {
                        accounts[accountIndex].cards.Add(new CreditCard());
                    }
                    else
                    {
                        accounts[accountIndex].cards.Add(new DebetCard());
                    }

                    showMessage(ConsoleProvider.successfullyOperation);
                    break;

                case 2:
                    accounts[accountIndex].Showcards();

                    if (accounts[accountIndex].cards.Count != 0)
                    {
                        accounts[accountIndex].DeleteCard();
                    }
                    break;

                case 3:
                    accounts[accountIndex].Showcards();

                    if(accounts[accountIndex].cards.Count != 0)
                    {
                        accounts[accountIndex].
                            cards[ConsoleProvider.ReadChoose(accounts[accountIndex].cards.Count, chooseCard)].
                            ChooseOperation();
                    }
                    break;
            }

        }

        public static void AddAccount()
        {
            accounts.Add(new Account());
            showMessage(ConsoleProvider.successfullyOperation);
        }

        public static void ShowAccounts()
        {
            if(accounts.Count == 0)
            {
                showMessage(noneAccount);

                return;
            }

            int countCard = 0;

            foreach(var account in accounts)
            {
                Console.WriteLine(countCard++ + " " + accountNumber + account.ID + accountBalance + account.Money);
            }
        }

        public static void DeleteAccount(int accountIndex)
        {
            bool haveDebt = false;

            foreach(var card in accounts[accountIndex].cards)
            {
                if(card.Type == TypeCard.Credit)
                {
                    if(!((CreditCard)card).CheckCredits())
                    {
                        haveDebt = true;
                        break;
                    }
                }
            }

            if(!haveDebt)
            {
                accounts.RemoveAt(accountIndex);
                showMessage(ConsoleProvider.successfullyOperation);
            }
            else
            {
                showMessage(ConsoleProvider.haveCredit);
            }
        }

        public static void AddCashOnAccount(int accountIndex, int sum)
        {
            if(sum > money)
            {
                showMessage(ConsoleProvider.lackingMoney);
            }
            else
            {
                accounts[accountIndex].Money += sum;
                money -= sum;
            }
        }

        public static void Start()
        {
            bool over = false;

            while(!over)
            {
                switch(ConsoleProvider.ChooseActions(ConsoleProvider.actionsBank, possibleCountActions))
                {
                    case 1:
                        AddAccount();
                        break;

                    case 2:
                        ShowAccounts();

                        if (accounts.Count != 0)
                        { 
                            AddCashOnAccount(ConsoleProvider.ReadChoose(accounts.Count - 1, chooseAccount),
                                             ConsoleProvider.InputValue());
                        }
                        break;

                    case 3:
                        ShowAccounts();

                        if (accounts.Count != 0)
                        {
                            DeleteAccount(ConsoleProvider.ReadChoose(accounts.Count - 1, chooseAccount));
                        }
                        break;

                    case 4:
                        ShowAccounts();
                        break;

                    case 5:
                        ShowAccounts();

                        if(accounts.Count != 0)
                        {
                            ManageCards(ConsoleProvider.ReadChoose(accounts.Count - 1, chooseAccount));
                        }
                        break;

                    case 6:
                        showMessage(messageBalance + money);
                        break;

                    case 7:
                        ShowAccounts();
                        showMessage(messageBalance + accounts[ConsoleProvider.ReadChoose(accounts.Count - 1)].Money);
                        break;

                    case 0:
                        over = true;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}