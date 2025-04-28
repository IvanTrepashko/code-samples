# Event Sourcing Sample

A simple event sourcing implementation using Marten and ASP.NET Core.

## Overview

This project demonstrates a basic event sourcing pattern for an Account domain using Marten as the event store. It shows how to:
- Create and modify an account using events
- Rebuild the current state from events
- Use Marten's event store capabilities

## Domain Model

### Events
- `AccountCreated`: Initial event when an account is created
- `AccountNameChanged`: Event for changing account name
- `AccountBalanceChanged`: Event for updating account balance
- `AccountBlocked`: Event for blocking an account
- `AccountActivated`: Event for activating an account

### Aggregate
The `Account` aggregate:
- Maintains current state (Id, Name, Balance, Status)
- Applies events to update its state
- Uses Marten's event sourcing capabilities

## API Endpoints

### Create Account
```http
POST /api/account/create
```
Creates a new account with initial balance of 150.

### Update Balance
```http
POST /api/account/balance
```
Updates the account balance with a random amount.

### Get Current Account
```http
GET /api/account/current
```
Retrieves the current state of the account by replaying all events.

## Technical Stack

- .NET 8.0
- Marten 7.40.1 (Event Store)
- PostgreSQL (via Marten)
- ASP.NET Core Web API
- Swagger UI for API documentation

## Setup

1. Ensure PostgreSQL is running
2. Update connection string in `Program.cs` if needed
3. Run the application
4. Access Swagger UI at `/swagger`

## How It Works

1. **Event Storage**: Events are stored in PostgreSQL using Marten
2. **State Rebuilding**: Current state is rebuilt by replaying all events
3. **Event Sourcing**: All changes are stored as events, not just the current state

## Example Flow

1. Create account → `AccountCreated` event
2. Update balance → `AccountBalanceChanged` event
3. Get current state → Marten replays all events to rebuild the account

## Notes

- Uses Marten's `LiveStreamAggregation` for real-time state rebuilding
- Events are immutable records
- Aggregate state is rebuilt by applying events in sequence 