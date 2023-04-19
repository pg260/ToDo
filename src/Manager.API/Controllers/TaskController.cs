using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DefaultNamespace;
using Manager.API.ViewModels.TasksViewModel;
using Manager.API.ViewModels.UserViewModels;
using Manager.Domain.Entities;
using Manager.Services.DTO.Tasks;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [Route(template: "/api/v1/Tasks/CreateTask")]
    [Authorize]
    public async Task<IActionResult> Create([FromForm]CreateTaskViewModel createTaskViewModel)
    {
        var taskDto = _mapper.Map<CreateTaskDto>(createTaskViewModel);
            taskDto.UserId = Guid.Parse(User.Identity.Name);
            var taskCreated = await _taskService.Create(tasksDto: taskDto);
            return Ok(value: new ResultViewModel 
            { 
                Message = "Task criada com sucesso", 
                Sucess = true, 
                Data = taskCreated 
            });
    }

    [HttpPut]
    [Route("/api/v1/Task/UpdateTask")]
    [Authorize]
    public async Task<IActionResult> Update([FromForm] UpdateTaskViewModel updateTaskViewModel)
    {
        var taskDto = _mapper.Map<TasksDTO>(updateTaskViewModel);
            taskDto.UserId = Guid.Parse(User.Identity.Name);
            var taskUpdated = await _taskService.Update(taskDto);
            return Ok(new ResultViewModel
            {
                Message = "Task modificado com sucesso.",
                Sucess = true,
                Data = taskUpdated
            });
    }
    
    [HttpDelete]
    [Route("/api/v1/Task/RemoveTask")]
    [Authorize]
    public async Task<IActionResult> Remove([Required] Guid id)
    {
        await _taskService.Remove(Guid.Parse(User.Identity.Name), id);

            return Ok(new ResultViewModel
            {
                Message = "Task deletado com sucesso",
                Sucess = true,
                Data = null
            }); 
    }

    [HttpGet]
    [Route(("/api/v1/Task/GetTask"))]
    [Authorize]
    public async Task<IActionResult> Get([Required] Guid id)
    {
        var task = await _taskService.Get( id, Guid.Parse(User.Identity.Name));

            if (task == null)
            {
                return Ok(new ResultViewModel
                {
                    Message = "Não existe nenhuma task com essas informações.",
                    Sucess = true,
                    Data = null
                });
            }
            
            return Ok(new ResultViewModel
            {
                Message = "Pesquisa realizada com sucesso.",
                Sucess = true,
                Data = task
            });
    }

    [HttpGet("/api/v1/Tasks/Search")]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] SearchViewModel dto)
    {
        var searchDto = _mapper.Map<SearchTask>(dto);
        
        List<TasksDTO> allTasks = await _taskService.Search( Guid.Parse(User.Identity.Name), searchDto);

        return Ok(new ResultViewModel
        {
            Message = "Pesquisa realizada com sucesso.",
            Sucess = true,
            Data = allTasks
        });
    }
}