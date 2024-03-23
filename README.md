# Test OData and EF Core async operations

This repository will be check if OData in .Net 6.0 executes their queries async with Entity Framework.

Firstly, we need to think of a way to verify when the non-async operations are executed, so, in this case, we need to create an interceptor to check it:

```csharp
public class BlockNonAsyncQueriesInterceptor : DbCommandInterceptor
{
    private const string SyncCodeError = @"Synchronous code is not allowed for performance reasons.";
    private readonly ILogger _logger;

    public BlockNonAsyncQueriesInterceptor(ILogger logger)
    {
        _logger = logger;
    }

    public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Ok NonQueryExecutingAsync");

        return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
    {
        _logger.LogError("Error NonQueryExecuting");

        throw new InvalidOperationException(SyncCodeError);
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Ok ReaderExecutingAsync");

        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        _logger.LogError("Error ReaderExecuting");

        throw new InvalidOperationException(SyncCodeError);
    }

    public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Ok ScalarExecutingAsync");

        return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
    {
        _logger.LogError("Error ScalarExecuting");

        throw new InvalidOperationException(SyncCodeError);
    }
}
```

Ok, after creating and applying this interceptor, let's configure a database that won't be the 'InMemory', because this kind of database won't trigger this interceptor.

In the database, we will need a fake table with his respective data to try to collect the data.

Everything is configured, so all that we need to do is execute this queryable.

Executing the `IQueryable` we faced this output console message: `Ok ReaderExecutingAsync`. It means that the OData is already compatible to execute queries async with Entity Framework.
