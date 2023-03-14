namespace Manager.Domain.Entities;

public abstract class Base
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    internal List<string>? _errors;
    public IReadOnlyCollection<string>? Errors => _errors;

    public abstract bool Validate();
}