using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = Manager.Domain.Entities.Task;

namespace Manager.Infra.Repositories;

public class Tasksepository : BaseRepository<Task>, ITaskRepository
{
    public Tasksepository(ManagerContext context) : base(context)
    {
        _context = context;
    }

    private readonly ManagerContext _context;

    public async Task<Task> Get(string name)
    {
        return await _context.Set<Task>().FindAsync(name);
    }
    
    public virtual async Task<bool> Remove(string name)
    {
        var obj = await _context.Set<Task>().SingleOrDefaultAsync(x => x.Name == name);

        if (obj == null) return false;
        
        _context.Remove(obj);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Task>> SearchByName(string name)
    {
        return await _context.Tasks
            .Where(x => x.Name.ToLower().Contains(name.ToLower()))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Task>> SearchByConcluded(bool concluded)
    {
        return await _context.Tasks
            .Where(x => x.Concluded == concluded)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Task>> SearchByUser(Guid id)
    {
        return await _context.Tasks
            .Where(x => x.UserId == id)
            .AsNoTracking()
            .ToListAsync();
    }
}