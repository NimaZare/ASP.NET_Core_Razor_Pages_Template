using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Account;

public class LoginViewModel
{
	[Display(Name = nameof(Resources.DataDictionary.Username),
		ResourceType = typeof(Resources.DataDictionary))]

	[Required(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[MaxLength(length: Constants.MaxLength.Username,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[RegularExpression(pattern: Constants.RegularExpression.Username,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Username))]
	public string? Username { get; set; }


	[Display(Name = nameof(Resources.DataDictionary.Password),
		ResourceType = typeof(Resources.DataDictionary))]

	[Required(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[MaxLength(length: Constants.MaxLength.Password,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[RegularExpression(pattern: Constants.RegularExpression.Password,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Password))]

	[DataType(dataType: DataType.Password)]
	public string? Password { get; set; }
	

	[Display(Name = nameof(Resources.DataDictionary.RememberMe),
		ResourceType = typeof(Resources.DataDictionary))]
	public bool RememberMe { get; set; }

}
