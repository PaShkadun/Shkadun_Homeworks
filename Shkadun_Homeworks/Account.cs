using System;
using System.Collections.Generic;

namespace Shkadun_Bank
{
    public class Account
    {
        private const string debitTypeCard = "Дебетовая";
        private const string creditTypeCard = "Кредитная";

        private const byte debitCard = 1;
        private const byte creditCard = 0;

        private static ConsoleProvider consoleProvider;

        public List<Card> listCard;

        public string NumberAccount { get; private set; }
        public int Money { get; set; }
        public int CountCard { get; private set; }

        // Оплата кредита
        public static void PayCredit(List<Account> listAccount, int chooseAccount) 
        {
            // Если нет карт, выходим
            if (listAccount[chooseAccount].listCard.Count == 0) 
            { 
                consoleProvider.SendMessage(ConsoleProvider.INVALID_CARD); 
                return; 
            }

            // Выбираем карту
            ListCard(listAccount, chooseAccount);
            int chooseCard = consoleProvider.ReadNumber(0, listAccount[chooseAccount].listCard.Count - 1);

            // Пробуем выполнить. Если не получается - карта не кредитная
            try
            {
                CreditCard creditCard = (CreditCard)listAccount[chooseAccount].listCard[chooseCard];

                // Если нет кредитов
                if (creditCard.creditList.Count == 0)
                {
                    consoleProvider.SendMessage(ConsoleProvider.NOT_HAVE_CREDIT);
                }
                else
                {
                    int numberCard = 0;

                    // Вывод списка кредитов
                    foreach (Credit credit in creditCard.creditList)
                    {
                        Console.WriteLine($"{numberCard} - {credit.Sum}");
                        numberCard++;
                    }

                    // Выбор кредита
                    int chooseCreditCard = consoleProvider.PayCredit(numberCard);   

                    // Если недостаточно средств
                    if (creditCard.Balance < creditCard.creditList[chooseCreditCard].Sum)
                    {
                        consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);
                    }
                    else
                    {
                        creditCard.Balance -= creditCard.creditList[chooseCreditCard].Sum;
                        creditCard.creditList[chooseCreditCard].Sum = 0;
                        creditCard.creditList[chooseCreditCard].MonthsOfDebt = 0;

                        // Если кредит выплачен, удаляем
                        if (creditCard.creditList[chooseCreditCard].Months == 0 &&
                            creditCard.creditList[chooseCreditCard].Sum == 0)
                        {
                            creditCard.creditList.RemoveAt(chooseCreditCard);
                        }

                        consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
                    }
                }
            }
            catch { 
                consoleProvider.SendMessage(ConsoleProvider.BLOCKING_OPERATION);
            }
        }

        // Проверка карт и начисления кредитов
        public static void CheckAllCards(List<Account> listAccount)
        {
            CreditCard creditCard;

            if (listAccount.Count == 0)
            {
                return;
            }

            foreach (Account account in listAccount)
            {
                if (account.listCard.Count == 0)
                {
                    return;
                }
                else
                {
                    foreach (Card card in account.listCard)
                    {
                        try
                        {  
                            // Если это кредитная, начисляем кредиты
                            creditCard = (CreditCard)card;

                            // ...если они есть
                            for (int creditNumber = 0; creditNumber < creditCard.creditList.Count; creditNumber++)    
                            {
                                if (creditCard.creditList[creditNumber].Months > 0)
                                {
                                    creditCard.creditList[creditNumber].Months--;
                                    creditCard.creditList[creditNumber].Sum += creditCard.creditList[creditNumber].CreditRate;
                                    creditCard.creditList[creditNumber].MonthsOfDebt++;
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        // Получение кредита
        public static void GetCredit(List<Account> listAccount, int chooseAccount)
        {
            if (listAccount[chooseAccount].listCard.Count == 0) 
            { 
                return; 
            }

            try
            {
                ListCard(listAccount, chooseAccount);

                int chooseCard = consoleProvider.ReadNumber(0, listAccount[chooseAccount].listCard.Count - 1);    
                
                CreditCard creditCard = (CreditCard)listAccount[chooseAccount].listCard[chooseCard];

                consoleProvider.SendMessage(ConsoleProvider.CREDIT_INFO);

                // Если есть задолженности по кредитам
                if (!Credit.CheckCreditList(creditCard.creditList))
                {
                    int howMany = consoleProvider.ReadNumber();
                    int months = consoleProvider.ReadNumber();

                    creditCard.creditList.Add(new Credit(howMany, months));

                    creditCard.Balance += howMany;

                    consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);
                }
            }
            catch
            {
                consoleProvider.SendMessage(ConsoleProvider.BLOCKING_OPERATION);
            }
        }

        // Снятие(переходный метод)
        public static void PullMoney(List<Account> listAccount, int chooseAccount)
        {
            if (listAccount[chooseAccount].listCard.Count == 0) {
                return; 
            }

            ListCard(listAccount, chooseAccount);

            int chooseCard = consoleProvider.ReadNumber(0, listAccount[chooseAccount].listCard.Count - 1);

            listAccount[chooseAccount].listCard[chooseCard].PullCash();
        }

        // Вывод счетов
        public static void ListAccount(List<Account> listAccount)
        {
            if (listAccount.Count == 0) 
            { 
                return; 
            }

            int countAccounts = 0;

            foreach (Account account in listAccount)
            {
                Console.WriteLine($"{countAccounts++} - {account.NumberAccount}");
            }
        }

        // Выбор карты для дальнейшего её пополнения
        public static void ChooseCard(List<Account> listAccount, int chooseAccount)
        {
            if (listAccount[chooseAccount].listCard.Count == 0) 
            { 
                return; 
            }

            Account.ListCard(listAccount, chooseAccount);

            int chooseCard = consoleProvider.ReadNumber(0, listAccount[chooseAccount].listCard.Count - 1);
            int howMany = consoleProvider.HowManyTransfer(ConsoleProvider.transferCash, ConsoleProvider.transferOnCard); 

            if (listAccount[chooseAccount]
                .listCard[chooseCard]
                .AddCash(howMany, listAccount[chooseAccount].Money) == true) 
            {
                listAccount[chooseAccount].Money -= howMany;
                listAccount[chooseAccount].listCard[chooseCard].Balance += howMany;
            }
        }

        public static void TransferOnCard(List<Account> listAccount, int chooseAccount)
        {
            if (listAccount[chooseAccount].listCard.Count == 0) 
            { 
                return; 
            }

            // Список карт на выбранном акке
            ListCard(listAccount, chooseAccount);
            int chooseCard = consoleProvider.ReadNumber(0, listAccount[chooseAccount].listCard.Count - 1);

            // Выбираем акк. куда переводим
            ListAccount(listAccount);
            int chooseTransferAccount = consoleProvider.ReadNumber(0, listAccount.Count - 1);

            if (listAccount[chooseTransferAccount].listCard.Count == 0)
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_CARD);

                return;
            }
            else
            {
                ListCard(listAccount, chooseTransferAccount);
                int chooseTransferCard = consoleProvider.ReadNumber(0, listAccount[chooseTransferAccount].listCard.Count - 1);

                // Попытка перевода. Не получится с кредитовой на дебетовую карту.
                try
                {
                    listAccount[chooseAccount].listCard[chooseCard].Transfer(
                            (CreditCard)listAccount[chooseTransferAccount].listCard[chooseTransferCard]
                        );
                }
                catch
                {
                    consoleProvider.SendMessage(ConsoleProvider.BLOCKING_OPERATION);
                }
            }
        }

        public static void TransferOnAccount(List<Account> listAccount, int chooseAccount)
        {
            ListCard(listAccount, chooseAccount);

            if (listAccount[chooseAccount].listCard.Count == 0) 
            { 
                return; 
            }

            int chooseCard = consoleProvider.ReadNumber(0, listAccount[chooseAccount].listCard.Count - 1);
            listAccount[chooseAccount].
                listCard[chooseCard].
                Transfer(consoleProvider.
                WriteNameAccount(), consoleProvider.HowManyTransfer(ConsoleProvider.transferCash, ConsoleProvider.transferOnAccount));
        }

        //Вывод списка карт
        public static void ListCard(List<Account> listAccount, int chooseAccount)
        {
            CreditCard creditCard = new CreditCard();
            string typeCard;
            int countCards = 0;

            foreach (Card card in listAccount[chooseAccount].listCard)
            {
                if (card.GetType() == creditCard.GetType()) 
                { 
                    typeCard = creditTypeCard; 
                }
                else 
                { 
                    typeCard = debitTypeCard; 
                }

                Console.WriteLine($"{countCards++} - {card.CardNumber} {typeCard} {card.Balance}");
            }
        }

        //Добавления карты
        public static void AddCard(List<Account> listAccount, int chooseAccount)
        {
            switch (consoleProvider.ChooseCardType())
            {
                case creditCard: 
                    listAccount[chooseAccount].listCard.Add(new CreditCard()); 
                    break;
                case debitCard: 
                    listAccount[chooseAccount].listCard.Add(new DebetCard()); 
                    break;
                default: 
                    break;
            }
        }

        public Account()
        {
            Money = 1000;

            NumberAccount = NewRandom.CreateNumberAccount();
            consoleProvider = new ConsoleProvider();
            listCard = new List<Card>();
        }
    }
}
