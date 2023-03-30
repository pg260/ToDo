using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Services.DTO.Tasks;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Http;

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
        try
        {
            VerificandoTasksUsuario(tasksDto.UserId);

            var taskExists = await _taskRepository.Get(tasksDto.Id, tasksDto.UserId);
    
            if (taskExists == null)
            {
                throw new DomainExceptions("Task não encontrada, veja se o nome digitado está correto.");
            }
            
            var task = _mapper.Map<Domain.Entities.Task>(tasksDto);
            task.CreatedAt = taskExists.CreatedAt;
            // task.UpdatedAt = DateTime.Now;

            if (task.Concluded && !taskExists.Concluded)
                task.ConcludedAt = DateTime.Now;
            
            task.Validate();
    
            var taskUpdated = await _taskRepository.Update(task);
            return _mapper.Map<TasksDTO>(taskUpdated);
        }
        catch (DomainExceptions)
        {
            throw new DomainExceptions("Veja se os dados estão digitados corretamente e tente novamente");
        }
    }

    public async Task Remove(RemoveTaskDto removeTaskDto)
    {
        try
        {
            VerificandoTasksUsuario(removeTaskDto.UserId);

            List<Domain.Entities.Task> tasksThisUser = await _taskRepository.SearchByUser(removeTaskDto.UserId);
        
            foreach (var tasks in tasksThisUser)
            {
                if(tasks.Id == removeTaskDto.Id)
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

        VerificandoTasksUsuario(userId);

        var taskExists = await _taskRepository.Get(id, userId);

        if (taskExists == null)
        {
            throw new DomainExceptions("Task não encontrada, veja se o nome digitado está correto.");
        }
            
        return _mapper.Map<TasksDTO>(taskExists);
    }

    public async Task<List<TasksDTO>> SearchByConcluded(bool concluded, Guid userid)
    {
        try
        {
            List<Domain.Entities.Task> tasksThisUser = await _taskRepository.SearchByUser(userid);

            if (tasksThisUser == null)
            {
                throw new DomainExceptions("Esse usuário não possui tasks.");
            }

            var taskExists = await _taskRepository.SearchByConcluded(concluded, userid);

            if (taskExists == null)
            {
                throw new DomainExceptions("Nenhuma task foi concluida ainda.");
            }

            return _mapper.Map<List<TasksDTO>>(taskExists);
        }
        catch (DomainExceptions)
        {
            throw new DomainExceptions("Ocorreu algum erro, contate o suporte.");
        }
    }

    public async Task<List<TasksDTO>> SearchByUser(Guid id)
    {
        VerificandoTasksUsuario(id);

        var allUsers = await _taskRepository.SearchByUser(id);

        return _mapper.Map<List<TasksDTO>>(allUsers);
    }

    public async void VerificandoTasksUsuario(Guid id)
    {
        List<Domain.Entities.Task> tasksThisUser = await _taskRepository.SearchByUser(id);

        if (tasksThisUser == null)
        {
            throw new DomainExceptions("Esse usuário não possui tasks.");
        }
    }
}