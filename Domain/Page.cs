using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Page :
	Seedwork.Entity,
	NZLib.Seedwork.Abstractions.IEntityHasIsActive,
	NZLib.Seedwork.Abstractions.IEntityHasIsSystemic,
	NZLib.Seedwork.Abstractions.IEntityHasIsUnupdatable,
	NZLib.Seedwork.Abstractions.IEntityHasIsUndeletable,
	NZLib.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	public const byte NameMaxLength = 100;
	public const byte TitleMaxLength = 100;
	public const byte AuthorMaxLength = 200;
	public const byte CategoryMaxLength = 200;
	public const byte DescriptionMaxLength = 100;
	public const int ImageUrlMaxLength = 4000;
	public const int CopyrightMaxLength = 1000;
	public const int IntroductionMaxLength = 4000;
	public const int ClassificationMaxLength = 1000;

	
	public Page(string title, Guid pageCategoryId, Guid creatorUserId)
	{
		Title = title;
		CreatorUserId = creatorUserId;
		PageCategoryId = pageCategoryId;
		UpdateDateTime = InsertDateTime;
	}

	
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CreatorUser))]
	public Guid CreatorUserId { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CreatorUser))]
	public virtual User? CreatorUser { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PageCategory))]
	public Guid PageCategoryId { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]

	[Required(ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual PageCategory? PageCategory { get; set; }
	
	
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]

	[MaxLength(length: NameMaxLength,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string Name { get; set; }


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Layout))]
	public Guid LayoutId { get; set; }
	
	
	[Required(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]
	public string? Title { get; set; }
	
	
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Password))]
	public string? Password { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsFeatured))]
	public bool IsFeatured { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.HasAttachment))]
	public bool HasAttachment { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DownloadCount))]
	public int DownloadCount { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Introduction))]
	public string? Introduction { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsCommentingEnabled))]
	public bool IsCommentingEnabled { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImageUrl))]
	public string? ImageUrl { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Body))]
	public string? Body { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DisplayCreatorUser))]
	public bool DisplayCreatorUser { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DoesSearchEnginesIndexIt))]
	public bool DoesSearchEnginesIndexIt { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DoesSearchEnginesFollowIt))]
	public bool DoesSearchEnginesFollowIt { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Author))]
	public string? Author { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Classification))]
	public string? Classification { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Copyright))]
	public string? Copyright { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Hits))]
	public int Hits { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PublishStartDateTime))]
	public DateTime? PublishStartDateTime { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PublishFinishDateTime))]
	public DateTime? PublishFinishDateTime { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsUpdatable))]
	public bool IsUnupdatable { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]
	public DateTime UpdateDateTime { get; private set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsUndeletable))]
	public bool IsUndeletable { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsSystemic))]
	public bool IsSystemic { get; set; }
	

	public string? Category { get; set; }



	public void SetUpdateDateTime()
	{
		UpdateDateTime = Seedwork.Utility.Now;
	}
}
