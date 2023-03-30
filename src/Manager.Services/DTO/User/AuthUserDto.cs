namespace Manager.Services.DTO.User;

public class AuthUserDto
{
    public AuthUserDto(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}