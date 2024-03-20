using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;

namespace PaperUniverse.Tests.Contexts.AccountContext.Create.Mocks;

public class MockService : IService
{
    public Task SendVerificationEmailAsync(User user)
    {
        return Task.CompletedTask;
    }
}