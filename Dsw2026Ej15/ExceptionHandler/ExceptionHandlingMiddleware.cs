using Dsw2026Ej15.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Api.ExceptionHandler;
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try { await _next(context); }
            catch (ValidationException ex) { await HandleValidationException(context, ex); }
            catch (Exception ex) { await HandleExceptionAsync(context, ex); }
        }

        private async Task HandleValidationException(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var responseBody = new
                {
                    Message = exception.Message,
                    Errors = exception.Errors
                };
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody, jsonOptions));
    }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError; // 500

            _logger.LogError(exception, "Ocurrió un error no controlado en el servidor.");

            var responseBody = new
            {
                Message = "Ocurrió un error interno en el servidor. Inténtelo más tarde."
            };

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody, jsonOptions));
        }
}
