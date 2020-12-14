namespace Shkadun_Bank
{
    public abstract class Card
    {
        public int Balance { get; set; }
        public string CardNumber { get; protected set; }
        abstract public TypeCard Type { get; }

        public abstract void TransferToCard();

        public void TransferToAccount(string accountNumber, int sum)
        {
            if (accountNumber != ConsoleProvider.incorrectInput)
            {
                if (sum > Balance)
                {
                    Bank.showMessage(ConsoleProvider.lackingMoney);
                }
                else
                {
                    Balance -= sum;

                    Bank.showMessage(ConsoleProvider.successfullyOperation);
                }
            }
        }

        public void SpendMoney()
        {
            int money = ConsoleProvider.InputValue();

            if (money > Balance)
            {
                Bank.showMessage(ConsoleProvider.lackingMoney);
            }
            else
            {
                Balance -= money;
                Bank.showMessage(ConsoleProvider.successfullyOperation);
            }
        }

        public abstract void ChooseOperation();
    }
}