using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.ApplicationHandlers;

public class CreateViewModel
{
	public CreateViewModel() : base()
	{
		Ordering = 10_000;
	}


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Ordering))]

	[Required(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[Range(minimum: Constants.MinValue.Ordering, maximum: Constants.MaxValue.Ordering,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int Ordering { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]

	[Required(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[MaxLength(length: Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Name { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]

	[MaxLength(length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Title { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Path))]

	[Required(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[MaxLength(length: Constants.MaxLength.Path,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Path { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]

	[DataType(DataType.MultilineText)]
	public string? Description { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AccessType))]
	public Domain.Enumerations.AccessType AccessType { get; set; }
	
}
