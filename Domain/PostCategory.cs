using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public class PostCategory :
		Seedwork.Entity,
		NZLib.Seedwork.Abstractions.IEntityHasIsActive,
		NZLib.Seedwork.Abstractions.IEntityHasIsUndeletable,
		NZLib.Seedwork.Abstractions.IEntityHasUpdateDateTime
	{

		public PostCategory(string title)
		{
			Title = title;
			UpdateDateTime = InsertDateTime;
			SubCategories = new List<PostCategory>();
		}


		[Display(Name = nameof(Resources.DataDictionary.Parent),
			ResourceType = typeof(Resources.DataDictionary))]
		public virtual PostCategory? Parent { get; set; }
		
		
		[Display(Name = nameof(Resources.DataDictionary.Parent),
			ResourceType = typeof(Resources.DataDictionary))]
		public Guid? ParentId { get; set; }
		

		[Display(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.Title))]
		public string? Title { get; set; }
		

		[Display(Name = nameof(Resources.DataDictionary.IsActive),
			ResourceType = typeof(Resources.DataDictionary))]
		public bool IsActive { get; set; }
		

		[Display(ResourceType = typeof(Resources.DataDictionary),
			Name = nameof(Resources.DataDictionary.Description))]
		public string? Description { get; set; }
		

		[Display(Name = nameof(Resources.DataDictionary.UpdateDateTime),
			ResourceType = typeof(Resources.DataDictionary))]
		public DateTime UpdateDateTime { get; private set; }
		

		[Display(Name = nameof(Resources.DataDictionary.IsUndeletable),
			ResourceType = typeof(Resources.DataDictionary))]
		public bool IsUndeletable { get; set; }
		

		public virtual IList<PostCategory> SubCategories { get; set; }
		

		public void SetUpdateDateTime()
		{
			UpdateDateTime = Seedwork.Utility.Now;
		}
	}
}
