using AutoMapper;
using DefaultNamespace;
using Manager.API.ViewModels.TasksViewModel;
using Manager.API.ViewModels.UserViewModels;
using Manager.Services.DTO.Tasks;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers;

[ApiController]
public class TaskController : ControllerBase
{
    public TaskController(ITaskService taskService, IMapper mapper)
    {
        _taskService = taskService;
        _mapper = mapper;
    }

    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;
    
    [HttpPost]
    [Route("/api/v1/Tasks/CreateTask")]
    public async Task<IActionResult> Create([FromBody] CreateTaskViewModel taskViewModel)
    {
        var taskDto = _mapper.Map<CreateTaskDto>(taskViewModel);
            var taskCreated = await _taskService.Create(taskDto);
            return Ok(new ResultViewModel 
            { 
                Message = "Task criada com sucesso", 
                Sucess = true, 
                Data = taskCreated 
            });
    }
    
    [HttpPut]
    [Route("/api/v1/Task/UpdateTask")]
    public async Task<IActionResult> Update([FromBody] UpdateTaskViewModel updateTaskViewModel)
    {
        try
        {
            var taskDto = _mapper.Map<TasksDTO>(updateTaskViewModel);
            var taskUpdated = await _taskService.Update(taskDto);
            return Ok(new ResultViewModel
            {
                Message = "Usuáio modificado com sucesso.",
                Sucess = true,
                Data = taskUpdated
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}