namespace Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
	public GlobalExceptionHandlerMiddleware(RequestDelegate next)
	{
		Next = next;
	}

	private RequestDelegate Next { get; }

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await Next(httpContext);
		}
		catch
		{
			httpContext.Response.Redirect(location: "/Errors/Error500", permanent: false);
		}
	}
}
