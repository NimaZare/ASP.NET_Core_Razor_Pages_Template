namespace ViewModels.Shared;

public class KeyValueViewModel : object
{
	public KeyValueViewModel() : base()
	{
	}

	public KeyValueViewModel(Guid? id, string? name) : base()
	{
		Id = id;
		Name = name;
	}

	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public Guid? Id { get; set; }


	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
	public string? Name { get; set; }
	
}
