using System.Data.Common;
using System.Threading;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Nabs.Tests.PersistenceTests.DbContexts;

public class TestDbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var a = 10; //TODO: DWS: What you gonna do here?

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}