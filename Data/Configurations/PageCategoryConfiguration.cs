using Microsoft.EntityFrameworkCore;

namespace Data.Configurations;

internal class PageCategoryConfiguration : IEntityTypeConfiguration<Domain.PageCategory>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.PageCategory> builder)
	{
		builder.HasIndex(current => new { current.Name }).IsUnique(unique: true);
	}
}
