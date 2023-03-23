namespace Manager.Services.DTO.User;

public class UpdateUserDto
{
    public UpdateUserDto()
    {
        
    }
    public UpdateUserDto(Guid id, string name, string email, string newPassword, string password, string newPasswordConfirme)
    {
        Id = id;
        Name = name;
        Email = email;
        NewPassword = newPassword;
        Password = password;
        NewPasswordConfirme = newPasswordConfirme;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string Password { get; set; }
    public string NewPasswordConfirme { get; set; }
    
    
    
}