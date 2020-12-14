namespace Shkadun_Bank
{
    public class DebetCard : Card
    {
        private const int possibleCountActions = 3;

        public override TypeCard Type { get; }

        public DebetCard()
        {
            Balance = 0;
            Type = TypeCard.Debit;
            CardNumber = NewRandom.RandomCardNumber();
        }

        public override void TransferToCard()
        {
            Bank.ShowAccounts();

            int chooseAccount = ConsoleProvider.ReadChoose(Bank.accounts.Count);
            
            if (Bank.accounts[chooseAccount].cards.Count == 0)
            {
                Bank.showMessage(ConsoleProvider.noneCardOnAccount);

                return;
            }

            Bank.accounts[chooseAccount].Showcards();

            int chooseCard = ConsoleProvider.ReadChoose(Bank.accounts[chooseAccount].cards.Count);
            Card card = Bank.accounts[chooseAccount].cards[chooseCard];

            int money = ConsoleProvider.InputValue();

            if (money > card.Balance)
            {
                Bank.showMessage(ConsoleProvider.lackingMoney);
            }
            else
            {
                if (card == this)
                {
                    Bank.showMessage(ConsoleProvider.incorrectOperation);
                }

                card.Balance += money;
                Balance -= money;

                Bank.showMessage(ConsoleProvider.successfullyOperation);
            }
        }

        public override void ChooseOperation()
        {
            Bank.showMessage(ConsoleProvider.operationsDebitCard);

            switch(ConsoleProvider.ReadChoose(possibleCountActions))
            {
                case 1:
                    string numberAccount = ConsoleProvider.InputNumberAccount();

                    if(numberAccount != ConsoleProvider.incorrectInput)
                    {
                        TransferToAccount(
                                numberAccount,
                                ConsoleProvider.InputValue()
                            );
                    }
                    break;

                case 2:
                    TransferToCard();
                    break;

                case 3: 
                    SpendMoney();
                    break;
            }
        }
    }
}