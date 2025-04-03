using Aplication.Exceptions;
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
            catch (RepositoryException ex)
            {
                _logger.LogError(ex, "Error en repositorio: {error}", ex.Message);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = "Ocurrió un error al acceder a la base de datos.",
                    detalle = ex.Message
                }));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Regla de negocio violada: {error}", ex.Message);
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = ex.Message
                }));
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

            catch (NotFoundException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = "Recurso no encontrado",
                    details = ex.Message
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
