namespace Shkadun_Bank
{
    public class DebetCard : Card
    {
        private const int PossibleCountOfDebitCardActions = 4;

        public DebetCard()
        {
            Balance = 0;
            CardNumber = CustomRandom.RandomCardNumber();
        }

        public override void TransferMoneyToCard()
        {
            Bank.ShowAccounts();

            int chooseAccount = ConsoleProvider.ReadChooseAction(Bank.Accounts.Count);
            
            if (Bank.Accounts[chooseAccount].Cards.Count == 0)
            {
                Bank.ShowMessage(ConsoleProvider.NoneCardOnAccount);

                return;
            }

            Bank.Accounts[chooseAccount].ShowCards();

            int chooseCard = ConsoleProvider.ReadChooseAction(Bank.Accounts[chooseAccount].Cards.Count);
            Card card = Bank.Accounts[chooseAccount].Cards[chooseCard];

            int money = ConsoleProvider.InputIntegerValue();

            if (money > card.Balance)
            {
                Bank.ShowMessage(ConsoleProvider.LackingMoney);
            }
            else
            {
                if (card == this)
                {
                    Bank.ShowMessage(ConsoleProvider.IncorrectOperation);

                    return;
                }

                card.Balance += money;
                Balance -= money;

                Bank.ShowMessage(ConsoleProvider.SuccessfullyOperation);
            }
        }

        public override void ChooseOperation()
        {
            Bank.ShowMessage(ConsoleProvider.OperationsDebitCard);

            switch (ConsoleProvider.ReadChooseAction(PossibleCountOfDebitCardActions))
            {
                case 1:
                    string numberAccount = ConsoleProvider.InputStringValue(ConsoleProvider.InputRecepientAccounts);

                    if (numberAccount.Length != 0)
                    {
                        TransferMoneyToAccount(numberAccount, ConsoleProvider.InputIntegerValue());
                    }
                    else
                    {
                        Bank.ShowMessage(ConsoleProvider.IncorrectInput);
                    }
                    break;

                case 2:
                    TransferMoneyToCard();
                    break;

                case 3: 
                    SpendMoney();
                    break;
            }
        }
    }
}