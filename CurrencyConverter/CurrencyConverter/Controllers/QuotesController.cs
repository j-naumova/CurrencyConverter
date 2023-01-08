using CurrencyConverter.Exceptions;
using CurrencyConverter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers;

[ApiController]
[Route("[controller]")]
public class QuotesController : ControllerBase
{
    private readonly IQuotesService _quotesService;

    public QuotesController(IQuotesService quotesService)
    {
        _quotesService = quotesService;
    }

    [HttpGet(Name = "latest/{baseCurrency}")]
    public async Task<IActionResult> GetLatestQuotes(string baseCurrency)
    {
        try
        {
            var result = await _quotesService.GetTheLatestQuotes(baseCurrency);
            return Ok(result);
        }
        catch (InvalidSymbolException ex)
        {
            return NotFound(ex.ToString());
        }
    }
}