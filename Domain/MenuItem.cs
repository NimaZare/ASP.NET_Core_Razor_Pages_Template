using System.ComponentModel.DataAnnotations;

namespace Domain;

public class MenuItem :
	Seedwork.Entity,
	NZLib.Seedwork.Abstractions.IEntityHasIsActive,
	NZLib.Seedwork.Abstractions.IEntityHasIsUndeletable,
	NZLib.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	public MenuItem(string title)
	{
		Title = title;
		SetUpdateDateTime();
		SubMenus = new List<MenuItem>();
	}


	[Display(Name = nameof(Resources.DataDictionary.MenuItem),
		ResourceType = typeof(Resources.DataDictionary))]
	public Guid? ParentId { get; set; }
	

	[Required(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[Display(Name = nameof(Resources.DataDictionary.Title),
		ResourceType = typeof(Resources.DataDictionary))]

	[MaxLength(length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Title { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.Link),
		ResourceType = typeof(Resources.DataDictionary))]

	[MaxLength(length: Constants.MaxLength.Link,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Link { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.IsActive),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsActive { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.IsPublic),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsPublic { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.IsUndeletable),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsUndeletable { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.IsDeleted),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool IsDeleted { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.Icon),
		ResourceType = typeof(Resources.DataDictionary))]

	[MaxLength(length: Constants.MaxLength.Icon,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Icon { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.IconPosition),
		ResourceType = typeof(Resources.DataDictionary))]
	public Enumerations.IconPosition? IconPosition { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.UpdateDateTime),
		ResourceType = typeof(Resources.DataDictionary))]
	public DateTime UpdateDateTime { get; private set; }
	

	public void SetUpdateDateTime()
	{
		UpdateDateTime = Seedwork.Utility.Now;
	}


	public virtual MenuItem? Parent { get; set; }

	public virtual IList<MenuItem> SubMenus { get; set; }

}
