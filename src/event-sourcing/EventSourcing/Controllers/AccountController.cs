using EventSourcing.Domain.Account;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController(IDocumentStore store) : ControllerBase
{
    public static Guid StreamId;

    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount()

    {
        var accountCreated = new AccountCreated(Guid.NewGuid(), "Test account", Balance.Create(150));

        await using var session = store.LightweightSession();

        var x = session.Events.StartStream<Account>(accountCreated.AccountId, accountCreated);
        StreamId = x.Id;
        await session.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("balance")]
    public async Task<IActionResult> UpdateBalance()
    {
        var balanceUpdated = new AccountBalanceChanged(StreamId, Balance.Create(new Random().Next(9999)));

        await using var session = store.LightweightSession();

        session.Events.Append(StreamId, balanceUpdated);

        await session.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentAccount()
    {
        await using var session = store.LightweightSession();

        var account = await session.Events.AggregateStreamAsync<Account>(StreamId);

        return Ok(account);
    }
}