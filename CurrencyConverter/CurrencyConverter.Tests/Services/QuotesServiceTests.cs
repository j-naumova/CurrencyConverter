using CurrencyConverter.Exceptions;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Interfaces;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Requests;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Responses;
using CurrencyConverter.Services;
using Moq;

namespace CurrencyConverter.Tests.Services;

public class QuotesServiceTests
{
    private Mock<IExchangeRatesClient> _exchangeRatesClientMock;

    private QuotesService _testSubject;

    [SetUp]
    public void SetUp()
    {
        _exchangeRatesClientMock = new Mock<IExchangeRatesClient>(MockBehavior.Strict);
        _testSubject = new QuotesService(_exchangeRatesClientMock.Object);
    }

    [Test]
    public void GetTheLatestQuotes_Returns()
    {
        var testSymbols =
            new Dictionary<string, string>(new[] { new KeyValuePair<string, string>("TEST", "test") });

        var testSymbolsResponse = new SymbolsResponse
        {
            Symbols = testSymbols
        };

        var request = new LatestQuotesRequest
        {
            Base = "TEST",
            Symbols = Constants.ResponseCurrencies
        };

        var testResponse = new LatestQuotesResponse
        {
            Base = "test",
            Rates = new Dictionary<string, decimal>(new[] { new KeyValuePair<string, decimal>("TEST", 100500) })
        };

        _exchangeRatesClientMock.Setup(m => m.GetSymbols()).ReturnsAsync(testSymbolsResponse);
        _exchangeRatesClientMock.Setup(m => m.LatestQuotes(It.Is<LatestQuotesRequest>(
                r => r.Base == request.Base && r.Symbols == request.Symbols)))
            .ReturnsAsync(testResponse);

        var actualResult = _testSubject.GetTheLatestQuotes("TEST").Result;

        Assert.That(actualResult, Is.Not.Null);
        Assert.That(actualResult.Base, Is.EqualTo(testResponse.Base));
        Assert.That(actualResult.Rates, Is.EqualTo(testResponse.Rates));

        _exchangeRatesClientMock.Verify(m => m.GetSymbols(), Times.Once);
        _exchangeRatesClientMock.Verify(m => m.LatestQuotes(It.Is<LatestQuotesRequest>(
            r => r.Base == request.Base && r.Symbols == request.Symbols)), Times.Once);
    }

    [Test]
    public void GetTheLatestQuotes_UsesCache()
    {
        var testSymbols =
            new Dictionary<string, string>(new[] { new KeyValuePair<string, string>("TEST", "test") });

        var testSymbolsResponse = new SymbolsResponse
        {
            Symbols = testSymbols
        };

        var request = new LatestQuotesRequest
        {
            Base = "TEST",
            Symbols = Constants.ResponseCurrencies
        };

        var testResponse = new LatestQuotesResponse
        {
            Base = "test",
            Rates = new Dictionary<string, decimal>(new[] { new KeyValuePair<string, decimal>("TEST", 100500) })
        };

        _exchangeRatesClientMock.Setup(m => m.GetSymbols()).ReturnsAsync(testSymbolsResponse);
        _exchangeRatesClientMock.Setup(m => m.LatestQuotes(It.Is<LatestQuotesRequest>(
                r => r.Base == request.Base && r.Symbols == request.Symbols)))
            .ReturnsAsync(testResponse);

        var firstCallResult = _testSubject.GetTheLatestQuotes("TEST").Result;
        var secondCallResult = _testSubject.GetTheLatestQuotes("TEST").Result;

        _exchangeRatesClientMock.Verify(m => m.GetSymbols(), Times.Once);
        _exchangeRatesClientMock.Verify(m => m.LatestQuotes(It.Is<LatestQuotesRequest>(
            r => r.Base == request.Base && r.Symbols == request.Symbols)), Times.Exactly(2));
    }

    [Test]
    public void GetTheLatestQuotes_Throws_When_InvalidSymbol()
    {
        var testSymbols =
            new Dictionary<string, string>(new[] { new KeyValuePair<string, string>("TEST", "test") });

        var testSymbolsResponse = new SymbolsResponse
        {
            Symbols = testSymbols
        };

        _exchangeRatesClientMock.Setup(m => m.GetSymbols()).ReturnsAsync(testSymbolsResponse);

        Assert.ThrowsAsync<InvalidSymbolException>(() => _testSubject.GetTheLatestQuotes("invalidBase"));

        _exchangeRatesClientMock.Verify(m => m.GetSymbols(), Times.Once);
        _exchangeRatesClientMock.Verify(m => m.LatestQuotes(It.IsAny<LatestQuotesRequest>()), Times.Never);
    }
}