using Manager.Services.DTO.Tasks;

namespace Manager.Services.Interfaces;

public interface ITaskService
{
    Task<CreateTaskDto> Create(CreateTaskDto tasksDto);
    
    Task<TasksDTO> Update(TasksDTO tasksDto);
    
    Task Remove(RemoveTaskDto removeTaskDto);
    Task<TasksDTO> Get(TasksDTO tasksDto);
    Task<List<TasksDTO>> Get();
    Task<List<TasksDTO>> SearchByName(string name);
    Task<List<TasksDTO>> SearchByConcluded(bool concluded);
}