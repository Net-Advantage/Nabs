namespace Nabs.Tests.TestDatabaseUnitTests.Database;

public sealed class SimpleDbContext(DbContextOptions options)
    : BaseDbContext(options)
{
    public DbSet<AllTypesEntity> AllTypesEntities { get; set; } = null!;
}
