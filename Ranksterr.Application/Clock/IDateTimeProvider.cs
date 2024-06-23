namespace Ranksterr.Application.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }    
}