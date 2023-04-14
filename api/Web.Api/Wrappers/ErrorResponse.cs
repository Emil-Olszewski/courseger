namespace Web.Api.Wrappers;

/// <summary>
/// Obudowany błąd zwracany zewnętrznemu konsumentowi.
/// </summary>
internal sealed class ErrorResponse
{
    public string Message { get; set; }

    public ErrorResponse(string message)
    {
        Message = message;
    }
}