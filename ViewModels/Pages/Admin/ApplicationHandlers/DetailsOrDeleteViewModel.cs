using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.ApplicationHandlers;

public class DetailsOrDeleteViewModel : UpdateViewModel
{
	public DetailsOrDeleteViewModel() : base()
	{
		Permissions = new Shared.KeyValueViewModel();
	}


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InsertDateTime))]
	public DateTime InsertDateTime { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]
	public DateTime UpdateDateTime { get; set; }
	

	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AccessType))]
	public string AccessTypeTitle
	{
		get
		{
			string title = string.Empty;

			switch (AccessType)
			{
				case Domain.Enumerations.AccessType.Special:
				{
					title = Resources.DataDictionary.Special;
					break;
				}
				case Domain.Enumerations.AccessType.Registered:
				{
					title = Resources.DataDictionary.Registered;
					break;
				}
				case Domain.Enumerations.AccessType.Public:
				{
					title = Resources.DataDictionary.Public;
					break;
				}
			}

			return title;
		}
	}


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Permissions))]
	public Shared.KeyValueViewModel Permissions { get; set; }
	
}
