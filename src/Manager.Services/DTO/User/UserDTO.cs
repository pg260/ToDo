namespace Manager.Services.DTO.User;

public class UserDTO
{
    public UserDTO()
    {
    }

    public UserDTO(Guid id, string role, string name, string email, string password)
    {
        Id = id;
        Role = role;
        Name = name;
        Email = email;
    }

    public Guid Id { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}