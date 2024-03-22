using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.ResendVerification;

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
            return new Response(400, "Os dados informados são inválidos.", res.Notifications);
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
            return new Response(500, "Erro ao obter o usuário.");
        }
        #endregion

        #region Gera o novo código de verificação e envia

        if (user.Email.Verification.IsActive)
            return new Response(400, "A conta já está ativada.");
            
        user.Email.ResendVerificationCode();

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response(500, "Erro ao salvar as alterações.");
        }

        await _service.SendVerificationEmailAsync(user, cancellationToken);

        return new Response(200, "Código de verificação enviado com sucesso.");
        #endregion
    }
}