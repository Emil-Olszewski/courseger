using System.Net;
using Core.Domain.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Web.Api.Wrappers;

namespace Web.Api.Middlewares;

/// <summary>
/// Middleware przechwytujący wszystkie błędy, które wystąpiły w aplikacji. Zapobiega wyciekowi
/// wrażliwych danych, a także umożliwia zgrabne odczytanie błędu na froncie.
/// </summary>
internal sealed class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleError(context.Response, exception);
        }
    }

    private async Task HandleError(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";

        var responseModel = new ErrorResponse(exception.Message);

        switch (exception)
        {
            case BusinessException:
                response.StatusCode = (int) HttpStatusCode.BadRequest;
                break;
            default:
                responseModel.Message = "Unknown error occured.";
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                break;
        }

        await response.WriteAsync(Serialize(responseModel));
    }

    private string Serialize(ErrorResponse responseModel)
    {
        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        return JsonConvert.SerializeObject(responseModel, serializerSettings);
    }
}