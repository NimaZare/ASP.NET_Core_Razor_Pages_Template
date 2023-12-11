namespace Infrastructure.Middlewares;

public static class ExtensionMethods
{
	public static IApplicationBuilder UseCultureCookie(this IApplicationBuilder app)
	{
		return app.UseMiddleware<CultureCookieHandlerMiddleware>();
	}

	public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
	{
		return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
	}

	public static IApplicationBuilder UseActivationKeys(this IApplicationBuilder app)
	{
		return app.UseMiddleware<ActivationKeysHandlerMiddleware>();
	}
}
