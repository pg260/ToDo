using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
    private readonly ManagerContext _context;

    public BaseRepository(ManagerContext context)
    {
        _context = context;
    }

    public virtual async Task<T> Create(T obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();

        return obj;
    }

    public virtual async Task<T> Update(T obj)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(obj.Id);
            _context.Entry(entity).CurrentValues.SetValues(obj);
            
            await _context.SaveChangesAsync();

            return obj;
        }
        catch (Exception e)
        {
            throw new DomainExceptions("Erro com o save");
        }
    }

    public virtual async Task<T> Get(Guid id)
    {
        return await _context.Set<T>().FindAsync(id) ?? throw new DomainExceptions("Não foi encontrado nenhum resultado para essa pesquisa.");
    }

    public virtual async Task<List<T>> Get()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
}