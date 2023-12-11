using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Users;

public class IndexItemViewModel
{
	public IndexItemViewModel() : base()
	{
		EmailAddress = string.Empty;
	}


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Role))]
	public string? RoleName { get; set; }

	
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool? IsRoleActive { get; init; }


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsUndeletable))]
	public bool IsUndeletable { get; set; }


	public Guid Id { get; set; }

	public Guid? RoleId { get; set; }

	public DateTime UpdateDateTime { get; set; }

	public DateTime InsertDateTime { get; set; }

	public DateTime? LastLoginDateTime { get; set; }

	public bool IsActive { get; set; }

	public bool IsProgrammer { get; set; }

	public bool IsEmailAddressVerified { get; set; }

	public bool IsVisibleInContactUsPage { get; set; }

	public bool IsCellPhoneNumberVerified { get; set; }

	public string? Username { get; set; }

	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public string EmailAddress { get; set; }

	public string? CellPhoneNumber { get; set; }

}
