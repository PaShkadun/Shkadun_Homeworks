using System;

namespace Shkadun_Bank
{
    public class NewRandom
    {
        private static Random random = new Random();

        public static string CreateNumberAccount()
        {
            //Разрешённые символы
            char[] accessSymbols = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                                's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            string numberAccount = "";

            for (int numberAccountLength = 0; numberAccountLength < 20; numberAccountLength++)
            {
                numberAccount += accessSymbols[random.Next(0, accessSymbols.Length - 1)];
            }

            Console.WriteLine(numberAccount);

            return numberAccount;
        }

        public static string RandomCardNumber()
        {
            string numberCard = "";
            //Разрешённые символы
            char[] accessSymbols = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            for (int numberCardLength = 0; numberCardLength < 16; numberCardLength++)
            {
                numberCard += accessSymbols[random.Next(0, accessSymbols.Length - 1)];
            }

            return numberCard;
        }
    }
}