using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.ResetPassword.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.ResetPassword;

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
            return new Response(400, "Dados inválidos", res.Notifications);
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
            return new Response(500, "Erro ao buscar o usuário no banco.");
        }
        #endregion

        #region Reseta a senha 
        user.ResetPassword(request.NewPassword, request.ResetCode);

        if (user.IsValid == false)
            return new Response(400, "O código informado está incorreto.");
        #endregion

        #region Salva as alterações no banco
        try
        {
            await _repository.Save(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response(500, "Erro ao salvar os dados do usuário no banco.");
        }
        #endregion

        return new Response(200, "Senha resetada com sucesso."); 
    }
}