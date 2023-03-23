using AutoMapper;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
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
    [Route("/api/v1/users/CreateUser")]
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
        catch (Exception e)
        {
            return StatusCode(500, "erro");
        }
    }

    [HttpPut]
    [Route("/api/v1/Users/UpdateUser")]
    public async Task<IActionResult> Update([FromBody] UpdateViewModel userViewModel)
    {
        try
        {
            var userDto = _mapper.Map<UpdateUserDto>(userViewModel);
            var userUpdated = await _userService.Update(userDto);
            return Ok(new ResultViewModel
            {
                Message = "Usuáio modificado com sucesso.",
                Sucess = true,
                Data = userUpdated
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpDelete]
    [Route("/api/v1/users/RemoveUser/{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        try
        {
            await _userService.Remove(id);

            return Ok(new ResultViewModel
            {
                Message = "Usuário deletado com sucesso",
                Sucess = true,
                Data = null
            });
        }
        catch (Exception)
        {
            return StatusCode(500, "erro");
        }
    }

    [HttpGet]
    [Route("/api/v1/Users/GetUser/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var userDto = await _userService.Get(id);

            if (userDto == null)
            {
                return Ok(new ResultViewModel
                {
                    Message = "Nenhum usuã́rio com o id informado foi encontrado.",
                    Sucess = true,
                    Data = userDto
                });
            }
            return Ok(new ResultViewModel
            {
                Message = "Pesquisa realizada com sucesso.",
                Sucess = true,
                Data = userDto
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, "erro");
        }
    }

    [HttpGet]
    [Route("/api/v1/Users/GetAllUsers")]
    public async Task<IActionResult> Get()
    {
        try
        {
            List<UserDTO> allUsers = await _userService.Get();

            return Ok(new ResultViewModel
            {
                Message = "Pesquisa realizada com sucesso",
                Sucess = true,
                Data = allUsers
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, "erro");
        }
    }

    [HttpGet]
    [Route("/api/v1/Users/SearchByName")]
    public async Task<IActionResult> SearchByName(string name)
    {
        try
        {
            List<UserDTO> searchUsers = await _userService.SearchByName(name);

            return Ok(new ResultViewModel
            {
                Message = "Pesquisa realizada com sucesso",
                Sucess = true,
                Data = searchUsers
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, "erro");
        }
    }
    
    [HttpGet]
    [Route("/api/v1/Users/SearchByEmail")]
    public async Task<IActionResult> SearchByEmail(string email)
    {
        try
        {
            List<UserDTO> searchUsers = await _userService.SearchByEmail(email);

            return Ok(new ResultViewModel
            {
                Message = "Pesquisa realizada com sucesso",
                Sucess = true,
                Data = searchUsers
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, "erro");
        }
    }
    
    [HttpGet]
    [Route("/api/v1/Users/GetByEmail")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        try
        {
            var searchUsers = await _userService.GetByEmail(email);

            return Ok(new ResultViewModel
            {
                Message = "Pesquisa realizada com sucesso",
                Sucess = true,
                Data = searchUsers
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, "erro");
        }
    }
}