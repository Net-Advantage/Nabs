﻿namespace Nabs.Tests.TestsDatabaseUnitTests.TenantDatabase;

public sealed class SimpleTenantEntity : EntityBase<Guid>, ITenantEntity
{
    public string Name { get; set; } = string.Empty;
    public TenantIsolationStrategy IsolationStrategy { get; set; }
}