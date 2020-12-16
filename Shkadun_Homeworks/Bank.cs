using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shkadun_Bank
{
    public static class Bank
    {
        public delegate void ShowMessages(string message);
        private const int startMoney = 2500;
        private const int possibleCountActions = 7;

        public static List<Account> Accounts;
        public static int Money;
        public static ShowMessages ShowMessage;

        static Bank() 
        {
            Accounts = new List<Account>();
            Money = startMoney;
            ShowMessage += ConsoleProvider.ShowMessage;
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
            foreach (Account account in Accounts)
            {
                foreach (Card card in account.Cards)
                {
                    if (card.Type == TypeCard.Credit)
                    {
                        ((CreditCard)card).ChargeCredit();
                    }
                }
            }
        }

        public static void AddAccount()
        {
            Accounts.Add(new Account());
            ShowMessage(ConsoleProvider.SuccessfullyOperation);
        }

        public static void ShowAccounts()
        {
            if (Accounts.Count == 0)
            {
                ShowMessage(ConsoleProvider.NoneAccount);

                return;
            }

            var countCard = 0;

            foreach (Account account in Accounts)
            {
                Console.WriteLine(countCard++ + " " + ConsoleProvider.AccountNumber + account.Id + ConsoleProvider.AccountBalance + account.Money);
            }
        }

        public static void DeleteAccount(int accountIndex)
        {
            var creditDebt = false;

            foreach (Card card in Accounts[accountIndex].Cards)
            {
                if (card.Type == TypeCard.Credit)
                {
                    if (!((CreditCard)card).CheckCredits())
                    {
                        creditDebt = true;
                        break;
                    }
                }
            }

            if (!creditDebt)
            {
                Accounts.RemoveAt(accountIndex);
                ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
            else
            {
                ShowMessage(ConsoleProvider.HaveCredit);
            }
        }

        public static void AddCashOnAccount(int accountIndex, int sum)
        {
            if (sum > Money)
            {
                ShowMessage(ConsoleProvider.LackingMoney);
            }
            else
            {
                Accounts[accountIndex].Money += sum;
                Money -= sum;
            }
        }

        public static void Start()
        {
            var over = false;

            while (!over)
            {
                Console.Clear();

                switch (ConsoleProvider.ChooseActions(ConsoleProvider.ActionsBank, possibleCountActions))
                {
                    case 1:
                        AddAccount();
                        break;

                    case 2:
                        ShowAccounts();

                        if (Accounts.Count != 0)
                        { 
                            AddCashOnAccount(ConsoleProvider.ReadChoose(Accounts.Count - 1, ConsoleProvider.ChooseAccount), ConsoleProvider.InputIntegerValue());
                        }
                        break;

                    case 3:
                        ShowAccounts();

                        if (Accounts.Count != 0)
                        {
                            DeleteAccount(ConsoleProvider.ReadChoose(Accounts.Count - 1, ConsoleProvider.ChooseAccount));
                        }
                        break;

                    case 4:
                        ShowAccounts();
                        break;

                    case 5:
                        ShowAccounts();

                        if (Accounts.Count != 0)
                        {
                            Accounts[ConsoleProvider.ReadChoose(Accounts.Count - 1, ConsoleProvider.ChooseAccount)].ManageCards();
                        }
                        break;

                    case 6:
                        ShowMessage(ConsoleProvider.MessageBalance + Money);
                        break;

                    case 7:
                        ShowAccounts();
                        ShowMessage(ConsoleProvider.MessageBalance + Accounts[ConsoleProvider.ReadChoose(Accounts.Count - 1)].Money);
                        break;

                    case 0:
                        over = true;
                        break;

                    default:
                        break;
                }

                // Нужен, чтобы можно было увидеть сообщение после совершения какого-либо действия
                Thread.Sleep(500);
            }
        }
    }
}
