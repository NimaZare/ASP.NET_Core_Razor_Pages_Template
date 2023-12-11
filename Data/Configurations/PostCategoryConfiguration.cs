using Microsoft.EntityFrameworkCore;

namespace Data.Configurations;

internal class PostCategoryConfiguration : IEntityTypeConfiguration<Domain.PostCategory>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.PostCategory> builder)
	{
		builder.Property(current => current.Title)
			.HasMaxLength(maxLength: Constants.MaxLength.Title)
			.IsRequired()
			.IsUnicode(true);

		builder.HasMany(current => current.SubCategories)
			.WithOne(other => other.Parent)
			.IsRequired(required: false)
			.HasForeignKey(other => other.ParentId)
			.OnDelete(deleteBehavior: DeleteBehavior.NoAction);

		builder.HasIndex(current => new { current.Ordering })
			.IsClustered(clustered: false)
			.IsUnique(unique: false);
		
		builder.HasIndex(current => new { current.ParentId, current.Title }).IsUnique(unique: true);

	}
}
