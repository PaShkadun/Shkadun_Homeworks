using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shkadun_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Account> listAccounts = new List<Account>();
            ConsoleProvider consoleProvider = new ConsoleProvider();

            // Параллельная задача, начисляющая проценты по кредитам каждые 20 секунд.
            Task task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(20000);
                    // Метод проверяющий и начисляющий кредиты
                    Account.CheckAllCards(listAccounts);    
                }
            });

            while (true)
            {
                switch (consoleProvider.WhatDo())
                {
                    case 0: //Создание счёта
                        listAccounts.Add(new Account());
                        break;
                    case 1: //Создание карты
                        Account.ListAccount(listAccounts);
                        Account.AddCard(listAccounts, consoleProvider.ReadNumber(0, listAccounts.Count - 1));
                        break;
                    case 2: //Вывод списка карт
                        Account.ListAccount(listAccounts);
                        Account.ListCard(listAccounts, consoleProvider.ReadNumber(0, listAccounts.Count - 1));
                        break;
                    case 3: //Пополнить карту
                        Account.ListAccount(listAccounts);
                        Account.ChooseCard(listAccounts, consoleProvider.ReadNumber(0, listAccounts.Count - 1));
                        break;
                    case 4: //Снять с карты 
                        Account.ListAccount(listAccounts);
                        Account.PullMoney(listAccounts, consoleProvider.ReadNumber(0, listAccounts.Count - 1));
                        break;
                    case 5: //Первод на карту
                        Account.ListAccount(listAccounts);
                        Account.TransferOnCard(listAccounts, consoleProvider.ReadNumber(0, listAccounts.Count - 1));
                        break;
                    case 6: //Перевод на счёт
                        Account.ListAccount(listAccounts);
                        Account.TransferOnAccount(listAccounts, consoleProvider.ReadNumber(0, listAccounts.Count - 1));
                        break;
                    case 7: //Взять кредит
                        Account.ListAccount(listAccounts);
                        Account.GetCredit(listAccounts, consoleProvider.ReadNumber(0, listAccounts.Count - 1));
                        break;
                    case 8: //Погасить кредит
                        Account.ListAccount(listAccounts);
                        Account.PayCredit(listAccounts, consoleProvider.ReadNumber(0, listAccounts.Count - 1));
                        break;
                    default: 
                        break;
                }

                // В некоторых ситуация экран обновляется и нет возможности увидеть
                // Что было веедено, поэтому здесь задержка в полсекунды.
                Thread.Sleep(500); 
            }
        }
    }
}
