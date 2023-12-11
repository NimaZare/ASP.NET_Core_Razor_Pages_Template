namespace Server.Pages.Account;

[Authorize]
public class LogoutModel : Infrastructure.BasePageModel
{
	public async Task<IActionResult> OnGet()
	{
		await HttpContext.SignOutAsync(scheme: Infrastructure.Security.Utility.AuthenticationScheme);
		return RedirectToPage(pageName: "/Index");
	}
}
