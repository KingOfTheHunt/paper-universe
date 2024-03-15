using MediatR;

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
                return Results.Created($"api/v1/account/{result.Id}", result);

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
    }
}