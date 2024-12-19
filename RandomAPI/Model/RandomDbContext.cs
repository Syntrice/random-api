using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RandomAPI.Options;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

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

            optionsBuilder.UseSeeding((context, _) =>
            {
                List<Fruit>? seedFruits = JsonSerializer.Deserialize<List<Fruit>>(File.ReadAllText(_databaseOptions.SeedFruitsPath));
                if (seedFruits is null) return;

                var fruits = context.Set<Fruit>();
                fruits.AddRange(seedFruits.Where(seedFruit => !fruits.Any(fruit => fruit.Id == seedFruit.Id)));
                context.SaveChanges();
            });

            optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                List<Fruit>? seedFruits = JsonSerializer.Deserialize<List<Fruit>>(await File.ReadAllTextAsync(_databaseOptions.SeedFruitsPath));
                if (seedFruits is null) return;

                var fruits = context.Set<Fruit>();
                fruits.AddRange(seedFruits.Where(seedFruit => !fruits.Any(fruit => fruit.Id == seedFruit.Id)));
                await context.SaveChangesAsync(cancellationToken);
            });
        }
    }
}
