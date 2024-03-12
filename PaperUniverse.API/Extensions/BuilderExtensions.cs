using Microsoft.EntityFrameworkCore;
using PaperUniverse.Core;
using PaperUniverse.Infra.Data;

namespace PaperUniverse.API.Extensions;

public static class BuilderExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder) 
    {
        Configuration.Database.ConnectionString = builder.Configuration
            .GetConnectionString("Default") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options => {
            options.UseSqlServer(Configuration.Database.ConnectionString, opt =>
                opt.MigrationsAssembly("PaperUniverse.API"));
        });
    }
}