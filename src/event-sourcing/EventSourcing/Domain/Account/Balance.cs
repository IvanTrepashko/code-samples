namespace EventSourcing.Domain.Account;

public record Balance(decimal Amount)
{
    public static Balance Create(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount should not be less than 0");
        }

        return new Balance(amount);
    }
}