using Manager.Domain.Entities;
using Manager.Services.DTO.User;

namespace Manager.Services.Interfaces;

public interface IAuthService
{
    Task<User> Get(string email, string password);

    Task<string> GenerateToken(User user);
}