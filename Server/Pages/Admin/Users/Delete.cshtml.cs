namespace Server.Pages.Admin.Users;

[Authorize(Roles = Constants.Role.Admin)]
public class DeleteModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public DeleteModel(Data.DatabaseContext databaseContext, ILogger<DeleteModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}

	private ILogger<DeleteModel> Logger { get; }

	[BindProperty]
	public ViewModels.Pages.Admin.Users.DetailsOrDeleteViewModel ViewModel { get; private set; }

	public async Task<IActionResult> OnGetAsync(Guid? id)
	{
		try
		{
			if (id.HasValue == false)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}

			ViewModel = await DatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.Select(current => new ViewModels.Pages.Admin.Users.DetailsOrDeleteViewModel
				{
					IsActive = current.IsActive,
					EmailAddress = current.EmailAddress,
					IsRoleActive = current.Role.IsActive,
					CellPhoneNumber = current.CellPhoneNumber,
					IsProfilePublic = current.IsProfilePublic,
					AdminDescription = current.AdminDescription,
					IsEmailAddressVerified = current.IsEmailAddressVerified,
					IsCellPhoneNumberVerified = current.IsCellPhoneNumberVerified
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

	public async Task<IActionResult> OnPostAsync(Guid? id)
	{
		if (id.HasValue == false)
		{
			AddToastError(message: Resources.Messages.Errors.IdIsNull);

			return RedirectToPage(pageName: "Index");
		}

		try
		{
			var foundedItem = await DatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefaultAsync();

			if (foundedItem == null)
			{
				AddToastError(message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

				return RedirectToPage(pageName: "Index");
			}

			if (foundedItem.IsProgrammer)
			{
				var errorMessage = string.Format(Resources.Messages.Errors.UnableTo,
					Resources.ButtonCaptions.Delete, Resources.DataDictionary.User);

				AddToastError(message: errorMessage);

				return RedirectToPage(pageName: "Index");
			}

			if (foundedItem.IsUndeletable)
			{
				var errorMessage = string.Format(Resources.Messages.Errors.UnableTo,
					Resources.ButtonCaptions.Delete, Resources.DataDictionary.User);

				AddToastError(message: errorMessage);

				return RedirectToPage(pageName: "Index");
			}

			var entityEntry = DatabaseContext.Remove(entity: foundedItem);
			var affectedRows = await DatabaseContext.SaveChangesAsync();

			var successMessage = string.Format(Resources.Messages.Successes.Deleted,
				Resources.DataDictionary.Role);

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
