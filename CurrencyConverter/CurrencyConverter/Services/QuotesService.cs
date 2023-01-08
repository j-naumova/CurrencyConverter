using CurrencyConverter.Exceptions;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Interfaces;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Requests;
using CurrencyConverter.Models;
using CurrencyConverter.Services.Interfaces;

namespace CurrencyConverter.Services;

public class QuotesService : IQuotesService
{
    private readonly IExchangeRatesClient _exchangeRatesClient;
    private Dictionary<string, string>? _availableSymbols;

    public QuotesService(IExchangeRatesClient exchangeRatesClient)
    {
        _exchangeRatesClient = exchangeRatesClient;
    }

    public async Task<Quote> GetTheLatestQuotes(string baseCurrency)
    {
        if (!(await ValidateSymbol(baseCurrency)))
        {
            throw new InvalidSymbolException(baseCurrency);
        }

        var request = new LatestQuotesRequest
        {
            Base = baseCurrency,
            Symbols = Constants.ResponseCurrencies
        };

        var response = await _exchangeRatesClient.LatestQuotes(request);

        return new Quote
        {
            Base = response.Base,
            Rates = response.Rates
        };
    }

    private async Task<bool> ValidateSymbol(string symbol)
    {
        _availableSymbols ??= (await _exchangeRatesClient.GetSymbols()).Symbols;

        return _availableSymbols.ContainsKey(symbol.ToUpper());
    }
}