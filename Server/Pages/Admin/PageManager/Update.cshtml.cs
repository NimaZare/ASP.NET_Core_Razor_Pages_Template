namespace Server.Pages.Admin.PageManager;

[Authorize(Roles = Constants.Role.Admin)]
public class UpdateModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public UpdateModel(Data.DatabaseContext databaseContext, ILogger<UpdateModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}

	private ILogger<UpdateModel> Logger { get; }

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

	public async Task<IActionResult> OnPostAsync(Guid id)
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		try
		{
			return RedirectToPage("./Index");
		}
		catch (Exception ex)
		{
			Logger.LogError(message: ex.Message);
			AddToastError(message: Resources.Messages.Errors.UnexpectedError);

			return Page();
		}
		finally
		{
			await DisposeDatabaseContextAsync();
		}
	}
}
