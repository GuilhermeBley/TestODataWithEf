using Microsoft.EntityFrameworkCore;
using TestODataWithEf.DotNet6.Model;

namespace TestODataWithEf.DotNet6.Context
{
    internal class FakeContext
        : DbContext
    {
        public DbSet<FakeModel> Fakes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase("fake-database");
        }
    }
}
