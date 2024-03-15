using Microsoft.EntityFrameworkCore;
using PaperUniverse.Core.Contexts.AccountContext.Entities;
using PaperUniverse.Infra.Data;

namespace PaperUniverse.Infra.Contexts.AccountContext.UseCases.Verify;

public class Repository : Core.Contexts.AccountContext.UseCases.Verify.Contracts.IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email.Address == email);
    }

    public async Task Save(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}