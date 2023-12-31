namespace Server.Pages.Admin.ApplicationHandlers;

[Authorize(Roles = Constants.Role.Admin)]
public class UpdateModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public UpdateModel(Data.DatabaseContext databaseContext, ILogger<UpdateModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}

	private ILogger<UpdateModel> Logger { get; }

	[BindProperty]
	public ViewModels.Pages.Admin.ApplicationHandlers.UpdateViewModel ViewModel { get; set; }

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
				.Select(current => new ViewModels.Pages.Admin.ApplicationHandlers.UpdateViewModel()
				{
					Id = current.Id,
					Name = current.Name,
					Path = current.Path,
					Title = current.Title,
					IsActive = current.IsActive,
					Ordering = current.Ordering,
					AccessType = current.AccessType,
					Description = current.Description
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

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		try
		{
			var foundedItem = await DatabaseContext.ApplicationHandlers
				.Where(current => current.Id == ViewModel.Id)
				.FirstOrDefaultAsync();

			if (foundedItem == null)
			{
				AddToastError(message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

				return RedirectToPage(pageName: "Index");
			}

			var fixedName = NZLib.Utility.RemoveSpaces(text: ViewModel.Name);
			var fixedPath = NZLib.Utility.FixText(text: ViewModel.Path);

			var foundedAny = await DatabaseContext.ApplicationHandlers
				.Where(current => current.Id != ViewModel.Id)
				.Where(current => current.Name.ToLower() == fixedName.ToLower())
				.Where(current => current.Path.ToLower() == fixedPath.ToLower())
				.AnyAsync();

			if (foundedAny)
			{
				var errorMessage = string.Format(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.ApplicationHandler);

				AddPageError(message: errorMessage);

				return Page();
			}

			var fixedTitle = NZLib.Utility.FixText(text: ViewModel.Title);
			var fixedDescription = NZLib.Utility.FixText(text: ViewModel.Description);

			foundedItem.SetUpdateDateTime();
			foundedItem.Name = fixedName;
			foundedItem.Path = fixedPath;
			foundedItem.Title = fixedTitle;
			foundedItem.Ordering = ViewModel.Ordering;
			foundedItem.IsActive = ViewModel.IsActive;
			foundedItem.Description = fixedDescription;
			foundedItem.AccessType = ViewModel.AccessType;

			var affectedRows = await DatabaseContext.SaveChangesAsync();

			var successMessage = string.Format(format: Resources.Messages.Successes.Updated,
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
