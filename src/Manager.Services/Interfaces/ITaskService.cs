using Manager.Services.DTO;
using Manager.Services.DTO.Tasks;

namespace Manager.Services.Interfaces;

public interface ITaskService
{
    Task<CreateTaskDto> Create(TasksDTO tasksDto);
    Task<TasksDTO> Update(TasksDTO tasksDto);
    Task Remove(Guid id);
    Task<TasksDTO> Get(Guid id);
    Task<List<TasksDTO>> Get();
    Task<List<TasksDTO>> SearchByName(string name);
    Task<List<TasksDTO>> SearchByConcluded(bool concluded);
}