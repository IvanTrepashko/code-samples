namespace EventSourcing.Domain.Account;

public class Account
{
    public Guid Id { get; private set; }

    public string AccountName { get; private set; }

    public Balance AccountBalance { get; private set; }

    public AccountStatus Status { get; private set; }

    public Account()
    {
    }

    private Account(Guid id, string accountName, Balance balance)
    {
        Id = id;
        AccountName = accountName;
        AccountBalance = balance;
        Status = AccountStatus.Active;
    }

    public static Account Create(AccountCreated accountCreated)
    {
        return new Account(accountCreated.AccountId, accountCreated.AccountName, accountCreated.Balance);
    }

    public void Apply(AccountBalanceChanged accountBalanceChanged)
    {
        AccountBalance = accountBalanceChanged.Balance;
    }

    public void Apply(AccountCreated accountCreated)
    {
        Id = accountCreated.AccountId;
        AccountName = accountCreated.AccountName;
        AccountBalance = accountCreated.Balance;
    }
}