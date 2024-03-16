using MediatR;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Core.Contexts.AccountContext.UseCases.Verify.Contracts;
using PaperUniverse.Core.Contexts.AccountContext.ValueObjects;

namespace PaperUniverse.Core.Contexts.AccountContext.UseCases.Verify;

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
            return new Response(400, "Os dados informados são inválidos.", res.Notifications);
        #endregion

        #region Gera o value object e verifica se ele existe no banco
        var email = new Email(request.Email);
        User? user;
        
        try
        {
            user = await _repository.GetUserByEmailAsync(email.Address);
            
            if (user == null)
                return new Response(404, "O e-mail informado não foi encontrado no banco de dados.");
        }
        catch (Exception)
        {
            return new Response(500, "Erro ao buscar o usuário no banco.");
        }
       
        
        #endregion

        #region Valida o código de verificação
        user.Email.Verification.Verify(request.VerificationCode);

        if (user.Email.Verification.IsValid == false)
            return new Response(400, "O código de verificação não é válido.", user.Email.Verification.Notifications);
        #endregion

        #region Salva as alterações
        try
        {
            await _repository.Save(user);
        }
        catch (Exception)
        {
            return new Response(500, "Erro ao salvar as alterações.");
        }
        #endregion

        return new Response(200, "Conta verificada com sucesso!");
    }
}