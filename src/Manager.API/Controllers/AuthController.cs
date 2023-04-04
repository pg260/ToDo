using System.ComponentModel.DataAnnotations;
using System.Security;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers;
[ApiController]
[Route("/api/v1")]
public class AuthController : ControllerBase
{
    public AuthController(IAuthService authServices, IAuthRepository authRepository)
    {
        _authServices = authServices;
        _authRepository = authRepository;
    }

    private readonly IAuthService _authServices;
    private readonly IAuthRepository _authRepository;
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<dynamic>> Authenticate([Required] string email, [Required] string password)
    {
        var user = await _authRepository.Get(email);

        HashServices hashServices = new();
        if(!hashServices.VerifyPassword(password, user.Password))
            return NotFound(new { message = "Usuário ou senha inválido." });

        var token = _authServices.GenerateToken(user);
        var refreshToken = _authServices.GenerateRefreshToken();
        _authServices.SaveRefreshToken(user.Id.ToString(), refreshToken);

        user.Password = "";

        return new
        {
            user = user,
            token = token,
            refreshToken = refreshToken
        };
    }

    [HttpPost]
    [Route("refresh")]
    public IActionResult Refresh(string token, string refreshToken)
    {
        var principal = _authServices.GetPrincipalFromExpiredToken(token);
        var id = principal.Identity.Name;
        var savedRefreshToken = _authServices.GetRefreshToken(id);
        if (savedRefreshToken != refreshToken)
            throw new SecurityException("Token inválido");

        var newJwtToken = _authServices.GenerateToken(principal.Claims);
        var newRefreshToken = _authServices.GenerateRefreshToken();
        _authServices.DeleteRefreshToken(id, refreshToken);
        _authServices.SaveRefreshToken(id, newRefreshToken);

        return new ObjectResult(new
        {
            token = newJwtToken,
            refreshToken = newRefreshToken
        });
    }
}