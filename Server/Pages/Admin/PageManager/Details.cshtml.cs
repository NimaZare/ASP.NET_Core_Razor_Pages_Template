namespace Server.Pages.Admin.PageManager;

[Authorize(Roles = Constants.Role.Admin)]
public class DetailsModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public DetailsModel(Data.DatabaseContext databaseContext, ILogger<DetailsModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}

	private ILogger<DetailsModel> Logger { get; }

	public async Task<IActionResult> OnGetAsync(Guid id)
	{
		try
		{
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

		return Page();
	}
}
