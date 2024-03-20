using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        Configuration.Smtp.Host = builder.Configuration
            .GetSection("Smtp").GetValue<string>("Host") ?? string.Empty;
        Configuration.Smtp.Port = builder.Configuration
            .GetSection("Smtp").GetValue<int>("Port");
        Configuration.JwtPrivateKey = builder.Configuration.
            GetValue<string>("JwtPrivateKey") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options => {
            options.UseSqlServer(Configuration.Database.ConnectionString, opt =>
                opt.MigrationsAssembly("PaperUniverse.API"));
        });
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => 
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.JwtPrivateKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddAuthorization();
    }

    public static void AddMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Configuration).Assembly));
    }
}