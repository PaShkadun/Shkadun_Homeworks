namespace Shkadun_Bank
{
    public enum BankOperation
    {
        AddAccount = 1,
        AddCashToAccount = 2,
        DeleteAccount = 3,
        ShowAccounts = 4,
        ManageAccount = 5,
        ShowBankBalance = 6,
        ShowAccountBalance = 7,
        Exit = 0
    }

    public enum DebitAccountOperation
    {
        AddNewCard = 1,
        DeleteCard = 2,
        ChooseOperation = 3,
        AddCashOnCart = 4,
    }

    public enum CreditAccountOperation
    {
        AddNewCard = 1,
        DeleteCard = 2,
        ChooseOperation = 3,
        AddCashOnCart = 4,
        AddCredit = 5,
        PayCredit = 6,
    }

    public enum DebitCardOperation
    {
        TransferMoneyToAccount = 1,
        TransferMoneyToCard = 2,
        SpendMoney = 3,
    }

    public enum CreditCardOperation
    {
        TransferMoneyToCard = 1,
        TransferMoneyToAccount = 2,
        AddCredit = 3,
        PayCredit = 4,
        ShowAllCredit = 5,
        SpendMoney = 6,
    }
}
