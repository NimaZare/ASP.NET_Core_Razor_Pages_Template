namespace ViewModels.Pages.Admin.ApplicationHandlers;

public class UpdateViewModel : CreateViewModel
{
	[System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public Guid Id { get; set; }
}
