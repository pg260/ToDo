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

    public async Task<CreateTaskDto> Create(CreateTaskDto tasksDto)
    {
        try
        {
            List<Domain.Entities.Task> tasksThisUser = await _taskRepository.SearchByUser(tasksDto.UserId);
            
            foreach (var tasks in tasksThisUser)
            {
                if(tasks.Name == tasksDto.Name)
                    throw new DomainExceptions("Não pode ter dois nomes iguais.");
            }

            var task = _mapper.Map<Domain.Entities.Task>(tasksDto);
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
        try
        {
            List<Domain.Entities.Task> tasksThisUser = await _taskRepository.SearchByUser(tasksDto.UserId);

            if (tasksThisUser == null)
            {
                throw new DomainExceptions("Esse usuário não possui tasks.");
            }

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
        catch (DomainExceptions)
        {
            throw new DomainExceptions("Veja se os dados estão digitados corretamente e tente novamente");
        }
    }

    public async Task Remove(RemoveTaskDto tasksDto)
    {
        try
        {
            List<Domain.Entities.Task> tasksThisUser = await _taskRepository.SearchByUser(tasksDto.UserId);

            if (tasksThisUser == null)
            {
                throw new DomainExceptions("Esse usuário já não possui nenhuma task.");
            }
        
            foreach (var tasks in tasksThisUser)
            {
                if(tasks.Name == tasksDto.Name)
                    await _taskRepository.Remove(tasks.Name);
            
            }
        }
        catch (DomainExceptions)
        {
            throw new DomainExceptions("Veja se os dados estão digitados corretamente.");
        }
    }

    public async Task<TasksDTO> Get(TasksDTO tasksDto)
    {
        try
        {
            List<Domain.Entities.Task> tasksThisUser = await _taskRepository.SearchByUser(tasksDto.UserId);

            if (tasksThisUser == null)
            {
                throw new DomainExceptions("Esse usuário não possui tasks.");
            }

            var taskExists = await _taskRepository.Get(tasksDto.Name);

            if (taskExists == null)
            {
                throw new DomainExceptions("Task não encontrada, veja se o nome digitado está correto.");
            }
            
            return _mapper.Map<TasksDTO>(taskExists);
        }
        catch (DomainExceptions)
        {
            throw new DomainExceptions("Veja se os dados estão digitados corretamente.");
        }
    }

    public async Task<TasksDTO> Get(string name)
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