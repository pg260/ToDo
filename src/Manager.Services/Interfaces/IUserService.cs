using Manager.Services.DTO;
using Manager.Services.DTO.User;

namespace Manager.Services.Interfaces;

public interface IUserService
{
    Task<CreateUserDto> Create(CreateUserDto userDTO);

    Task<UpdateUserDto> Update(UpdateUserDto userDTO);
    Task Remove(Guid id);
    Task<UserDTO> Get(Guid id);
    Task<List<UserDTO>> Get();
    Task<List<UserDTO>> SearchByName(string name);
    Task<List<UserDTO>> SearchByEmail(string email);
    Task<UserDTO?> GetByEmail(string email);
}