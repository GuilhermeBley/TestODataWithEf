using Microsoft.EntityFrameworkCore;
using TestODataWithEf.DotNet6.Model;

namespace TestODataWithEf.DotNet6.Context
{
    public class FakeContext
        : DbContext
    {
        public DbSet<FakeModel> Fakes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase("fake-database");
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
