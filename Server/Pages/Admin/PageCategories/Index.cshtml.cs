namespace Server.Pages.Admin.PageCategories;

[Authorize(Roles = Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public IndexModel(Data.DatabaseContext databaseContext, ILogger<IndexModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new List<ViewModels.Pages.Admin.PageCategories.IndexItemViewModel>();
	}

	private ILogger<IndexModel> Logger { get; }

	public IList<ViewModels.Pages.Admin.PageCategories.IndexItemViewModel> ViewModel { get; private set; }

	public async Task<IActionResult> OnGetAsync()
	{
		try
		{
			ViewModel = await DatabaseContext.PageCategories
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				.Select(current => new ViewModels.Pages.Admin.PageCategories.IndexItemViewModel
				{
					Id = current.Id,
					Name = current.Name,
					IsActive = current.IsActive,
					Ordering = current.Ordering,
					PageCount = current.Pages.Count,
					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime
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
