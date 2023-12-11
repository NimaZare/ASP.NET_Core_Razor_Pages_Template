namespace Infrastructure.Messages;

public static class Utility
{
	public static bool AddMessage(ITempDataDictionary tempData, MessageType type, string? message)
	{
		message = NZLib.Utility.FixText(text: message);

		if (message == null)
		{
			return false;
		}

		List<string>? list;
		var tempDataItems = (tempData[key: type.ToString()] as IList<string>);

		if (tempDataItems == null)
		{
			list = new List<string>();
		}
		else
		{
			list = tempDataItems as List<string>;
			list ??= tempDataItems.ToList();
		}

		tempData[key: type.ToString()] = list;

		if (list.Contains(item: message))
		{
			return false;
		}

		list.Add(item: message);

		return true;
	}
}
