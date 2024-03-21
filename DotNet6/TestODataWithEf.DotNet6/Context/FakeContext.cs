using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestODataWithEf.DotNet6.Model;

namespace TestODataWithEf.DotNet6.Context
{
    public class FakeContext
        : DbContext
    {
        private readonly ILogger<FakeContext> _logger;
        private readonly IOptions<MySqlContextConfig> _options;

        public DbSet<FakeModel> Fakes { get; set; } = null!;

        public FakeContext(
            ILogger<FakeContext> logger,
            IOptions<MySqlContextConfig> options)
        {
            _logger = logger;
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(
                ServerVersion.AutoDetect(_options.Value.ConnectionString));
            optionsBuilder.AddInterceptors(new BlockNonAsyncQueriesInterceptor(_logger));
        }

        public static async Task PopulateAsync(
            IServiceProvider provider)
        {
            await using var scope = provider.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<FakeContext>();

            foreach(var index in Enumerable.Range(0, 100))
            {
                await context.Fakes.AddAsync(new());
            }

            await context.SaveChangesAsync();
        }
    }
}
