using System.ComponentModel.DataAnnotations;

namespace Domain;

public class LoginLog : Seedwork.Entity
{
	public LoginLog(Guid userId, string userIP) : base()
	{
		UserIP = userIP;
		UserId = userId;
	}

	
	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]

	[Required(ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public Guid UserId { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]
	public virtual User? User { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IP))]

	[Required(ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[MaxLength(length: Constants.MaxLength.IP,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[RegularExpression(pattern: Constants.RegularExpression.IP,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.IP))]
	public string UserIP { get; set; }

}
