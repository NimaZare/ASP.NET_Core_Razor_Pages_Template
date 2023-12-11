namespace NZLib.Seedwork.Abstractions;

public interface IEntityHasIsDeleted
{
	bool IsDeleted { get; set; }

	DateTime DeleteDateTime { get; }

	void SetDeleteDateTime();
}
