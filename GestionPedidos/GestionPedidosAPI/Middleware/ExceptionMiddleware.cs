using FluentValidation;
using System.Net;
using System.Text.Json;

namespace GestionPedidosAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Errores de validación: {Errors}", ex.Errors);
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = "Error de validación",
                    errors
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = "Error interno del servidor"
                }));
            }
        }
    }
}
