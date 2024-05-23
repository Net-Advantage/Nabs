namespace Nabs.Persistence;

public abstract class TenantableDbContext<TTenantEntity>(
    DbContextOptions options,
    IApplicationContext applicationContext)
    : DbContext(options), ITenantableDbContext
    where TTenantEntity : class, ITenantEntity
{
    public IApplicationContext ApplicationContext { get; } = applicationContext;

    public DbSet<TTenantEntity> Tenants => Set<TTenantEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantableEntity).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddTenantEntityQueryFilter(this);
            }

            if (typeof(TTenantEntity) == entityType.ClrType)
            {
                Expression<Func<TTenantEntity, bool>> filter = entity =>
                    EF.Property<Guid>(entity, "Id") == ApplicationContext.TenantContext.TenantId;

                entityType.SetQueryFilter(filter);
            }
        }
    }

    public override int SaveChanges()
    {
        SetTenantId();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTenantId();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetTenantId()
    {
        var entries = ChangeTracker.Entries()
                        .Where(e => e.Entity is ITenantableEntity &&
                                    e.State == EntityState.Added ||
                                    e.State == EntityState.Modified);
        if (entries.Any())
        {
            if (ApplicationContext.TenantContext.TenantId == Guid.Empty)
            {
                throw new InvalidOperationException("TenantId is not set.");
            }
        }

        foreach (var entry in entries)
        {
            entry.Property(nameof(TenantId)).CurrentValue = ApplicationContext.TenantContext.TenantId;
        }
    }
}
