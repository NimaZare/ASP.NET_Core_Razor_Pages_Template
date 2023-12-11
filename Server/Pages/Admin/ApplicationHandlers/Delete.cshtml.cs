namespace Server.Pages.Admin.ApplicationHandlers;

[Authorize(Roles = Constants.Role.Admin)]
public class DeleteModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public DeleteModel(Data.DatabaseContext databaseContext, Logger<DeleteModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}

	private ILogger<DeleteModel> Logger { get; }


	[BindProperty]
	public ViewModels.Pages.Admin.ApplicationHandlers.DetailsOrDeleteViewModel ViewModel { get; private set; }

	public async Task<IActionResult> OnGetAsync(Guid? id)
	{
		try
		{
			if (id.HasValue == false)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}

			ViewModel = await DatabaseContext.ApplicationHandlers
				.Where(current => current.Id == id.Value)
				.Select(current => new ViewModels.Pages.Admin.ApplicationHandlers.DetailsOrDeleteViewModel()
				{
					Id = current.Id,
					Name = current.Name,
					Path = current.Path,
					Title = current.Title,
					IsActive = current.IsActive,
					Ordering = current.Ordering,
					AccessType = current.AccessType,
					Description = current.Description,
					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime
				}).FirstOrDefaultAsync();

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

	public async Task<IActionResult> OnPostAsync(Guid? id)
	{
		try
		{
			if (id.HasValue == false)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}
			
			var foundedItem = await DatabaseContext.ApplicationHandlers
				.Where(current => current.Id == id.Value)
				.FirstOrDefaultAsync();

			if (foundedItem == null)
			{
				AddToastError(message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

				return RedirectToPage(pageName: "Index");
			}
			
			var entityEntry = DatabaseContext.Remove(entity: foundedItem);
			var affectedRows = await DatabaseContext.SaveChangesAsync();

			var successMessage = string.Format(format: Resources.Messages.Successes.Deleted,
				arg0: Resources.DataDictionary.ApplicationHandler);

			AddToastSuccess(message: successMessage);

			return RedirectToPage(pageName: "Index");
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
