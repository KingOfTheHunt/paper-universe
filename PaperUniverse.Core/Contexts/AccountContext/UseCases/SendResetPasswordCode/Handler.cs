using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.SendResetPasswordCode.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.SendResetPasswordCode;

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
            return new Response(400, "Os dados informados são inválidos.", notifications: res.Notifications);
        #endregion

        #region Obtém o usuário
        User? user;

        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user == null)
                return new Response(404, "Não foi encontrado nenhum usuário com este e-mail.");
        }
        catch (Exception)
        {
            return new Response(500, "Não foi possível obter o usuário no banco.");
        }
        #endregion

        #region Valida se a conta está ativada
        if (user.Email.Verification.IsActive == false)
            return new Response(400, "A sua conta não está ativada.");
        #endregion

        #region Envia o e-mail com o código para resetar a senha
        await _service.SendResetPasswordCodeEmailAsync(user, cancellationToken);
        #endregion

        return new Response(200, "Código de reset enviado com sucesso.");
    }
}