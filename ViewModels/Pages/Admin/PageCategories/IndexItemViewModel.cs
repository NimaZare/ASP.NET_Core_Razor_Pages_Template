namespace ViewModels.Pages.Admin.PageCategories;

public class IndexItemViewModel
{
	public IndexItemViewModel() : base()
	{
		Name = string.Empty;
	}


	public Guid Id { get; set; }
	
	public bool IsActive { get; set; }
	
	public string Name { get; set; }
	
	public int Ordering { get; set; }
	
	public int PageCount { get; set; }
	
	public DateTime InsertDateTime { get; set; }
	
	public DateTime UpdateDateTime { get; set; }
	
}
