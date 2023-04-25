using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO.Tasks;
using Manager.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Manager.Services.Services;

public class TasksServices : ITaskService
{
    public TasksServices(IMapper mapper, ITaskRepository taskRepository)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public async Task<CreateTaskDto> Create(CreateTaskDto tasksDto)
    {
        try
        {
            var allUsers = await Search(tasksDto.UserId, null);
            
            foreach (var tasks in allUsers)
            {
                if (tasks.Name == tasksDto.Name)
                {
                    throw new Exception("Não é possível criar duas tasks com o mesmo nome.");
                }
            }

            var task = _mapper.Map<Domain.Entities.Task>(tasksDto);
            task.CreatedAt = DateTime.Now;
            task.Validate();

            var taskCreated = await _taskRepository.Create(task);
            return _mapper.Map<CreateTaskDto>(taskCreated);
        }
        catch (DomainExceptions)
        {
            throw new DomainExceptions("Erro na criação, por favor reveja os dados enviados e tente novamente.");
        }
    }

    public async Task<TasksDTO> Update(TasksDTO tasksDto)
    {
        var taskExists = await _taskRepository.Get(tasksDto.Id, tasksDto.UserId);
    
            if (taskExists == null)
            {
                throw new DomainExceptions("Task não encontrada, veja se o nome digitado está correto.");
            }
            
            var task = _mapper.Map<Domain.Entities.Task>(tasksDto);
            
            if (task.Concluded == null)
                task.Concluded = taskExists.Concluded;

            task = VerificandoPropriedades(taskExists, task);

            task.Validate();
    
            var taskUpdated = await _taskRepository.Update(task);
            return _mapper.Map<TasksDTO>(taskUpdated);
    }

    public async Task Remove(Guid userId, Guid id)
    {
        try
        {
            var allUsers = await VerificandoTasksUsuario(userId);

            foreach (var tasks in allUsers)
            {
                if(tasks.Id == id)
                    await _taskRepository.Remove(tasks.Id);
            
            }
        }
        catch (DomainExceptions)
        {
            throw new DomainExceptions("Veja se os dados estão digitados corretamente.");
        }
    }

    public async Task<TasksDTO> Get(Guid id, Guid userId)
    {
        var taskExists = await _taskRepository.Get(id, userId);

        if (taskExists == null)
        {
            throw new DomainExceptions("Task não encontrada, veja se o nome digitado está correto.");
        }
            
        return _mapper.Map<TasksDTO>(taskExists);
    }

    public async Task<List<TasksDTO>> SearchByUser(Guid id)
    {
        var allUsers = await VerificandoTasksUsuario(id);

        return _mapper.Map<List<TasksDTO>>(allUsers);
    }

    public async Task<List<TasksDTO>> Search(Guid? id, SearchTask seachTaskDto)
    {
        List<Domain.Entities.Task> search = new List<Domain.Entities.Task>();
        
        if(id != null)
        {
            search = await _taskRepository.Search((Guid)id, seachTaskDto);
        }
        else
        {
            search = await _taskRepository.Search(null, seachTaskDto);
        }

        if (search == null)
        {
            throw new DomainExceptions("Não foi encontrado nenhum item.");
        }

        return _mapper.Map<List<TasksDTO>>(search);
    }

    public async Task<List<Domain.Entities.Task>> VerificandoTasksUsuario(Guid id)
    {
        List<Domain.Entities.Task> tasksThisUser = await _taskRepository.Search(id, null);

        if (tasksThisUser == null)
        {
            throw new DomainExceptions("Esse usuário não possui tasks.");
        }

        return tasksThisUser;
    }

    public Domain.Entities.Task VerificandoPropriedades(Domain.Entities.Task tasksDto, Domain.Entities.Task task)
    {
        task.CreatedAt = tasksDto.CreatedAt;
        task.UpdatedAt = DateTime.Now;

        if (task.Concluded != null && tasksDto.Concluded != null)
        {
            if ((bool)task.Concluded && !(bool)tasksDto.Concluded)
                task.ConcludedAt = DateTime.Now;
            
            if (task.ConcludedAt == null && (bool)task.Concluded)
                task.ConcludedAt = tasksDto.ConcludedAt;

            if (tasksDto.ConcludedAt != null && !(bool)task.Concluded)
                task.ConcludedAt = null;
        }

        if (task.Name == null)
            task.Name = tasksDto.Name;

        if (task.Description == null)
            task.Description = tasksDto.Description;

        return task;
    }
}