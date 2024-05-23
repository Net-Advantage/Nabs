﻿namespace Nabs.Tests.TestDatabaseUnitTests.TenantDatabase;

public sealed class CommentEntity : EntityBase<Guid>, ITenantableEntity
{
    public string Comments { get; set; } = string.Empty;
}
