using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public static class Bank
    {
        public delegate void ShowMessages(string message);

        private const int StartMoney = 2500;
        private const int PossibleCountOfBankActions = 7;
        private const int TypesOfAccount = 2;

        public static List<Account> Accounts;
        public static int Money;
        public static ShowMessages ShowMessage;

        static Bank() 
        {
            Accounts = new List<Account>();
            Money = StartMoney;
            ShowMessage += ConsoleProvider.ShowMessage;
        }

        public static void AddAccount()
        {
            ShowMessage(ConsoleProvider.TypesCardOrAccount);
            int chooseType = ConsoleProvider.ReadChooseAction(TypesOfAccount);

            if (chooseType == (int)TypeAccount.Debit)
            {
                Accounts.Add(new DebitAccount());
            }
            else
            {
                Accounts.Add(new CreditAccount());
            }

            ShowMessage(ConsoleProvider.SuccessfullyOperation);
        }

        public static List<Account> GetAccounts()
        {
            return Accounts;
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
            if (Accounts[accountIndex].Cards.Count == 0)
            {
                Accounts.RemoveAt(accountIndex);
                ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
            else
            {
                ShowMessage(ConsoleProvider.HaveCardOnAccount);
            }
        }

        public static void AddCashToAccount(int accountIndex, int sum)
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

                switch (ConsoleProvider.ReadChooseAction(PossibleCountOfBankActions, ConsoleProvider.ActionsBank))
                {
                    case (int)BankOperation.AddAccount:
                        AddAccount();
                        break;

                    case (int)BankOperation.AddCashToAccount:
                        ShowAccounts();

                        if (Accounts.Count != 0)
                        { 
                            AddCashToAccount(ConsoleProvider.ReadChooseAction(Accounts.Count - 1, ConsoleProvider.ChooseAccount), ConsoleProvider.InputIntegerValue());
                        }
                        break;

                    case (int)BankOperation.DeleteAccount:
                        ShowAccounts();

                        if (Accounts.Count != 0)
                        {
                            DeleteAccount(ConsoleProvider.ReadChooseAction(Accounts.Count - 1, ConsoleProvider.ChooseAccount));
                        }
                        break;

                    case (int)BankOperation.ShowAccounts:
                        ShowAccounts();
                        break;

                    case (int)BankOperation.ManageAccount:
                        ShowAccounts();

                        if (Accounts.Count != 0)
                        {
                            Accounts[ConsoleProvider.ReadChooseAction(Accounts.Count - 1, ConsoleProvider.ChooseAccount)].ManageAccount();
                        }
                        break;

                    case (int)BankOperation.ShowBankBalance:
                        ShowMessage(ConsoleProvider.MessageBalance + Money);
                        break;

                    case (int)BankOperation.ShowAccountBalance:
                        ShowAccounts();
                        ShowMessage(ConsoleProvider.MessageBalance + Accounts[ConsoleProvider.ReadChooseAction(Accounts.Count - 1)].Money);
                        break;

                    case (int)BankOperation.Exit:
                        over = true;
                        break;

                    default:
                        break;
                }

                Console.ReadKey(true);
            }
        }
    }
}
