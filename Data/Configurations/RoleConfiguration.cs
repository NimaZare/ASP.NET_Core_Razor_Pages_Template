using Microsoft.EntityFrameworkCore;

namespace Data.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Domain.Role>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Role> builder)
	{
		builder.HasIndex(current => new { current.Name }).IsUnique(unique: true);

		builder.HasMany(current => current.Users)
			.WithOne(other => other.Role)
			.IsRequired(required: false)
			.HasForeignKey(other => other.RoleId)
			.OnDelete(deleteBehavior: DeleteBehavior.NoAction);

		builder.HasMany(current => current.Permissions)
			.WithOne(other => other.Role)
			.IsRequired(required: false)
			.HasForeignKey(other => other.RoleId)
			.OnDelete(deleteBehavior: DeleteBehavior.NoAction);

	}
}
