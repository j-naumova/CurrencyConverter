namespace CurrencyConverter.Models;

public class Quote
{
    public string? Base { get; set; }
    public Dictionary<string, decimal> Rates { get; set; } = new ();
}