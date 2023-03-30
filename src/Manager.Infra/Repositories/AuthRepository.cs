using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories;

public class AuthRepository : IAuthRepository
{
    public AuthRepository()
    {
        
    }
    
    public AuthRepository(ManagerContext context)
    {
        _context = context;
    }

    private readonly ManagerContext _context;
    
    public async Task<User> Get(string email, string password)
    {
        await using var context = new ManagerContext();
        return await context.Set<User>()
            .Where(x => x.Email == email && x.Password == password)
            .AsNoTracking()
            .SingleOrDefaultAsync() ?? throw new DomainExceptions("Esse login/senha não existe.");

    }
}