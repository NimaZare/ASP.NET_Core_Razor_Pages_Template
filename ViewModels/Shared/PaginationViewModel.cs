namespace ViewModels.Shared;

public class PaginationViewModel
{
	public int PageSize { get; set; }

	public int TotalCount { get; set; }

	public int PageNumber { get; set; }

	public int PageCount
	{
		get
		{
			int result = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TotalCount) / PageSize));
			return result;
		}
	}
}
