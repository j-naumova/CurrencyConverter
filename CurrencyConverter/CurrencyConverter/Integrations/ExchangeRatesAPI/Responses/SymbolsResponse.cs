namespace CurrencyConverter.Integrations.ExchangeRatesAPI.Responses;

public class SymbolsResponse
{
    public bool? Success { get; set; }
    public Dictionary<string, string> Symbols { get; set; } = new();
}