namespace Constants;

public static class Format
{
	public static string NullValue => "-----";

	public static string Integer => "#,##0";

	public static string Time => "HH:mm:ss";

	public static string Date => "yyyy/MM/dd";

	public static string DateTime => $"{Date} - {Time}";
}
