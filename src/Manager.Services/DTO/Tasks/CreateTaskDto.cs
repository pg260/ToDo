namespace Manager.Services.DTO.Tasks;

public class CreateTaskDto
{
    public CreateTaskDto(Guid userId, string name, string? description, DateTime createdAt, short concluded, DateTime? deadline)
    {
        UserId = userId;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        Concluded = concluded;
        Deadline = deadline;
    }

    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public short Concluded { get; set; }
    public DateTime? Deadline { get; set; }
}