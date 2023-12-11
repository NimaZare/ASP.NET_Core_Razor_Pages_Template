using Infrastructure.Middlewares;


var builder = WebApplication.CreateBuilder();

builder.Services.AddHttpContextAccessor();

builder.Services.AddRouting(options =>
{
	options.LowercaseUrls = true;
	options.LowercaseQueryStrings = true;
});

builder.Services.AddRazorPages();

builder.Services.Configure<Infrastructure.Settings.ApplicationSettings>
	(builder.Configuration.GetSection(key: Infrastructure.Settings.ApplicationSettings.KeyName))
	.AddSingleton
	(implementationFactory: serviceType =>
	{
		var result = serviceType.GetRequiredService
			<Microsoft.Extensions.Options.IOptions
			<Infrastructure.Settings.ApplicationSettings>>().Value;

		return result;
	});

builder.Services.AddAuthentication(defaultScheme: Infrastructure.Security.Utility.AuthenticationScheme)
	.AddCookie(authenticationScheme: Infrastructure.Security.Utility.AuthenticationScheme);


// **************************************************
//builder.Services
//	.AddAuthentication(defaultScheme: Infrastructure.Security.Utility.AuthenticationScheme)
//	.AddCookie(authenticationScheme: Infrastructure.Security.Utility.AuthenticationScheme)
//	.AddGoogle(authenticationScheme: Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme,
//	configureOptions: options =>
//	{
//		options.ClientId =
//			builder.Configuration["ApplicationSettings:Authentication:Google:ClientId"];

//		options.ClientSecret =
//			builder.Configuration["ApplicationSettings:Authentication:Google:ClientSecret"];

//		// MapJsonKey() -> using Microsoft.AspNetCore.Authentication;
//		options.ClaimActions.MapJsonKey
//			(claimType: "urn:google:picture", jsonKey: "picture", valueType: "url");
//	})
//	;
// **************************************************
// **************************************************
// **************************************************


var connectionString = builder.Configuration.GetConnectionString(name: "ConnectionString");

builder.Services.AddDbContext<Data.DatabaseContext>(optionsAction: options =>
{
	options.UseLazyLoadingProxies();
	options.UseSqlServer(connectionString: connectionString);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseGlobalException();
	app.UseExceptionHandler("/Errors/Error");
	app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseActivationKeys();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCultureCookie();
app.MapRazorPages();

app.Run();
