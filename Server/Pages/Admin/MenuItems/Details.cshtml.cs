namespace Server.Pages.Admin.MenuItems;

[Authorize(Roles = Constants.Role.Admin)]
public class DetailsModel : Infrastructure.BasePageModelWithDatabaseContext
{
	public DetailsModel(Data.DatabaseContext databaseContext, ILogger<DetailsModel> logger) : base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}

	private ILogger<DetailsModel> Logger { get; }
	
	public ViewModels.Pages.Admin.MenuItems.DetailsOrDeleteViewModel ViewModel { get; set; }
	
	public async Task<IActionResult> OnGetAsync(Guid? id)
	{
		try
		{
			if (id.HasValue == false)
			{
				AddToastError(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}

			ViewModel = await DatabaseContext.MenuItems
				.Where(current => current.Id == id.Value)
				.Select(current => new ViewModels.Pages.Admin.MenuItems.DetailsOrDeleteViewModel
				{
					Id = current.Id,
					Link = current.Link,
					Icon = current.Icon,
					Title = current.Title,
					Ordering = current.Ordering,
					IsPublic = current.IsPublic,
					IsActive = current.IsActive,
					ParentTitle = current.Parent.Title,
					IsDeleted = current.IsDeleted,
					IconPosition = current.IconPosition,
					IsUndeletable = current.IsUndeletable,
					UpdateDateTime = current.UpdateDateTime,
					InsertDateTime = current.InsertDateTime,
					NumberOfSubMenus = current.SubMenus.Count
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
