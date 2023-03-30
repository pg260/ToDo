using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO.User;
using Manager.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Manager.Services.AuthSettings;

namespace Manager.Services.Services;

public class AuthServices : IAuthService
{
    public AuthServices(IMapper mapper, IAuthRepository authRepository)
    {
        _mapper = mapper;
        _authRepository = authRepository;
    }

    private readonly IMapper _mapper;
    private readonly IAuthRepository _authRepository;
    
    
    public async Task<User> Get(string email, string password)
    {
        var user = await _authRepository.Get(email, password);
        
        return _mapper.Map<User>(user);
    }
    
    public async  Task<string> GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}