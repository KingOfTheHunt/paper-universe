using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PaperUniverse.API.Extensions;

namespace PaperUniverse.API.Extensions;

public static class AccountContextExtensions
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create
        builder.Services.AddTransient<
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            PaperUniverse.Infra.Contexts.AccountContext.UseCases.Create.Repository
        >();

        builder.Services.AddTransient<
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
            PaperUniverse.Infra.Contexts.AccountContext.UseCases.Create.Service
        >();
        #endregion

        #region Verify
        builder.Services.AddTransient<
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Verify.Contracts.IRepository,
            PaperUniverse.Infra.Contexts.AccountContext.UseCases.Verify.Repository
        >();
        #endregion

        #region Authenticate
        builder.Services.AddTransient<
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
            PaperUniverse.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository
        >();
        #endregion

        #region Resend verification
        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.ResendVerification.Repository
        >();
        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts.IService,
            Infra.Contexts.AccountContext.UseCases.ResendVerification.Service
        >();
        #endregion

        #region Details
        builder.Services.AddScoped<
            Core.Contexts.AccountContext.UseCases.Details.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.Details.Repository
        >();
        #endregion

        #region Update password
        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.UpdatePassword.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.UpdatePassword.Repository
        >();
        #endregion
    }

    public static void MapAccountContextEndpoints(this WebApplication app)
    {
        #region Create
        app.MapPost("api/v1/account/create", async (
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Request request,
            IRequestHandler<PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Request,
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Response> handler
        ) => 
        {
            var result = await handler.Handle(request, new CancellationToken());

            if (result.Success)
                return Results.Created($"api/v1/account/{result.Data?.Id}", result);

            return Results.Json(result, statusCode: result.Status);
        });
        #endregion

        #region Verify
        app.MapPost("api/v1/account/verify", async (
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Verify.Request request,
            IRequestHandler<PaperUniverse.Core.Contexts.AccountContext.UseCases.Verify.Request,
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Verify.Response> handler
        ) => 
        {
            var result = await handler.Handle(request, new CancellationToken());

            if (result.Success)
                return Results.Ok(result);

            return Results.Json(result, statusCode: result.Status);
        });
        #endregion

        #region Authenticate
        app.MapPost("api/v1/account/authenticate", async (
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate.Request request,
            IRequestHandler<PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate.Request,
            PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler
        ) => 
        {
            var result = await handler.Handle(request, new CancellationToken());

            if (result.Success == false)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data == null)
                return Results.Json(result, statusCode: 500);

            result.Data.Token = JwtExtensions.Generate(result.Data);

            return Results.Ok(result);
        });
        #endregion

        #region Resend Verification
        app.MapPost("api/v1/account/resend-verification", async (
            Core.Contexts.AccountContext.UseCases.ResendVerification.Request request,
            IRequestHandler<Core.Contexts.AccountContext.UseCases.ResendVerification.Request,
            Core.Contexts.AccountContext.UseCases.ResendVerification.Response> handler
        ) => 
        {
            var result = await handler.Handle(request, new CancellationToken());

            if (result.Success)
                return Results.Ok(result);
            
            return Results.Json(result, statusCode: result.Status);
        });
        #endregion

        #region Details
        app.MapGet("api/v1/account/details", async (
            HttpContext httpContext,
            IRequestHandler<Core.Contexts.AccountContext.UseCases.Details.Request, 
            Core.Contexts.AccountContext.UseCases.Details.Response> handler
        ) => 
        {
            var email = httpContext.User.Identity?.Name;
            var request = new Core.Contexts.AccountContext.UseCases.Details.Request 
            {
                Email = email ?? string.Empty
            };

            var result = await handler.Handle(request, new CancellationToken());

            if (result.Success)
                return Results.Ok(result);

            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();
        #endregion

        #region Update password
        app.MapPut("api/v1/account/update-password", async (
            HttpContext httpContext,
            Core.Contexts.AccountContext.UseCases.UpdatePassword.Request request,
            IRequestHandler<Core.Contexts.AccountContext.UseCases.UpdatePassword.Request,
            Core.Contexts.AccountContext.UseCases.UpdatePassword.Response> handler
        ) => 
        {
            request.Email = httpContext.User.Identity?.Name ?? string.Empty;

            var result = await handler.Handle(request, new CancellationToken());

            if (result.Success)
                return Results.Ok(result);

            return Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();
        #endregion
    }
}