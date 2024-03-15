using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Verify.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task Save(User user);
}