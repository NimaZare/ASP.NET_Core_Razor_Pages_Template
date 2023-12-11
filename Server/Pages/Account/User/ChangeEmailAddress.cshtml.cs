namespace Server.Pages.Security.User;

[Authorize]
public class ChangeEmailAddressModel : Infrastructure.BasePageModel
{
	public ChangeEmailAddressModel()
	{
		ViewModel = new();
	}

	[BindProperty]
	protected ViewModels.Pages.Account.Users.ChangeEmailAddressViewModel ViewModel { get; set; }

	public void OnGet()
	{
	}
}
