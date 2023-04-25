using Manager.Domain.Entities;
using Manager.Services.DTO.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Manager.Services.Interfaces;

public interface ITaskService
{
    Task<CreateTaskDto> Create(CreateTaskDto tasksDto);
    Task<TasksDTO> Update(TasksDTO tasksDto);
    Task Remove(Guid UserId, Guid id);
    Task<TasksDTO> Get(Guid id, Guid userId);
    Task<List<TasksDTO>> Search(Guid? id, SearchTask? searchTask);
}