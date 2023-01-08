namespace CurrencyConverter.Services.Interfaces;

public interface ISettingsService
{
    string ApiBaseUrl { get; }
    string ApiKey { get; }
}