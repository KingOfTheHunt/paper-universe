using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.SendResetPasswordCode.Contracts;

public interface IService
{
    Task SendResetPasswordCodeEmailAsync(User user, CancellationToken cancellationToken);
}