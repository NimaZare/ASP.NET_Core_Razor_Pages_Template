using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.MenuItems;

public class UpdateViewModel : CreateViewModel
{
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid? Id { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Parent))]
	public string? ParentTitle { get; set; }

}
