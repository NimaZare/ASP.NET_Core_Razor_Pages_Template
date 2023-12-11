namespace Server.Pages.Admin.MenuItems;

[Authorize(Roles = Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public IndexModel(Data.DatabaseContext databaseContext, ILogger<IndexModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new List<ViewModels.Pages.Admin.MenuItems.IndexItemViewModel>();
	}

	private ILogger<IndexModel> Logger { get; }
	
	public IList<ViewModels.Pages.Admin.MenuItems.IndexItemViewModel> ViewModel { get; private set; }

	public async Task<IActionResult> OnGetAsync(Guid? id = null)
	{
		try
		{
			ViewModel = await DatabaseContext.MenuItems
				.Where(x => x.ParentId == id)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Title)
				.Select(current => new ViewModels.Pages.Admin.MenuItems.IndexItemViewModel
				{
					Id = current.Id,
					Icon = current.Icon,
					Title = current.Title,
					IsActive = current.IsActive,
					IsPublic = current.IsPublic,
					Ordering = current.Ordering,
					IsDeleted = current.IsDeleted,
					IsUndeletable = current.IsUndeletable,
					HasAnySubMenu = current.SubMenus.Any(),
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
