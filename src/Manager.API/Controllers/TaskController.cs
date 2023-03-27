using AutoMapper;
using Manager.API.ViewModels.TasksViewModel;
using Manager.API.ViewModels.UserViewModels;
using Manager.Services.DTO.Tasks;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers;

[ApiController]
public class TaskController : ControllerBase
{
    public TaskController(ITaskService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    private readonly ITaskService _userService;
    private readonly IMapper _mapper;
    
    [HttpPost]
    [Route("/api/v1/Tasks/CreateTask")]
    public async Task<IActionResult> Create([FromBody] CreateTaskViewModel taskViewModel)
    {
        var taskDto = _mapper.Map<CreateTaskDto>(taskViewModel);
            var taskCreated = await _userService.Create(taskDto);
            return Ok(new ResultViewModel 
            { 
                Message = "Task criada com sucesso", 
                Sucess = true, 
                Data = taskCreated 
            });
    }
}