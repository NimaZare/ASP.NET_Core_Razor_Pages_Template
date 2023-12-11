using System.ComponentModel.DataAnnotations;

namespace Domain;

public class PageCategory :
	Seedwork.Entity,
	NZLib.Seedwork.Abstractions.IEntityHasIsActive,
	NZLib.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	public PageCategory(string name)
	{
		Name = name;
		UpdateDateTime = InsertDateTime;
		Pages = new List<Page>();
	}

	
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	
	
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public DateTime UpdateDateTime { get; private set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]

	[Required(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[MaxLength(length: Constants.MaxLength.Name,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string Name { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	

	public void SetUpdateDateTime()
	{
		UpdateDateTime = NZLib.Utility.Now;
	}


	public virtual IList<Page> Pages { get; private set; }

}
