namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IService
{
    Task SendVerificationEmailAsync();
}