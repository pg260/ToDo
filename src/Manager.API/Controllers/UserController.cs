using AutoMapper;
using Manager.API.ViewModels;
using Manager.Services.DTO.User;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Task = Manager.Domain.Entities.Task;

namespace Manager.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    
    [HttpPost]
    [Route("/api/v1/users/create")]
    public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
    {
        try
        {
            var userDto = _mapper.Map<CreateUserDto>(userViewModel);
            var userCreated = await _userService.Create(userDto);

            return Ok(new ResultViewModel
            {
                Message = "Usuário criado com sucesso",
                Sucess = true,
                Data = userCreated
            });
        }
        catch (DomainExceptions ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro");
        }
    }
}