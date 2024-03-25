using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Details.Contracts;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Details;

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
            return new Response(400, "Os dados informados são inválidos", notifications: res.Notifications);
        #endregion

        #region Obtém o usuário
        User? user;

        try
        {
            user = await _repository.GetUserByIdAsync(request.Email, cancellationToken);

            if (user == null)
                return new Response(404, "O usuário não foi encontrado.");
        }
        catch (Exception)
        {
            return new Response(500, "Erro ao obter os dados do usuário no banco.");
        }
        #endregion

        var data = new ResponseData(user.Name, user.Email.Address, user.Image);

        return new Response(200, "Dados obtidos com sucesso.", data);
    }
}