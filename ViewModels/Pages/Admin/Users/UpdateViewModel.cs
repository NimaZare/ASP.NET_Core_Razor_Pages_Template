namespace ViewModels.Pages.Admin.Users;

public class UpdateViewModel : CommonViewModel
{
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid Id { get; set; }
}
