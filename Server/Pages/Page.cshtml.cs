namespace Server.Pages;

public class PageModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public PageModel(Data.DatabaseContext databaseContext, ILogger<PageModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}

	private Domain.Page ViewModel { get; }

	private ILogger<PageModel> Logger { get; }

	public async Task<IActionResult> OnGetAsync(string? name)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(value: name))
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}

			var viewmodel = await DatabaseContext.Pages
				.Where(current => current.Title == name.ToLower())
				.FirstOrDefaultAsync();

			if (viewmodel == null)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}

			return Page();
		}
		catch (Exception ex)
		{
			Logger.LogError(message: Constants.Logger.ErrorMessage, args: ex.Message);
			AddToastError(message: Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage(pageName: "Index");
		}
		finally
		{
			await DisposeDatabaseContextAsync();
		}
	}
}
