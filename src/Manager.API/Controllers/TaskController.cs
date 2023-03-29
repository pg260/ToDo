using AutoMapper;
using DefaultNamespace;
using Manager.API.ViewModels.TasksViewModel;
using Manager.API.ViewModels.UserViewModels;
using Manager.Core.Exceptions;
using Manager.Services.DTO.Tasks;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

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
    [Route(template: "/api/v1/Tasks/CreateTask/{UserId}/{Name}")]
    public async Task<IActionResult> Create(Guid UserId, string Name, [FromQuery(Name = "Description"), SwaggerParameter(Required = false)] string? Description = null, 
        [FromQuery(Name = "Deadline"), SwaggerParameter(Required = false)] DateTime? Deadline = null)
    {
        try
        {
            var taskDto = new CreateTaskDto(userId: UserId, name: Name, description: Description, deadline: Deadline);
            var taskCreated = await _taskService.Create(tasksDto: taskDto);
            return Ok(value: new ResultViewModel 
            { 
                Message = "Task criada com sucesso", 
                Sucess = true, 
                Data = taskCreated 
            });
        }
        catch (Exception e)
        {
            return StatusCode(statusCode: 500, value: e);
        }
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
    
    [HttpDelete]
    [Route("/api/v1/Task/RemoveTask/{id}/{userId}")]
    public async Task<IActionResult> Remove(Guid id, Guid userId)
    {
        try
        {
            await _taskService.Remove(id, userId);

            return Ok(new ResultViewModel
            {
                Message = "Usuário deletado com sucesso",
                Sucess = true,
                Data = null
            });
        }
        catch (DomainExceptions)
        {
            throw new DomainExceptions("Falha na remoção do usuário, por favor contate o suporte.");
        }
    }
}