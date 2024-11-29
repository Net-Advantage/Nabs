namespace Nabs.Tests.TestsDatabaseUnitTests.Database;

public sealed class SimpleDbContext(DbContextOptions options)
    : BaseDbContext(options)
{
    public DbSet<AllTypesEntity> AllTypesEntities { get; set; } = null!;
}
