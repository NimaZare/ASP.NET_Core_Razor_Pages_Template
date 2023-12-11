namespace Infrastructure.Middlewares;

public class ActivationKeysHandlerMiddleware
{
	public ActivationKeysHandlerMiddleware(RequestDelegate next)
	{
		Next = next;
	}

	private static string GetSha256(string value)
	{
		var stringBuilder = new System.Text.StringBuilder();

		try
		{
			var valueBytes = System.Text.Encoding.UTF8.GetBytes(s: value);
			byte[] hashBytes = System.Security.Cryptography.SHA256.HashData(source: valueBytes);

			foreach (byte theByte in hashBytes)
			{
				stringBuilder.Append(value: theByte.ToString("x2"));
			}

			return stringBuilder.ToString();
		}
		catch
		{
			return string.Empty;
		}
	}

	private static string GetValidActivationKeyByDomain(string domain)
	{
		var result = GetSha256(value: domain);

		return result;
	}

	private RequestDelegate Next { get; }

	public async Task InvokeAsync(HttpContext httpContext, Settings.ApplicationSettings applicationSettings)
	{
		if (applicationSettings == null || applicationSettings.ActivationKeys == null || applicationSettings.ActivationKeys.Length == 0)
		{
			await httpContext.Response.WriteAsync("No Activation Key");
			return;
		}

		var domain = httpContext.Request.Host.Value;
		var validActivationKey = GetValidActivationKeyByDomain(domain: domain);

		if (string.IsNullOrWhiteSpace(validActivationKey))
		{
			await httpContext.Response.WriteAsync("No Activation Key");
			return;
		}

		var contains = applicationSettings.ActivationKeys
			.Where(current => current.ToLower() == validActivationKey.ToLower())
			.Any();

		if (contains == false)
		{
			await httpContext.Response.WriteAsync("No Activation Key");
			return;
		}

		await Next(httpContext);
	}
}
