using Manager.Core.Exceptions;
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

    public virtual async Task<bool> Remove(Guid id)
    {
        var obj = await _context.Set<User>().SingleOrDefaultAsync(x => x.Id == id);

        if (obj == null) return false;
        
        _context.Remove(obj);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<List<User>> SearchByEmail(string email)
    {
        return await _context.Users
            .Where(x => x.Email.ToLower().Contains(email.ToLower()))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<User>> SearchByName(string name)
    {
        return await _context.Users
            .Where(x => x.Name.ToLower().Contains(name.ToLower()))
            .AsNoTracking()
            .ToListAsync();
    }
}