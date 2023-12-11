namespace Server.Pages.Admin.PageManager;

[Authorize(Roles = Constants.Role.Admin)]
public class CreateModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public CreateModel(Data.DatabaseContext databaseContext, ILogger<CreateModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}

	private ILogger<CreateModel> Logger { get; }

	[BindProperty]
	public string? ReturnUrl { get; set; }

	public async Task OnGetAsync(string? returnUrl)
	{
		ReturnUrl = returnUrl;

		try
		{
		}
		catch (Exception ex)
		{
			Logger.LogError(message: ex.Message);
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
			if (ModelState.IsValid is false)
			{
				return Page();
			}
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

		if (string.IsNullOrWhiteSpace(value: ReturnUrl))
		{
			return RedirectToPage(pageName: "./Index");
		}
		else
		{
			return Redirect(url: ReturnUrl);
		}
	}
}
