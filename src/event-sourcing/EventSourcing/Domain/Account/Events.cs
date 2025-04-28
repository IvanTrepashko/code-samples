namespace EventSourcing.Domain.Account;

public record AccountCreated(Guid AccountId, string AccountName, Balance Balance);

public record AccountNameChanged(Guid AccountId, string AccountName);

public record AccountBalanceChanged(Guid AccountId, Balance Balance);

public record AccountBlocked(Guid AccountId);

public record AccountActivated(Guid AccountId);