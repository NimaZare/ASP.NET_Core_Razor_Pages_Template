namespace ViewModels.Pages.Admin.ApplicationHandlers;

public class IndexItemViewModel
{
	public IndexItemViewModel() : base()
	{
		Name = string.Empty;
		Path = string.Empty;
	}

	public Guid Id { get; set; }

	public bool IsActive { get; set; }

	public string Name { get; set; }

	public string Path { get; set; }

	public string? Title { get; set; }

	public int Ordering { get; set; }

	public DateTime InsertDateTime { get; set; }

	public DateTime UpdateDateTime { get; set; }

}
