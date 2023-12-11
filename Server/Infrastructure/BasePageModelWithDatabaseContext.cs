namespace Infrastructure;

public abstract class BasePageModelWithDatabaseContext : BasePageModel
{
	public BasePageModelWithDatabaseContext(Data.DatabaseContext databaseContext)
	{
		DatabaseContext = databaseContext;
	}

	protected Data.DatabaseContext DatabaseContext { get; }

	protected async Task DisposeDatabaseContextAsync()
	{
		if (DatabaseContext != null)
		{
			await DatabaseContext.DisposeAsync();
		}
	}
}
