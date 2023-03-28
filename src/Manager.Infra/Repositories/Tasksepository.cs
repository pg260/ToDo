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


    public async Task<Task> Get(Guid id, Guid userId)
    {
        await using var context = new ManagerContext();
        return await context.Set<Task>()
            .Where(x => x.UserId == userId && x.Id == id)
            .AsNoTracking()
            .SingleOrDefaultAsync() ?? throw new DomainExceptions("Essa task não existe.");
    }

    public override async Task<Task> Update(Task obj)
    {
        try
        {
            var entity = await _context.Set<Task>().FindAsync(obj.Id);
            _context.Entry(entity).CurrentValues.SetValues(obj);
        
            await _context.SaveChangesAsync();

            return obj;
        }
        catch (Exception e)
        {
            throw new DomainExceptions("Erro com o save");
        }
    }

    public virtual async Task<bool> Remove(Guid id)
    {
        var obj = await _context.Set<Task>().SingleOrDefaultAsync(x => x.Id == id);

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

    public async Task<List<Task>> SearchByUser(Guid userId)
    {
        return await _context.Tasks
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }
}