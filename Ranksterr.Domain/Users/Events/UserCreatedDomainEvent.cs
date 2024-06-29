using Ranksterr.Domain.Abstractions;

namespace Ranksterr.Domain.Users.Events;

public sealed record UserCreatedDomainEvent( Guid UserId ) : IDomainEvent;
