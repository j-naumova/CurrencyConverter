using System.Runtime.Serialization;

namespace CurrencyConverter.Exceptions;

[Serializable]
public class InvalidSymbolException : Exception
{
    public InvalidSymbolException(string message) : base(message) { }

    protected InvalidSymbolException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public override string ToString()
    {
        return $"Symbol {Message} is invalid.";
    }
}