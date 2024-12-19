using Microsoft.EntityFrameworkCore;
using RandomAPI.Model;
using RandomAPI.Options;
using RandomAPI.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = Build(args);

        MigrateDatabase(app);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();


        app.Run();
    }

    private static WebApplication Build(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.SectionName));
        builder.Services.AddDbContext<RandomDbContext>();
        builder.Services.AddScoped<FruitsService>();


        return builder.Build();
    }

    private static void MigrateDatabase(WebApplication app)
    {
        // contrived example, maybe change this later
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<RandomDbContext>();
            context.Database.Migrate();
        }
    }
}