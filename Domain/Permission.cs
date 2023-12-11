using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Permission :
	Seedwork.Entity,
	NZLib.Seedwork.Abstractions.IEntityHasIsActive
{

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]
	public Guid RoleId { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]
	public virtual Role? Role { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
	Name = nameof(Resources.DataDictionary.ApplicationHandler))]
	public Guid ApplicationHandlerId { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
	Name = nameof(Resources.DataDictionary.ApplicationHandler))]
	public virtual ApplicationHandler? ApplicationHandler { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }

}
