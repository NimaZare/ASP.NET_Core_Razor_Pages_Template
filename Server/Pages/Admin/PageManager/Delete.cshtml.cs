namespace Server.Pages.Admin.PageManager;

[Authorize(Roles = Constants.Role.Admin)]
public class DeleteModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public DeleteModel(Data.DatabaseContext databaseContext, ILogger<DeleteModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}

	private ILogger<DeleteModel> Logger { get; }

	public async Task OnGetAsync(Guid? id)
	{
		{
			try
			{
				if (id.HasValue)
				{
				}
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
		}
	}

	public async Task<IActionResult> OnPostDeleteAsync(Guid? id)
	{
		try
		{
			if (id.HasValue)
			{
			}
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
