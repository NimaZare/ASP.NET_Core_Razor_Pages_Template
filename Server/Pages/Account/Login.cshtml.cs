namespace Server.Pages.Security;

public class LoginModel : Infrastructure.BasePageModel
{
	public LoginModel(Infrastructure.Settings.ApplicationSettings applicationSettings)
	{
		ViewModel = new();
		ApplicationSettings = applicationSettings;
	}

	[BindProperty]
	public string? ReturnUrl { get; set; }

	[BindProperty]
	public ViewModels.Pages.Account.LoginViewModel ViewModel { get; set; }

	public Infrastructure.Settings.ApplicationSettings ApplicationSettings { get; }

	public void OnGet(string? returnUrl)
	{
		ReturnUrl = returnUrl;
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		if (string.Compare(strA: ViewModel.Username, strB: "nimazare", ignoreCase: true) != 0)
		{
			AddPageError(message: Resources.Messages.Errors.InvalidUsernameOrPassword);
			return Page();
		}

		string? passwordHash = NZLib.Security.Hashing.GetSha256(text: ViewModel.Password);

		if (string.IsNullOrWhiteSpace(value: passwordHash))
		{
			AddPageError(message: Resources.Messages.Errors.InvalidUsernameOrPassword);
			return Page();
		}

		if (string.Compare(passwordHash, ApplicationSettings.MasterPassword, ignoreCase: true) != 0)
		{
			AddPageError(message: Resources.Messages.Errors.InvalidUsernameOrPassword);
			return Page();
		}

		var claims = new List<System.Security.Claims.Claim>();
		System.Security.Claims.Claim claim;

		claim = new System.Security.Claims.Claim(type: "FullName", value: "Mr. Nima Zare");
		claims.Add(item: claim);

		claim = new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Role, value: "Admin");
		claims.Add(item: claim);

		claim = new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Name, value: "Nima");
		claims.Add(item: claim);

		claim = new System.Security.Claims.Claim(type: System.Security.Claims.ClaimTypes.Email, value: "info@nimazare.org");
		claims.Add(item: claim);

		var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims: claims,
			authenticationType: Infrastructure.Security.Utility.AuthenticationScheme);

		var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(identity: claimsIdentity);

		var authenticationProperties = new AuthenticationProperties
		{
			IsPersistent = ViewModel.RememberMe
		};
		
		await HttpContext.SignInAsync(scheme: Infrastructure.Security.Utility.AuthenticationScheme,
			principal: claimsPrincipal, properties: authenticationProperties);
		
		if (string.IsNullOrWhiteSpace(ReturnUrl))
		{
			return RedirectToPage(pageName: "/Index");
		}
		else
		{
			return Redirect(url: ReturnUrl);
		}
	}
}
