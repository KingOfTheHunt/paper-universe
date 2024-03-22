using MediatR;
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

        #region Resend Verification
        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts.IRepository,
            Infra.Contexts.AccountContext.UseCases.ResendVerification.Repository
        >();
        builder.Services.AddTransient<
            Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts.IService,
            Infra.Contexts.AccountContext.UseCases.ResendVerification.Service
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
    }
}