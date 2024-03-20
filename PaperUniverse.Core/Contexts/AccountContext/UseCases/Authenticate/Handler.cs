using MediatR;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly Contracts.IRepository _repository;

    public Handler(Contracts.IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Valida a requisição
        var res = Specification.Assert(request);

        if (res.IsValid == false)
            return new Response(400, "Os dados informados são inválidos", res.Notifications);
        #endregion

        #region Obtém o usuário no banco
        var user = await _repository.GetUserByEmailAsync(request.Email);

        if (user == null)
            return new Response(404, "O usuário não foi encontrado em nossa base de dados.");
        #endregion

        #region Verifica se a conta foi ativada
        if (user.Email.Verification.IsActive == false)
            return new Response(400, "A conta não foi ativada.");
        #endregion

        #region Valida a senha
        if (user.Password.Challenge(request.Password) == false)
            return new Response(400, "A senha informada está incorreta.");
        #endregion

        var data = new ResponseData 
        {
            Name = user.Name,
            Email = user.Email.Address,
            Image = user.Image
        };

        return new Response(200, "Autenticado com sucesso", data: data);
    }
}