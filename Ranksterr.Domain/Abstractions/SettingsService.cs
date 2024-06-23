using Microsoft.Extensions.Options;

namespace Ranksterr.Domain.Abstractions;

public class SettingsService<T>: ISettingsService<T> where T:class 
{
    private readonly T _settings;
    
    public SettingsService(IOptions<T> options)
    {
       _settings =  options.Value;
    }

    public T GetSettings()
    {
        return _settings;
    }
}

public interface ISettingsService<T>
{
    public T GetSettings();
}
