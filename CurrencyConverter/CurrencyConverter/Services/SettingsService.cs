using CurrencyConverter.Services.Interfaces;

namespace CurrencyConverter.Services;

public class SettingsService : ISettingsService
{
    private readonly IConfiguration _configuration;

    public SettingsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ApiBaseUrl => GetSetting("BaseUrl");
    public string ApiKey => GetSetting("ApiKey");

    private string GetSetting(string key)
    {
        return _configuration.GetSection("Settings")[key] ?? "";
    }
}