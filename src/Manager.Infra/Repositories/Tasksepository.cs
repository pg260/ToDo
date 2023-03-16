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
}