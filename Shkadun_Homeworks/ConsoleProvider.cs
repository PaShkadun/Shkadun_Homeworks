using System;
using System.Text.RegularExpressions;

namespace Shkadun_Bank
{
    public static class ConsoleProvider
    {
        public const string incorrectInput = "Incorrect input. ";
        public const string lackingMoney = "You haven't money";
        public const string possibleValue = "Possible values 0-";
        public const string inputValue = "Input int value ";
        public const string actionsBank = "1. New Account\n2. Add cash on Account\n" +
                                          "3. Delete Account\n4. Show all accounts" +
                                          "\n5. Manage cards\n6. Show bank balance\n7. Show account balance\n0. Exit";
        public const string actionsCards= "1. Add card\n2. Delete card\n3. Choose card";
        public const string operationsCreditCard = "1. Transfer to card\n2. Transfer to account\n3. Add credit" +
                                                  "\n4. Pay credit\n5. Show credits\n6. Spend money";
        public const string operationsDebitCard = "1. Transfer to account\n2. Transfer to card\n3. Spend money";
        public const string successfullyOperation = "Successfully";
        public const string typesCard = "0 - Debet, 1 - Credit";
        public const string haveCredit = "You have credit.";
        public const string haveNotCredit = "You haven't credit";
        public const string creditPaid = "This credit paid out";
        public const string haveNegativeCredit = "You have debt credit(s)";
        public const string haveNotNegativeCredit = "You haven't debt credit";
        public const string noneCardOnAccount = "This account hasn't cards";
        public const string incorrectOperation = "This operation is block";
        public const string inputName = "Input recepient names";
        public const string inputRecepientAccounts = "Input number recepients account";

        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static int ChooseActions(string message, int maxValue)
        {
            Console.WriteLine(message);

            return ReadChoose(maxValue);
        }

        public static int ReadChoose(int maxValue, string message = null)
        {
            int choose;
            
            if(message != null)
            {
                Console.WriteLine(message);
            }

            while(true)
            {
                if (int.TryParse(Console.ReadLine(), out choose) && choose >= 0 && choose <= maxValue)
                {
                    return choose;
                }
                else
                {
                    Console.WriteLine(incorrectInput + possibleValue + maxValue);
                }
            }
        }

        public static int InputValue()
        {
            Console.WriteLine(inputValue);

            int sum;

            while (!int.TryParse(Console.ReadLine(), out sum)) ;

            return sum;
        }

        public static string InputStringValue()
        {
            Console.WriteLine(inputName);

            string name;

            while ((name = Console.ReadLine()) != null) ;

            return name;
        }

        public static string InputNumberAccount()
        {
            Console.WriteLine(inputRecepientAccounts);

            string numberAccount;

            while ((numberAccount = Console.ReadLine()) == null) ;

            Regex regex = new Regex(@"\w");
            MatchCollection matchCollection = regex.Matches(numberAccount);

            if(matchCollection.Count != 20)
            {
                Bank.showMessage(incorrectInput);

                return incorrectInput;
            }
            else
            {
                return numberAccount;
            }
        }
    }
}