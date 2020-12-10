using System;

namespace Shkadun_Bank
{
    class ConsoleProvider
    {
        public const string INVALID_BALANCE = "Недостаточно средств";
        public const string INVALID_CARD = "Недостаточно карт";
        public const string BLOCKING_OPERATION = "Недоступная операция";
        public const string INVALID_INPUT = "Некорректный ввод";
        public const string NEGATIVE_CREDIT = "Отказано. Имеется задолженность по кредиту или баланс отрицателен.";
        public const string SUCCESSFUL = "Успешная операция";
        public const string CREDIT_INFO = "Введите сумму и кол-во месяцев кредита";
        public const string NOT_HAVE_CREDIT = "У вас нет кредитов по этой карте";
        public const string putOn = "положить на";
        public const string incorrectInput = "Некорректный ввод";
        public const string chooseTypeCard = "Выберите тип: 0 кредитная, 1 - дебеторвая";
        public const string chooseCard = "Выберите карту";
        public const string pullCash = "Сколько хотите снять";
        public const string transferCash = "Сколько хотите перевести на";
        public const string transferOnCard = " карту";
        public const string transferOnAccount = " счёт";

        public int ChooseCardType()
        {
            Console.WriteLine(chooseTypeCard);
            return ReadNumber(0, 1);
        }

        public int HowManyTransfer(string transferMessage, string whereTransfer = null)
        {
            Console.WriteLine(transferMessage + whereTransfer);
            return ReadNumber();
        }//

        public int PayCredit(int limit)
        {
            Console.WriteLine(chooseCard);
            return ReadNumber(0, limit);
        }

        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }

        public int ReadNumber()
        {
            int read;

            while (!int.TryParse(Console.ReadLine(), out read)) ;

            return read;
        }
        public int ReadNumber(int min, int max)
        {
            int read;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out read))
                {
                    if (read >= min && read <= max)
                    {
                        Console.Clear();
                        return read;
                    }
                    else
                    {
                        Console.WriteLine($"{incorrectInput}, диапазон {min}-{max}");
                    }
                }
                else
                {
                    Console.WriteLine(incorrectInput);
                }
            }
        }

        public int WhatDo()
        {
            Console.WriteLine("\nЧто дальше?\n0. Создать счёт\n1. Добавить карту\n2. Список карт" +
                              "\n3. Пополнить карту\n4. Снять с карты\n5. Перевести на карту\n" +
                              "6. Перевести на счёт\n7. Взять кредит\n8. Погасить кредит");
            return ReadNumber(0, 8);
        }

        public string WriteNameAccount()
        {
            Console.WriteLine("Введите ФИО получателя");
            Console.ReadLine();
            Console.WriteLine("Введите номер счёта получателя(буквы + цифры)");
            return Console.ReadLine();
        }
    }
}
