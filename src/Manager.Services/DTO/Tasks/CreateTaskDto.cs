namespace Manager.Services.DTO.Tasks;

public class CreateTaskDto
{
    public CreateTaskDto(string name, string? description, DateTime createdAt, DateTime? deadline, short concluded)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        Deadline = deadline;
        Concluded = concluded;
    }

    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public short Concluded { get; set; }
    public DateTime? Deadline { get; set; }
}