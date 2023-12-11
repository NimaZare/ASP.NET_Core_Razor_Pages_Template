using Microsoft.EntityFrameworkCore;

namespace Data;

public class DatabaseContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public DatabaseContext
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		(DbContextOptions<DatabaseContext> options) : base(options: options)
	{
		Database.EnsureCreated();
	}

	public DbSet<Domain.Role> Roles { get; set; }

	public DbSet<Domain.User> Users { get; set; }

	public DbSet<Domain.Page> Pages { get; set; }

	public DbSet<Domain.LoginLog> LoginLogs { get; set; }

	public DbSet<Domain.MenuItem> MenuItems { get; set; }

	public DbSet<Domain.Permission> Permissions { get; set; }

	public DbSet<Domain.PageCategory> PageCategories { get; set; }

	public DbSet<Domain.ApplicationHandler> ApplicationHandlers { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(Configurations.RoleConfiguration).Assembly);
	}

	protected override void ConfigureConventions(ModelConfigurationBuilder builder)
	{
		builder.Properties<DateOnly>().HaveConversion<Conventions.DateTimeConventions.DateOnlyConverter>()
			.HaveColumnType(typeName: nameof(DateTime.Date));

		builder.Properties<DateOnly?>().HaveConversion<Conventions.DateTimeConventions.NullableDateOnlyConverter>()
			.HaveColumnType(typeName: nameof(DateTime.Date));

	}
}
