using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;

namespace PaperUniverse.Tests.Contexts.AccountContext.Create.Mocks;

public class MockRepository : IRepository
{
    public Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
    {
        if (email == "email@email.com")
            return Task.FromResult(true);

        return Task.FromResult(false);
    }

    public Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}