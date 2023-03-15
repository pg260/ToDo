using Task = Manager.Domain.Entities.Task;

namespace Manager.Infra.Interfaces;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<List<Task>> SearchByName(string name);
    Task<List<Task>> SearchByConcluded(bool concluded);

}