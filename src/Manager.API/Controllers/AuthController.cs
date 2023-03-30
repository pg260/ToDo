using System.ComponentModel.DataAnnotations;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Services.Interfaces;
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
    public async Task<ActionResult<dynamic>> Authenticate([FromForm] [Required] string email, [Required] string password)
    {
        var user = await _authRepository.Get(email, password);

        if (user == null)
            return NotFound(new { message = "Usuário ou senha inválido." });

        var token = _authServices.GenerateToken(user);

        user.Password = "";

        return new
        {
            user = user,
            token = token
        };
    }
}