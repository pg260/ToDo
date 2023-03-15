using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ManagerContext context) : base(context)
    {
        _context = context;
    }

    private readonly ManagerContext _context;
    
    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public Task<List<User>> SearchByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> SearchByName(string name)
    {
        throw new NotImplementedException();
    }
}