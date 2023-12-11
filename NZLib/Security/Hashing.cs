namespace NZLib.Security;

public static class Hashing
{
	public static string GetSha256(string text)
	{
		var inputBytes = System.Text.Encoding.UTF8.GetBytes(s: text);
		var outputBytes = System.Security.Cryptography.SHA256.HashData(source: inputBytes);
		var result = Convert.ToBase64String(inArray: outputBytes);

		return result;
	}
}
