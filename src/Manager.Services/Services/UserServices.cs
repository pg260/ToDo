using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO.User;
using Manager.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Manager.Services.Services;

public class UserServices : IUserService
{
    public UserServices(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public async Task<CreateUserDto> Create(CreateUserDto userDto)
    {
        var userExists = await _userRepository.GetByEmail(userDto.Email);

        if (userExists != null)
        {
            throw new DomainExceptions("Esse email já está cadastrado.");
        }

        HashServices hashServices = new();
        var hash = hashServices.GenerateHash(userDto.Password);

        var user = _mapper.Map<User>(userDto);
        user.Password = hash;
        user.Validate();

        var userCreated = await _userRepository.Create(user);

        return _mapper.Map<CreateUserDto>(userCreated);
    }

    public async Task<UpdateUserDto> Update(UpdateUserDto userDto)
    {
        var userExists = await _userRepository.Get(userDto.Id);

        if (userExists == null)
        {
            throw new DomainExceptions("Usuário não encontrado");
        }

        HashServices hashServices = new();

        if (!hashServices.VerifyPassword(userDto.Password, userExists.Password))
        {
            throw new DomainExceptions("Senha incorreta, verifique e tente novamente");
        }

        if (userDto.NewPassword != userDto.NewPasswordConfirme)
        {
            throw new DomainExceptions("Senhas diferentes, verifique e tente novamente");
        }
        
        var hash = hashServices.GenerateHash(userDto.Password);
        
        var user = _mapper.Map<User>(userDto);
        user.Password = hash;
        user.Validate();
        
        var userUpdated = await _userRepository.Update(user);

        return _mapper.Map<UpdateUserDto>(userUpdated);
    }

    public async Task Remove(Guid id)
    {
        await _userRepository.Remove(id);
    }

    public async Task<UserDTO> Get(Guid id)
    {
        var user = await _userRepository.Get(id);

        return _mapper.Map<UserDTO>(user);
    }

    public async Task<List<UserDTO>> Get()
    {
        var allUsers = await _userRepository.Get();

        return _mapper.Map<List<UserDTO>>(allUsers);
    }

    public async Task<List<UserDTO>> SearchByName(string name)
    {
        var allUsers = await _userRepository.SearchByName(name);

        return _mapper.Map<List<UserDTO>>(allUsers);
    }

    public async Task<List<UserDTO>> SearchByEmail(string email)
    {
        var allUsers = await _userRepository.SearchByEmail(email);

        return _mapper.Map<List<UserDTO>>(allUsers);
    }

    public async Task<UserDTO?> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);

        return _mapper.Map<UserDTO>(user);
    }

}