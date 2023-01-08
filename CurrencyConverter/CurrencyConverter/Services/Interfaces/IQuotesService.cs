using CurrencyConverter.Models;

namespace CurrencyConverter.Services.Interfaces;

public interface IQuotesService
{
    Task<Quote> GetTheLatestQuotes(string baseCurrency);
}