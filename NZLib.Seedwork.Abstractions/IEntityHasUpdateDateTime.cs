namespace NZLib.Seedwork.Abstractions;

public interface IEntityHasUpdateDateTime
{
	DateTime UpdateDateTime { get; }

	void SetUpdateDateTime();
}
