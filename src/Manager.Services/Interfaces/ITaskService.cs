using Manager.Services.DTO.Tasks;

namespace Manager.Services.Interfaces;

public interface ITaskService
{
    Task<CreateTaskDto> Create(CreateTaskDto tasksDto);

    Task<TasksDTO> Update(TasksDTO tasksDto);
    
    Task Remove(RemoveTaskDto removeTaskDto);
    Task<TasksDTO> Get(Guid id, Guid userId);
    Task<List<TasksDTO>> SearchByConcluded(bool concluded, Guid user);
    Task<List<TasksDTO>> SearchByUser(Guid id);
}