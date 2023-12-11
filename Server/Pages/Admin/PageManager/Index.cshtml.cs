namespace Server.Pages.Admin.PageManager;

[Authorize(Roles = Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public IndexModel(Data.DatabaseContext databaseContext, ILogger<IndexModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}

	private ILogger<IndexModel> Logger { get; }
	
	public async Task<IActionResult> OnGetAsync(int pageSize = 10, int pageNumber = 1)
	{
		try
		{
		}
		catch (Exception ex)
		{
			Logger.LogError(message: ex.Message);
			AddToastError(message: Resources.Messages.Errors.UnexpectedError);
		}
		finally
		{
			await DisposeDatabaseContextAsync();
		}

		return Page();
	}
}
