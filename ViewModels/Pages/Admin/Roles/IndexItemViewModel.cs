namespace ViewModels.Pages.Admin.Roles;

public class IndexItemViewModel
{
	public IndexItemViewModel() : base()
	{
		// Note: Just For Ignoring Warning!
		Name = string.Empty;
	}


	public Guid Id { get; set; }
	
	public bool IsActive { get; set; }
	
	public string Name { get; set; }
	
	public int Ordering { get; set; }
	
	public int UserCount { get; set; }
	
	public DateTime InsertDateTime { get; set; }
	
	public DateTime UpdateDateTime { get; set; }
	
}
