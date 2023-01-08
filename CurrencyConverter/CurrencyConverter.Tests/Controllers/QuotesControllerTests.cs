using CurrencyConverter.Controllers;
using CurrencyConverter.Exceptions;
using CurrencyConverter.Models;
using CurrencyConverter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CurrencyConverter.Tests.Controllers;

public class QuotesControllerTests
{
    private Mock<IQuotesService> _quotesServiceMock;
    private QuotesController _testSubject;

    [SetUp]
    public void Setup()
    {
        _quotesServiceMock = new Mock<IQuotesService>(MockBehavior.Strict);
        _testSubject = new QuotesController(_quotesServiceMock.Object);
    }

    [Test]
    public void GetLatestQuotes_Returns()
    {
        var testSymbol = "BTC";
        var testResult = new Quote
        {
            Base = testSymbol,
            Rates = new Dictionary<string, decimal>()
        };

        _quotesServiceMock.Setup(m => m.GetTheLatestQuotes(testSymbol)).ReturnsAsync(testResult);

        var actualResult = _testSubject.GetLatestQuotes(testSymbol).Result;

        Assert.That(actualResult, Is.Not.Null);

        var objectResult = actualResult as ObjectResult;

        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult.StatusCode, Is.EqualTo(200));
        Assert.That(objectResult.Value, Is.EqualTo(testResult));

        _quotesServiceMock.Verify(m => m.GetTheLatestQuotes(testSymbol), Times.Once);
    }

    [Test]
    public void GetLatestQuotes_ReturnsNotFound_WhenSymbolInvalid()
    {
        var testSymbol = "invalid";
        var ex = new InvalidSymbolException(testSymbol);

        _quotesServiceMock.Setup(m => m.GetTheLatestQuotes(testSymbol)).ThrowsAsync(ex);

        var actualResult = _testSubject.GetLatestQuotes(testSymbol).Result;

        Assert.That(actualResult, Is.Not.Null);

        var notFoundResult = actualResult as NotFoundObjectResult;

        Assert.That(notFoundResult, Is.Not.Null);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        Assert.That(notFoundResult.Value, Is.EqualTo(ex.ToString()));

        _quotesServiceMock.Verify(m => m.GetTheLatestQuotes(testSymbol), Times.Once);
    }
}