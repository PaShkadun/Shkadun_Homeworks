using System;

namespace Shkadun_Bank
{
    public static class ConsoleProvider
    {
        public const string AddCreditInfo = "Input credit info(monthes, sum)";
        public const string ChargeCredit = "Charge sum ";
        public const string ChargeNumberCredit = " | credit number #";
        public const string AccountBalance = ", sum = ";
        public const string AccountNumber = "Number = ";
        public const string NoneAccount = "You haven't accounts";
        public const string ChooseAccount = "Choose account";
        public const string MessageBalance = "You balance = ";
        public const string ChooseCard = "Choose card";
        public const string NoneCards = "You haven't cards";
        public const string IncorrectInput = "Incorrect input. ";
        public const string LackingMoney = "You haven't money";
        public const string PossibleValue = "Possible values 0-";
        public const string InputValue = "Input int value ";
        public const string ActionsBank = "1. New Account\n2. Add cash on Account\n" +
                                          "3. Delete Account\n4. Show all accounts" +
                                          "\n5. Manage cards\n6. Show bank balance\n7. Show account balance\n0. Exit";
        public const string ActionsCards= "1. Add card\n2. Delete card\n3. Choose card";
        public const string OperationsCreditCard = "1. Transfer to card\n2. Transfer to account\n3. Add credit" +
                                                  "\n4. Pay credit\n5. Show credits\n6. Spend money";
        public const string OperationsDebitCard = "1. Transfer to account\n2. Transfer to card\n3. Spend money";
        public const string SuccessfullyOperation = "Successfully";
        public const string TypesCard = "0 - Debet, 1 - Credit";
        public const string HaveCredit = "You have credit.";
        public const string HaveNotCredit = "You haven't credit";
        public const string CreditPaid = "This credit paid out";
        public const string HaveNegativeCredit = "You have debt credit(s)";
        public const string HaveNotNegativeCredit = "You haven't debt credit";
        public const string NoneCardOnAccount = "This account hasn't cards";
        public const string IncorrectOperation = "This operation is block";
        public const string InputName = "Input recepient names";
        public const string InputRecepientAccounts = "Input number recepients account";
        public const string HaveCardOnAccount = "Blocking operation. You have card.";

        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static int ReadChooseAction(int maxValue, string message = null)
        {
            int choose;
            
            if (message != null)
            {
                Console.WriteLine(message);
            }

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choose) && choose >= 0 && choose <= maxValue)
                {
                    return choose;
                }
                else
                {
                    Console.WriteLine(IncorrectInput + PossibleValue + maxValue);
                }
            }
        }

        public static int InputIntegerValue()
        {
            Console.WriteLine(InputValue);

            // Как сделать без объявления не догнал.
            int sum;

            while (!(int.TryParse(Console.ReadLine(), out sum))) ;

            return sum;
        }

        public static string InputStringValue(string message)
        {
            Console.WriteLine(message);

            string name;

            while ((name = Console.ReadLine()) == null) ;

            return name;
        }
    }
}