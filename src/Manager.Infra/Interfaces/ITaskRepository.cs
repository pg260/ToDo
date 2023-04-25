using Manager.Domain.Entities;
using Task = Manager.Domain.Entities.Task;

namespace Manager.Infra.Interfaces;

public interface ITaskRepository : IBaseRepository<Task>
{

    Task<Task> Get(Guid id, Guid userId);
    Task<bool> Remove(Guid id);
    Task<List<Task>> Search(Guid? id, SearchTask? searchTask);

}