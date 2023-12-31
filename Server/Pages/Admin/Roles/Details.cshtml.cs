namespace Server.Pages.Admin.Roles;

[Authorize(Roles = Constants.Role.Admin)]
public class DetailsModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public DetailsModel(Data.DatabaseContext databaseContext, ILogger<DetailsModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}

	private ILogger<DetailsModel> Logger { get; }
	
	public ViewModels.Pages.Admin.Roles.DetailsOrDeleteViewModel ViewModel { get; private set; }
	
	public async Task<IActionResult> OnGetAsync(Guid? id)
	{
		try
		{
			if (id.HasValue == false)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}

			ViewModel = await DatabaseContext.Roles
				.Where(current => current.Id == id.Value)
				.Select(current => new ViewModels.Pages.Admin.Roles.DetailsOrDeleteViewModel()
				{
					Id = current.Id,
					Name = current.Name,
					IsActive = current.IsActive,
					Ordering = current.Ordering,
					UserCount = current.Users.Count,
					Description = current.Description,
					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime
				})
				.FirstOrDefaultAsync();

			if (ViewModel == null)
			{
				AddToastError(message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

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
