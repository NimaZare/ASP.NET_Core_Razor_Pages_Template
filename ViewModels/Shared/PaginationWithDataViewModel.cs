namespace ViewModels.Shared;

public class PaginationWithDataViewModel<T>
{
	public PaginationWithDataViewModel() : base()
	{
		PageInformation = new();
		Data = new List<T>();
	}

	public PaginationViewModel PageInformation { get; set; }

	public IList<T> Data { get; set; }
}
