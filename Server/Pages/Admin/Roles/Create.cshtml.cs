namespace Server.Pages.Admin.Roles;

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
	public ViewModels.Pages.Admin.Roles.CreateViewModel ViewModel { get; set; }

	public IActionResult OnGet()
	{
		// Note: If you want to change default value!
		//ViewModel.Ordering = 100;

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
			var fixedName = NZLib.Utility.FixText(text: ViewModel.Name);

			var foundedAny = await DatabaseContext.Roles
				.Where(current => current.Name.ToLower() == fixedName.ToLower())
				.AnyAsync();

			if (foundedAny)
			{
				var errorMessage = string.Format(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.Name);

				AddPageError(message: errorMessage);

				return Page();
			}

			var fixedDescription = NZLib.Utility.FixText(text: ViewModel.Description);

			var newEntity = new Domain.Role(name: fixedName)
			{
				Ordering = ViewModel.Ordering,
				IsActive = ViewModel.IsActive,
				Description = fixedDescription
			};

			var entityEntry = await DatabaseContext.AddAsync(entity: newEntity);
			var affectedRows = await DatabaseContext.SaveChangesAsync();

			var successMessage = string.Format(format: Resources.Messages.Successes.Created,
				arg0: Resources.DataDictionary.Role);

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
