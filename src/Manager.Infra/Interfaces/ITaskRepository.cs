using Task = Manager.Domain.Entities.Task;

namespace Manager.Infra.Interfaces;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<Task> Get(string name);
    Task<bool> Remove(string name);
    Task<List<Task>> SearchByName(string name);
    Task<List<Task>> SearchByConcluded(bool concluded);

    Task<List<Task>> SearchByUser(Guid id);

}