namespace Manager.Services.DTO.Tasks;

public class TasksDTO
{
    public TasksDTO()
    {
        
    }

    public TasksDTO(Guid id, string name, string? description, Guid userId, short concluded, DateTime? concludedAt, DateTime? deadline, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        Description = description;
        UserId = userId;
        Concluded = concluded;
        ConcludedAt = concludedAt;
        Deadline = deadline;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public short Concluded { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}