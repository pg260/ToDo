namespace Manager.Services.DTO.Tasks;

public class RemoveTaskDto
{
    public RemoveTaskDto(string name, Guid userId)
    {
        Name = name;
        UserId = userId;
    }

    public string Name { get; set; }
    public Guid UserId { get; set; }
}