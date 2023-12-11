using Microsoft.EntityFrameworkCore;

namespace Data.Configurations;

internal class PermissionConfiguration : IEntityTypeConfiguration<Domain.Permission>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Permission> builder)
	{
		builder.HasIndex(current => new { current.RoleId, current.ApplicationHandlerId }).IsUnique(unique: true);

		builder.HasOne(current => current.Role)
			.WithMany(other => other.Permissions)
			.HasForeignKey(current => current.RoleId)
			.OnDelete(DeleteBehavior.NoAction);

		builder
			.HasOne(current => current.ApplicationHandler)
			.WithMany(other => other.Permissions)
			.HasForeignKey(current => current.ApplicationHandlerId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
