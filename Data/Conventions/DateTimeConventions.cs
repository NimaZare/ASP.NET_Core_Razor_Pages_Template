using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Conventions;

internal static class DateTimeConventions
{
	internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
	{
		public DateOnlyConverter() : base(current => current.ToDateTime(TimeOnly.MinValue),
			current => DateOnly.FromDateTime(current))
		{
		}
	}

	internal class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
	{
		public NullableDateOnlyConverter() : base(current => current == null ? null : new System.DateTime?(current.Value.ToDateTime(TimeOnly.MinValue)),
			current => current == null ? null : new System.DateOnly?(DateOnly.FromDateTime(current.Value)))
		{
		}
	}
}
