namespace Server.Pages.Admin.Users;

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
	public ViewModels.Pages.Admin.Users.CreateViewModel ViewModel { get; set; }

	public SelectList? RolesSelectList { get; set; }

	public async Task<IActionResult> OnGetAsync()
	{
		try
		{
			RolesSelectList = await Infrastructure.SelectLists.GetRolesAsync
				(databaseContext: DatabaseContext, selectedValue: null);

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
		try
		{
			RolesSelectList = await Infrastructure.SelectLists.GetRolesAsync
				(databaseContext: DatabaseContext, selectedValue: ViewModel.RoleId);

			if (ModelState.IsValid == false)
			{
				return Page();
			}

			var fixedEmailAddress = NZLib.Utility.FixText(text: ViewModel.EmailAddress);

			var foundedAny = await DatabaseContext.Users
				.Where(current => current.EmailAddress.ToLower() == fixedEmailAddress.ToLower())
				.AnyAsync();

			if (foundedAny)
			{
				var key = $"{nameof(ViewModel)}.{nameof(ViewModel.EmailAddress)}";

				var errorMessage = string.Format(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.EmailAddress);

				ModelState.AddModelError(key: key, errorMessage: errorMessage);
			}

			var fixedUsername = NZLib.Utility.FixText(text: ViewModel.Username);

			if (fixedUsername != null)
			{
				foundedAny = await DatabaseContext.Users
					.Where(current => current.Username.ToLower() == fixedUsername.ToLower())
					.AnyAsync();

				if (foundedAny)
				{
					var key = $"{nameof(ViewModel)}.{nameof(ViewModel.Username)}";

					var errorMessage = string.Format(format: Resources.Messages.Errors.AlreadyExists,
						arg0: Resources.DataDictionary.Username);

					ModelState.AddModelError(key: key, errorMessage: errorMessage);
				}
			}

			var fixedCellPhoneNumber = NZLib.Utility.FixText(text: ViewModel.CellPhoneNumber);

			if (fixedCellPhoneNumber != null)
			{
				foundedAny = await DatabaseContext.Users
					.Where(current => current.CellPhoneNumber == fixedCellPhoneNumber)
					.AnyAsync();

				if (foundedAny)
				{
					var key = $"{nameof(ViewModel)}.{nameof(ViewModel.CellPhoneNumber)}";

					var errorMessage = string.Format(format: Resources.Messages.Errors.AlreadyExists,
						arg0: Resources.DataDictionary.CellPhoneNumber);

					ModelState.AddModelError(key: key, errorMessage: errorMessage);
				}
			}

			if (ModelState.IsValid == false)
			{
				return Page();
			}

			var fixedFirstName = NZLib.Utility.FixText(text: ViewModel.FirstName);
			var fixedLastName = NZLib.Utility.FixText(text: ViewModel.LastName);
			var fixedDescription = NZLib.Utility.FixText(text: ViewModel.Description);
			var fixedAdminDescription = NZLib.Utility.FixText(text: ViewModel.AdminDescription);
			var fixedTitleInContactUsPage = NZLib.Utility.FixText(text: ViewModel.TitleInContactUsPage);
			var hashOfPassword = NZLib.Security.Hashing.GetSha256(text: ViewModel.Password);

			var newEntity = new Domain.User(emailAddress: fixedEmailAddress)
			{
				RoleId = ViewModel.RoleId,
				Ordering = ViewModel.Ordering,

				IsSystemic = false,
				IsUndeletable = false,

				IsActive = ViewModel.IsActive,
				IsProgrammer = ViewModel.IsProgrammer,
				IsProfilePublic = ViewModel.IsProfilePublic,
				IsEmailAddressVerified = ViewModel.IsEmailAddressVerified,
				IsVisibleInContactUsPage = ViewModel.IsVisibleInContactUsPage,
				IsCellPhoneNumberVerified = ViewModel.IsCellPhoneNumberVerified,

				Password = hashOfPassword,
				Username = fixedUsername,
				LastName = fixedLastName,
				FirstName = fixedFirstName,
				Description = fixedDescription,
				CellPhoneNumber = fixedCellPhoneNumber,
				AdminDescription = fixedAdminDescription,
				TitleInContactUsPage = fixedTitleInContactUsPage,
			};

			var entityEntry = await DatabaseContext.AddAsync(entity: newEntity);
			var affectedRows = await DatabaseContext.SaveChangesAsync();

			var successMessage = string.Format(format: Resources.Messages.Successes.Created,
				arg0: Resources.DataDictionary.User);

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
