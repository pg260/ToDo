using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces;

public interface IAuthRepository
{
    Task<User> Get(string email);
}