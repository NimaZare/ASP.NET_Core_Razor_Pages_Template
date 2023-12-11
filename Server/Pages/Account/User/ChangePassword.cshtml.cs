namespace Server.Pages.Security.User;

[Authorize]
public class ChangePasswordModel : Infrastructure.BasePageModel
{
	public ChangePasswordModel()
	{
		ViewModel = new();
	}

	[BindProperty]
	protected ViewModels.Pages.Account.Users.ChangePasswordViewModel ViewModel { get; set; }

	public void OnGet()
	{
	}
}
