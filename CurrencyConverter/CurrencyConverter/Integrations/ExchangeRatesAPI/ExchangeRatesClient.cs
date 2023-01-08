using CurrencyConverter.Integrations.ExchangeRatesAPI.Exceptions;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Interfaces;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Requests;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Responses;
using CurrencyConverter.Services.Interfaces;
using RestSharp;

namespace CurrencyConverter.Integrations.ExchangeRatesAPI;

public class ExchangeRatesClient : IExchangeRatesClient, IDisposable
{
    private readonly ISettingsService _settingsService;
    private readonly RestClient _client;

    public ExchangeRatesClient(ISettingsService settingsService)
    {
        _settingsService = settingsService;

        var options = new RestClientOptions(settingsService.ApiBaseUrl);
        _client = new RestClient(options);
    }

    public async Task<LatestQuotesResponse> LatestQuotes(LatestQuotesRequest request)
    {
        try
        {
            var restRequest = CreateBaseGetRequest("latest");

            restRequest.AddParameter("base", request.Base);
            restRequest.AddParameter("symbols", string.Join(',', request.Symbols));

            return await _client.GetAsync<LatestQuotesResponse>(restRequest);
        }
        catch (HttpRequestException ex)
        {
            throw new ExchangeRatesApiException(ex.Message) { Request = request.ToString() };
        }
    }

    public async Task<SymbolsResponse> GetSymbols()
    {
        try
        {
            var restRequest = CreateBaseGetRequest("symbols");
            return await _client.GetAsync<SymbolsResponse>(restRequest);

        }
        catch (HttpRequestException ex)
        {
            throw new ExchangeRatesApiException(ex.Message) { Request = nameof(GetSymbols) };
        }
    }

    private RestRequest CreateBaseGetRequest(string resource)
    {
        var restRequest = new RestRequest
        {
            Method = Method.Get,
            Resource = resource
        };

        restRequest.AddHeader("apikey", _settingsService.ApiKey);

        return restRequest;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _client?.Dispose();
    }
}