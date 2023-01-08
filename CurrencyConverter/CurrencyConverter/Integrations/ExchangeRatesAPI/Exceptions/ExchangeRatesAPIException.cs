using System.Runtime.Serialization;

namespace CurrencyConverter.Integrations.ExchangeRatesAPI.Exceptions;

[Serializable]
public class ExchangeRatesApiException : Exception
{
    public ExchangeRatesApiException(string message) : base(message) { }

    protected ExchangeRatesApiException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public string? Request { get; set; }

    public override string ToString()
    {
        return $"{base.ToString()}, the request was {Request}";
    }
}