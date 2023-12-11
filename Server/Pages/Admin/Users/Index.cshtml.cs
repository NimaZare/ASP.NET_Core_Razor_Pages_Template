namespace Server.Pages.Admin.Users;

[Authorize(Roles = Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public IndexModel(Data.DatabaseContext databaseContext, ILogger<IndexModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new List<ViewModels.Pages.Admin.Users.IndexItemViewModel>();
	}

	private ILogger<IndexModel> Logger { get; }

	public IList<ViewModels.Pages.Admin.Users.IndexItemViewModel> ViewModel { get; private set; }

	public async Task<IActionResult> OnGetAsync()
	{
		try
		{
			ViewModel = await DatabaseContext.Users
				.OrderByDescending(current => current.InsertDateTime)
				.Select(current => new ViewModels.Pages.Admin.Users.IndexItemViewModel
				{
					Id = current.Id,

					IsActive = current.IsActive,
					IsProgrammer = current.IsProgrammer,
					IsUndeletable = current.IsUndeletable,
					IsEmailAddressVerified = current.IsEmailAddressVerified,
					IsVisibleInContactUsPage = current.IsVisibleInContactUsPage,
					IsCellPhoneNumberVerified = current.IsCellPhoneNumberVerified,

					RoleId = current.RoleId,
					RoleName = current.Role.Name,
					IsRoleActive = current.Role.IsActive,

					LastName = current.LastName,
					Username = current.Username,
					FirstName = current.FirstName,
					EmailAddress = current.EmailAddress,
					CellPhoneNumber = current.CellPhoneNumber,

					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime,
					LastLoginDateTime = current.LastLoginDateTime
				})
				.ToListAsync();
		}
		catch (Exception ex)
		{
			Logger.LogError(message: Constants.Logger.ErrorMessage, args: ex.Message);
			AddPageError(message: Resources.Messages.Errors.UnexpectedError);
		}
		finally
		{
			await DisposeDatabaseContextAsync();
		}

		return Page();
	}
}
