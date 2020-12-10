
namespace Shkadun_Bank
{
    abstract class Card
    {
        public int Balance { get; set; }
        public long CardNumber { get; protected set; }

        protected ConsoleProvider consoleProvider;

        abstract public void Transfer(CreditCard card);

        abstract public void Transfer(string NumberAccount, int howManySpend);

        //Положить средства на карту
        public bool AddCash(int howManySpend, int accountMoney)
        {  
            //Если средств недостаточно
            if (howManySpend > accountMoney)
            {
                consoleProvider.SendMessage(ConsoleProvider.INVALID_BALANCE);

                return false;
            }
            else
            {
                consoleProvider.SendMessage(ConsoleProvider.SUCCESSFUL);

                return true;
            }
        }

        abstract public void PullCash();
    }
}
