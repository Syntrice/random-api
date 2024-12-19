using RandomAPI.Model;
using RandomAPI.Options;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = Build(args);

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

        return builder.Build();
    }
}