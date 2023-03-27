using Manager.Services.DTO.Tasks;

namespace Manager.Services.Interfaces;

public interface ITaskService
{
    Task<CreateTaskDto> Create(CreateTaskDto tasksDto);
    
    Task<TasksDTO> Update(TasksDTO tasksDto);
    
    Task Remove(RemoveTaskDto removeTaskDto);
    Task<TasksDTO> Get(string name, Guid id);
    Task<List<TasksDTO>> SearchByConcluded(bool concluded, Guid id);
    Task<List<TasksDTO>> SearchByUser(Guid id);
}