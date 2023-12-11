using Microsoft.EntityFrameworkCore;

namespace Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<Domain.User>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.User> builder)
	{
		builder.Property(current => current.EmailAddress).IsUnicode(unicode: false);

		builder.HasIndex(current => new { current.EmailAddress }).IsUnique(unique: true);

		builder.HasIndex(current => new { current.EmailAddressVerificationKey }).IsUnique(unique: true);

		builder.Property(current => current.Username).IsUnicode(unicode: false);

		builder.HasIndex(current => new { current.Username }).IsUnique(unique: true);

		builder.Property(current => current.CellPhoneNumber).IsUnicode(unicode: false);

		builder.HasIndex(current => new { current.CellPhoneNumber }).IsUnique(unique: true);

		builder.Property(current => current.CellPhoneNumberVerificationKey).IsUnicode(unicode: false);

		builder.HasIndex(current => new { current.CellPhoneNumberVerificationKey }).IsUnique(unique: true);

		builder.Property(current => current.Password).IsUnicode(unicode: false);

		builder.HasMany(current => current.CreatedPages)
			.WithOne(other => other.CreatorUser)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CreatorUserId)
			.OnDelete(deleteBehavior: DeleteBehavior.Cascade);

		builder.HasMany(current => current.LoginLogs)
			.WithOne(other => other.User)
			.IsRequired(required: true)
			.HasForeignKey(other => other.UserId)
			.OnDelete(deleteBehavior: DeleteBehavior.Cascade);

		var user = new Domain.User(emailAddress: "info@nimazare.org")
		{
			Ordering = 1,

			IsActive = true,
			IsSystemic = true,
			IsProgrammer = true,
			IsUndeletable = true,
			IsProfilePublic = true,
			IsEmailAddressVerified = true,
			IsVisibleInContactUsPage = true,
			IsCellPhoneNumberVerified = true,

			Description = null,
			AdminDescription = null,
			LastLoginDateTime = null,

			Username = "nimazare",
			FirstName = "نیما",
			LastName = "زارع",
			CellPhoneNumber = "09351551295",
			TitleInContactUsPage = "مالک سایت",

			Password = NZLib.Security.Hashing.GetSha256(text: "1DC18EFA4160456EBFEBA1CD66A0DFD3")
		};

		user.SetId(id: Domain.User.SuperUserId);

		builder.HasData(data: user);

	}
}
