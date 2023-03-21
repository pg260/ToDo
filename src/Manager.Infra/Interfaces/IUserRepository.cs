using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    
    Task<User> Get(Guid id);
    Task<User?> GetByEmail(string email);
    Task<List<User>> SearchByEmail(string email);
    Task<List<User>> SearchByName(string name);
}