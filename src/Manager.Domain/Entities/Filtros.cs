namespace Manager.Domain.Entities;

public class Filtros
{
    public Filtros(string name, string description, bool? concluded, DateTime? deadlineEnd, DateTime? createdAtStart, DateTime? updatedAtEnd)
    {
        Name = name;
        Description = description;
        Concluded = concluded;
        Deadline = deadlineEnd;
        CreatedAt = createdAtStart;
        UpdatedAt = updatedAtEnd;
    }

    public string? Name;
    public string? Description;
    public bool? Concluded;
    public DateTime? Deadline;
    public DateTime? CreatedAt;
    public DateTime? UpdatedAt;
}