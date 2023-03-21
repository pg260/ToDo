using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Infra.Interfaces;
using Manager.Services.DTO.Tasks;
using Manager.Services.Interfaces;

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
    private ITaskService _taskServiceImplementation;


    public async Task<CreateTaskDto> Create(TasksDTO tasksDto)
    {
        var taskExists = await _taskRepository.Get(tasksDto.Name);

        if (taskExists != null)
        {
            throw new DomainExceptions("Já existe uma task com esse nome.");
        }
        
        var task = _mapper.Map<Domain.Entities.Task>(tasksDto);
        task.Validate();

        var taskCreated = await _taskRepository.Create(task);
        return _mapper.Map<CreateTaskDto>(taskCreated);
    }

    public async Task<TasksDTO> Update(TasksDTO tasksDto)
    {
        var taskExists = await _taskRepository.Get(tasksDto.Name);

        if (taskExists == null)
        {
            throw new DomainExceptions("Task não encontrada, veja se o nome digitado está correto.");
        }

        var task = _mapper.Map<Domain.Entities.Task>(tasksDto);
        task.Validate();

        var taskUpdated = await _taskRepository.Update(task);
        return _mapper.Map<TasksDTO>(taskUpdated);
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TasksDTO> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TasksDTO>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<List<TasksDTO>> SearchByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<TasksDTO>> SearchByConcluded(bool concluded)
    {
        throw new NotImplementedException();
    }
}