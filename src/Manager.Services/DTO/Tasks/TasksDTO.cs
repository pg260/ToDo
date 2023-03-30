namespace Manager.Services.DTO.Tasks;

public class TasksDTO
{
    public TasksDTO()
    {
        
    }

    public TasksDTO(Guid id, string name, string? description, Guid userId, bool concluded, DateTime? concludedAt, DateTime? deadline)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Description = description;
        Concluded = concluded;
        ConcludedAt = concludedAt;
        Deadline = deadline;
        UpdatedAt = DateTime.Now;
    }

    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Concluded { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}