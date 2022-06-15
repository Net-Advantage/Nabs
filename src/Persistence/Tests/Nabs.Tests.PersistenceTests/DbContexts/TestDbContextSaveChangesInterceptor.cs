using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;

namespace Nabs.Tests.PersistenceTests.DbContexts;

public class TestDbContextSaveChangesInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default)
	{
		Console.WriteLine(nameof(TestDbContextSaveChangesInterceptor));

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}