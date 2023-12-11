using Microsoft.EntityFrameworkCore;

namespace Data.Configurations;

internal class UserLoginConfiguration : IEntityTypeConfiguration<Domain.LoginLog>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.LoginLog> builder)
	{
		builder.Property(current => current.UserIP).IsUnicode(unicode: false);

		builder.HasIndex(current => new { current.UserIP }).IsUnique(unique: false);
		
	}
}
