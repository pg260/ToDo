using Manager.Core.Exceptions;
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

    public async Task<Task> Get(string name, Guid id)
    {
        return await _context.Set<Task>()
            .Where(x => x.UserId == id && x.Name == name)
            .FirstOrDefaultAsync() ?? throw new DomainExceptions("Não existe nenhuma task com esse nome.");
    }
    
    public virtual async Task<bool> Remove(string name)
    {
        var obj = await _context.Set<Task>().SingleOrDefaultAsync(x => x.Name == name);

        if (obj == null) return false;
        
        _context.Remove(obj);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Task>> SearchByConcluded(bool concluded, Guid id)
    {
        return await _context.Tasks
            .Where(x => x.UserId == id && x.Concluded == concluded)
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