namespace Manager.Services.DTO.Tasks;

public class RemoveTaskDto
{
    public RemoveTaskDto(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}