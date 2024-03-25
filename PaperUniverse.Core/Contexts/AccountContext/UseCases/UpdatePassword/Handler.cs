using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.UpdatePassword;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Valida a requisição
        var res = Specification.Assert(request);

        if (res.IsValid == false)
            return new Response(400, "Os dados da requisição não são válidos.", res.Notifications); 
        #endregion

        #region Obtém o usuário
        User? user;

        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user == null)
                return new Response(404, "O usuário não foi encontrado.");
        }
        catch (Exception)
        {
            return new Response(500, "Houve um erro ao buscar o usuário no banco.");
        }
        #endregion

        #region Valida se a nova senha não é igual a antiga
        if (user.Password.Challenge(request.NewPassword))
            return new Response(400, "A nova senha não pode ser igual a antiga.");
        #endregion

        #region Atualiza e salva nova senha
        user.UpdatePassword(request.NewPassword);

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response(500, "Houve um erro ao atualizar a senha no banco.");
        }
        #endregion

        return new Response(200, "Senha atualizada com sucesso.");
    }
}