namespace Server.Pages.Security;

public class RegisterModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public RegisterModel(Data.DatabaseContext databaseContext, ILogger<RegisterModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}

	[BindProperty]
	public ViewModels.Pages.Account.RegisterViewModel ViewModel { get; set; }
	
	private ILogger<RegisterModel> Logger { get; }

	public void OnGet()
	{
	}

	public async Task OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return;
		}

		try
		{
			//bool isUsernameFound =
			//	await DatabaseContext.Users
			//	.Where(current => current.Username == fixedUsername)
			//	.AnyAsync();

			//bool isEmailAddressFound =
			//	await DatabaseContext.Users
			//	.Where(current => current.EmailAddress == fixedEmailAddress)
			//	.Where(current => current.IsEmailAddressVerified.HasValue && current.IsEmailAddressVerified.Value)
			//	.Where(current => current.IsDeleted == false)
			//	.AnyAsync();

			//// **************************************************
			//if (isUsernameFound)
			//{
			//	string errorMessage = string.Format
			//		(Resources.Messages.Errors.AlreadyExists, Resources.DataDictionary.Username);

			//	AddPageError(message: errorMessage);
			//}

			//if (isEmailAddressFound)
			//{
			//	string errorMessage = string.Format
			//		(Resources.Messages.Errors.AlreadyExists, Resources.DataDictionary.EmailAddress);

			//	AddPageError(message: errorMessage);
			//}
			//// **************************************************

			//if (isUsernameFound || isEmailAddressFound)
			//{
			//	return;
			//}

			//// **************************************************
			//Domain.User user = new()
			//{
			//	Username = fixedUsername,
			//	//RoleId = DefaultRoleId,
			//	EmailAddress = fixedEmailAddress,
			//	Password = NZLib.Security.Cryptography.GetSha256(text: ViewModel.Password),
			//};

			//await DatabaseContext.AddAsync(entity: user);

			//await DatabaseContext.SaveChangesAsync();
			// **************************************************

			AddToastSuccess(message: "اطلاعات شما با موفقیت در این سامانه ثبت شد...");

			// **************************************************
			// TODO: Send Verification Key To User Email Address
			// **************************************************
		}
		catch (Exception ex)
		{
			Logger.LogError(message: ex.Message);
			AddPageError(message: Resources.Messages.Errors.UnexpectedError);
		}
		finally
		{
			await DisposeDatabaseContextAsync();
		}

		return;
	}
}
