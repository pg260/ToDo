using Manager.Core.Exceptions;
using Manager.Domain.Entities;
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
            await using var context = new ManagerContext();
            var entity = await context.Set<Task>().FindAsync(obj.Id);
            context.Entry(entity).CurrentValues.SetValues(obj);
        
            await context.SaveChangesAsync();

            return obj;
        }
        catch (Exception e)
        {
            throw new DomainExceptions("Erro com o save");
        }
    }

    public virtual async Task<bool> Remove(Guid id)
    {
        await using var context = new ManagerContext();
        var obj = await context.Set<Task>().SingleOrDefaultAsync(x => x.Id == id);

        if (obj == null) return false;
        
        _context.Remove(obj);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Task>> Search(Guid id, SearchTask? seachTaskDto)
    {
        await using var context = new ManagerContext();

        var query = context.Tasks.AsQueryable();

        query = query.Where(x => x.UserId == id);

        if (seachTaskDto != null)
        {
            if (seachTaskDto.Name != null)
                query = query.Where(x => x.Name.Contains(seachTaskDto.Name));

            if (seachTaskDto.Description != null)
                query = query.Where(x => x.Description.Contains(seachTaskDto.Description));

            if (seachTaskDto.Concluded != null && (bool)seachTaskDto.Concluded)
                query = query.Where(x => x.Concluded == seachTaskDto.Concluded);

            if (seachTaskDto.OrderByA != null && seachTaskDto.OrderByZ != null)
            {
                if ((bool)seachTaskDto.OrderByA && (bool)seachTaskDto.OrderByZ)
                    throw new DomainExceptions("Não podemos ordernar de A-Z e de Z-A ao mesmo tempo.");
            }

            if (seachTaskDto.OrderByA != null && (bool)seachTaskDto.OrderByA)
            {
                query = query.OrderBy((p => p.Name));
            }

            if (seachTaskDto.OrderByZ != null && (bool)seachTaskDto.OrderByZ)
            {
                query = query.OrderByDescending((p => p.Name));
            }
            
            if (seachTaskDto.MaiorQue!= null && seachTaskDto.MenorQue != null)
            {
                if ((bool)seachTaskDto.MaiorQue && (bool)seachTaskDto.MenorQue)
                    throw new DomainExceptions("Não podemos ordernar por esses parametros de data mesmo tempo.");
            }
            
            if ((seachTaskDto.MaiorQue != null || seachTaskDto.MenorQue != null) && seachTaskDto.CreatedTime == null)
                throw new DomainExceptions("Insira a data para podermos ordenar os dados.");
            
            if (seachTaskDto.MaiorQue != null && (bool)seachTaskDto.MaiorQue)
            {
                query = query.Where(x => x.CreatedAt >= seachTaskDto.CreatedTime);
                query = query.OrderBy((p => p.CreatedAt));
            }

            if (seachTaskDto.MenorQue != null && (bool)seachTaskDto.MenorQue)
            {
                query = query.Where(x => x.CreatedAt <= seachTaskDto.CreatedTime);
                query = query.OrderBy((p => p.CreatedAt));
            }
            

            query = query.Skip((int)((seachTaskDto.PAtual - 1) * seachTaskDto.PTake))
                .Take((int)seachTaskDto.PTake);
        }

        return await query
            .AsNoTracking()
            .ToListAsync();
    }
}