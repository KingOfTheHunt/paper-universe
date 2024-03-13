using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Valida a requisição
        var res = Specification.Assert(request);

        if (res.IsValid == false)
            return new Response(400, "Não foi possível realizar o seu cadastro.", res.Notifications);
        #endregion

        #region Gera a entidade e os value objects
        var email = new Email(request.Email);
        var password = new Password(request.Password);
        var user = new User(request.Name, email, password);

        if (user.IsValid == false)
            return new Response(400, "Os dados informados são inválidos.", user.Notifications);
        #endregion

        #region Verifica se o e-mail já está cadastrado
        var emailExists = await _repository.AnyAsync(user.Email.Address, cancellationToken);

        if (emailExists)
            return new Response(400, "O e-mail já está cadastrado no banco de dados.");
        #endregion

        #region Persiste o usuário no banco
        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response(500, "Não foi possível realizar o cadastro.");
        }
        #endregion

        #region Envia o e-mail com o código de ativação
        await _service.SendVerificationEmailAsync(user);
        #endregion

        return new Response(user.Id, 201, "Cadastro realizado com sucesso.");
    }
}