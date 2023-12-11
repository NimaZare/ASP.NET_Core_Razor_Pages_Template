namespace Infrastructure.Settings;

public class TablesDefaultSettings
{
	public TablesDefaultSettings()
	{
		DisplayDateTimeFormat = "yyyy/MM/dd [HH:mm:ss]";

		TableHeaderStyle = "table-primary";
		TableFooterStyle = "table-secondary";
		TableStyle = "table table-bordered table-sm table-striped table-hover";
	}


	public string? NoIcon { get; set; }

	public string? YesIcon { get; set; }

	public string TableStyle { get; set; }

	public string? NextPageIcon { get; set; }

	public string TableHeaderStyle { get; set; }

	public string TableFooterStyle { get; set; }

	public string? PreviousPageIcon { get; set; }
	public string DisplayDateTimeFormat { get; set; }

}
