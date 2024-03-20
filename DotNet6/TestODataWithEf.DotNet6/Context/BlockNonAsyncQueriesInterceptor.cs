using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace TestODataWithEf.DotNet6.Context
{
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
}
