namespace Core.Domain.Exceptions;

/// <summary>
/// Obudowany wyjątek biznesowy.
/// </summary>
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
        
    }
}