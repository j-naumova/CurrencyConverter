using CurrencyConverter.Integrations.ExchangeRatesAPI.Requests;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Responses;

namespace CurrencyConverter.Integrations.ExchangeRatesAPI.Interfaces;

public interface IExchangeRatesClient
{
    Task<LatestQuotesResponse> LatestQuotes(LatestQuotesRequest request);
    Task<SymbolsResponse> GetSymbols();
}