using Application.Common;
using Application.Common.Exceptions;
using Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Common.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static ILogger<ExceptionMiddleware> _logger;

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
                await HandleExceptionAsync(
                    httpContext: httpContext,
                    statusCode: HttpStatusCode.BadRequest,
                    ex: ex,
                    message: string.Concat(ex.Errors.Select(x => $"{x.Key}: {string.Join(",", x.Value)}"))
                );
            }
            catch (BusinessException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex, ex.Message);
            }
            catch (BusinessRuleValidationException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, ex, ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.Conflict, ex, ex.Message);
            }
            catch (UnitOfWorkException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.ServiceUnavailable, ex, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.ServiceUnavailable, ex, "General system error");
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, HttpStatusCode statusCode, Exception ex,
            string message)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) statusCode;
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(GenericResult<string>.Failure(message),
                Formatting.Indented));
        }
    }
}