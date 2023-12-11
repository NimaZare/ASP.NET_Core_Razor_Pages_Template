using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.PageCategories;

public class DetailsOrDeleteViewModel : UpdateViewModel
{
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PageCount))]
	public int PageCount { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InsertDateTime))]
	public DateTime InsertDateTime { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]
	public DateTime UpdateDateTime { get; set; }

}
