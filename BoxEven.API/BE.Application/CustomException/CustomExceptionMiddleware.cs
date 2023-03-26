using Newtonsoft.Json;

namespace BE.Application.CustomException;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
    {
        _next = next;
        _logger = logger.CreateLogger("CustomExceptionMiddleware");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var data = DateTime.Now;

            _logger.LogError(ex, "----> BoxEven API - Erro capturado as: {data}", data.ToString("dd/MM/yyyy HH:mm"));
            await HandleExceptionAsync(context);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        const int statusCode = 500;
        const string statusMessage = "Internal Server Error";
        const string jsonType = "application/json";

        var errorObject = JsonConvert.SerializeObject(new { StatusCode = statusCode, ErrorMessage = statusMessage });

        context.Response.ContentType = jsonType;
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(errorObject);
    }
}
