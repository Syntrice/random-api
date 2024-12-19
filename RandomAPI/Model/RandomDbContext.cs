using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RandomAPI.Options;
using System.Diagnostics.CodeAnalysis;

namespace RandomAPI.Model
{
    public class RandomDbContext : DbContext
    {
        private DatabaseOptions _databaseOptions;

        public DbSet<Fruit> Fruits { get; set; } = null!;

        public RandomDbContext(IOptions<DatabaseOptions> options)
        {
            _databaseOptions = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_databaseOptions.DatabasePath}");
        }
    }
}
