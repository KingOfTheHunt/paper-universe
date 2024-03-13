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
        Configuration.Smtp.Username = builder.Configuration
            .GetSection("Smtp").GetValue<string>("Username") ?? string.Empty;
        Configuration.Smtp.Password = builder.Configuration
            .GetSection("Smtp").GetValue<string>("Password") ?? string.Empty;
        Configuration.Smtp.Server = builder.Configuration
            .GetSection("Smtp").GetValue<string>("Server") ?? string.Empty;
        Configuration.Smtp.Port = builder.Configuration
            .GetSection("Smtp").GetValue<int>("Port");
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options => {
            options.UseSqlServer(Configuration.Database.ConnectionString, opt =>
                opt.MigrationsAssembly("PaperUniverse.API"));
        });
    }

    public static void AddMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Configuration).Assembly));
    }
}