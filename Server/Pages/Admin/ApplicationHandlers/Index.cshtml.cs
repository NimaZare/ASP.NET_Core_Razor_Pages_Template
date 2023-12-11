namespace Server.Pages.Admin.ApplicationHandlers;


[Authorize(Roles = Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public IndexModel(Data.DatabaseContext databaseContext, ILogger<IndexModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new List<ViewModels.Pages.Admin.ApplicationHandlers.IndexItemViewModel>();
	}

	private ILogger<IndexModel> Logger { get; }

	public IList<ViewModels.Pages.Admin.ApplicationHandlers.IndexItemViewModel> ViewModel { get; private set; }

	public async Task<IActionResult> OnGetAsync(bool reloadData = false)
	{
		try
		{
			bool foundAny = await DatabaseContext.ApplicationHandlers.AnyAsync();

			if (reloadData || (foundAny == false))
			{
				await UpdateHandlersAsync();
			}

			ViewModel = await DatabaseContext.ApplicationHandlers
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				.ThenBy(current => current.Title)
				.Select(current => new ViewModels.Pages.Admin.ApplicationHandlers.IndexItemViewModel
				{
					Id = current.Id,
					Name = current.Name,
					Path = current.Path,
					Title = current.Title,
					IsActive = current.IsActive,
					Ordering = current.Ordering,
					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime
				}).ToListAsync();
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

	private async Task UpdateHandlersAsync()
	{
		try
		{
			var handlers = Infrastructure.RouteFinder.Find();

			if (handlers.Any())
			{
				foreach (var item in handlers)
				{
					bool foundAny = await DatabaseContext.ApplicationHandlers
						.Where(current => current.Name.ToLower().Trim() == item.Name.ToLower().Trim())
						.Where(current => current.Path.ToLower().Trim() == item.Path.ToLower().Trim())
						.AnyAsync();

					if (foundAny == false)
					{
						var newEntity = new Domain.ApplicationHandler(name: item.Name, path: item.Path)
						{
							Title = item.Title,
							Ordering = item.Ordering,
							IsActive = item.IsActive,
							AccessType = item.AccessType,
							Description = item.Description,
						};

						var entityEntry = await DatabaseContext.AddAsync(entity: newEntity);
						var affectedRows = await DatabaseContext.SaveChangesAsync();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(message: Constants.Logger.ErrorMessage, args: ex.Message);
			AddPageError(message: Resources.Messages.Errors.UnexpectedError);
		}
	}
}
