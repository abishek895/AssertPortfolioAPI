using Serilog;
using System.Net;
using System.Text.Json;

namespace AssetPortfolio.Middleware
{
	public class GlobalExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalExceptionMiddleware> _logger;

		public GlobalExceptionMiddleware(RequestDelegate next,ILogger<GlobalExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
				Console.WriteLine("Next Middleware", context);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Unhandled exception occurred while processing request {Method} {Path}",
				   context.Request.Method, context.Request.Path);
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";

			var statusCode = ex switch
			{
				KeyNotFoundException => HttpStatusCode.NotFound,
				ArgumentException => HttpStatusCode.BadRequest,
				UnauthorizedAccessException => HttpStatusCode.Unauthorized,
				_ => HttpStatusCode.InternalServerError
			};

			context.Response.StatusCode = (int)statusCode;

			var response = new
			{
				status = context.Response.StatusCode,
				message = ex.Message
			};

			return context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
	}
}
