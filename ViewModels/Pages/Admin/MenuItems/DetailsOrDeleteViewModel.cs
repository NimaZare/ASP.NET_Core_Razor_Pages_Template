using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.MenuItems;

public class DetailsOrDeleteViewModel : UpdateViewModel
{
	public DetailsOrDeleteViewModel() : base()
	{
		InsertDateTime = Domain.Seedwork.Utility.Now;
		UpdateDateTime = Domain.Seedwork.Utility.Now;
	}


	[Display(Name = nameof(Resources.DataDictionary.SubMenuItems),
		ResourceType = typeof(Resources.DataDictionary))]
	public int NumberOfSubMenus { get; init; }
	

	[Display(Name = nameof(Resources.DataDictionary.IsDeleted),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsDeleted { get; init; }
	

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
