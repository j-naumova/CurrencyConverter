namespace CurrencyConverter.Integrations.ExchangeRatesAPI.Requests;

public class LatestQuotesRequest
{
    public string? Base { get; set; }
    public List<string> Symbols { get; set; } = new();

    public override string ToString()
    {
        return $"{nameof(LatestQuotesRequest)} Base: {Base}, Symbols: {string.Join(",", Symbols)}";
    }
}