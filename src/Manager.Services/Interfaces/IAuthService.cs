using System.Security.Claims;
using Manager.Domain.Entities;
using Manager.Services.DTO.User;

namespace Manager.Services.Interfaces;

public interface IAuthService
{
    Task<User> Get(string email, string password);

    public string GenerateToken(User user);

    public string GenerateToken(IEnumerable<Claim> claims);

    public string GenerateRefreshToken();

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    public void SaveRefreshToken(string id, string refreshToken);

    public string GetRefreshToken(string id);
    public void DeleteRefreshToken(string id, string refreshToken);
}