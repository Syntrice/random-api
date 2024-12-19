using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RandomAPI.Options;

namespace RandomAPI.Model
{
    public class RandomDbContext : DbContext
    {
        private DatabaseOptions _databaseOptions;

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
