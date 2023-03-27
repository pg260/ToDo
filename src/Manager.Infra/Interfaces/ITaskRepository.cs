using Task = Manager.Domain.Entities.Task;

namespace Manager.Infra.Interfaces;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<Task> Get(string name, Guid id);
    Task<bool> Remove(string name);
    Task<List<Task>> SearchByConcluded(bool concluded, Guid id);
    Task<List<Task>> SearchByUser(Guid id);

}