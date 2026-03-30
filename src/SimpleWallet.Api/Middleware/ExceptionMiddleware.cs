using System.Net;
using SimpleWallet.Domain.Exceptions;

namespace SimpleWallet.Api.Middleware;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionMiddleware> _logger;

	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled exception while processing request for {Path}", context.Request.Path);
			await HandleExceptionAsync(context, ex);
		}
	}

	private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		var (statusCode, title) = ex switch
		{
			DomainValidationException => (HttpStatusCode.BadRequest, "Validation failed"),
			InsufficientFundsException => (HttpStatusCode.BadRequest, "Insufficient funds"),
			KeyNotFoundException => (HttpStatusCode.NotFound, "Resource not found"),
			ArgumentException => (HttpStatusCode.BadRequest, "Invalid request"),
			_ => (HttpStatusCode.InternalServerError, "Server error")
		};

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)statusCode;

		await context.Response.WriteAsJsonAsync(new
		{
			status = context.Response.StatusCode,
			title,
			detail = ex.Message
		});
	}
}

