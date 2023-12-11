namespace Server.Pages.Security.User;

[Authorize]
public class ChangeCellPhoneNumberModel : Infrastructure.BasePageModel
{
	public ChangeCellPhoneNumberModel()
	{
		ViewModel = new();
	}

	[BindProperty]
	protected ViewModels.Pages.Account.Users.ChangeCellPhoneNumberViewModel ViewModel { get; set; }

	public void OnGet()
	{
	}
}
