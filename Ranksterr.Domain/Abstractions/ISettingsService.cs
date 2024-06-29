namespace Ranksterr.Domain.Abstractions;

public interface ISettingsService<T>
{
    public T GetSettings();
}