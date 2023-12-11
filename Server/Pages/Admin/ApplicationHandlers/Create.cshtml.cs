namespace Server.Pages.Admin.ApplicationHandlers;

[Authorize(Roles = Constants.Role.Admin)]
public class CreateModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public CreateModel(Data.DatabaseContext databaseContext, ILogger<CreateModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}

	private ILogger<CreateModel> Logger { get; }


	[BindProperty]
	public ViewModels.Pages.Admin.ApplicationHandlers.CreateViewModel ViewModel { get; set; }

	public IActionResult OnGet()
	{
		return Page();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		try
		{
			var fixedName = NZLib.Utility.RemoveSpaces(text: ViewModel.Name);
			var fixedPath = NZLib.Utility.RemoveSpaces(text: ViewModel.Path);

			var foundedAny = await DatabaseContext.ApplicationHandlers
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

			var newEntity = new Domain.ApplicationHandler(name: fixedName, path: fixedPath)
			{
				Title = fixedTitle,
				Ordering = ViewModel.Ordering,
				IsActive = ViewModel.IsActive,
				Description = fixedDescription,
				AccessType = ViewModel.AccessType
			};

			var entityEntry = await DatabaseContext.AddAsync(entity: newEntity);
			var affectedRows = await DatabaseContext.SaveChangesAsync();

			var successMessage = string.Format(format: Resources.Messages.Successes.Created,
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
