using Manager.Domain.Validator;

namespace Manager.Domain.Entities;

public class Task : Base
{
    public Task(string description, Guid userId, bool concluded, DateTime concludedAt, DateTime deadline)
    {
        Description = description;
        UserId = userId;
        Concluded = concluded;
        ConcludedAt = concludedAt;
        Deadline = deadline;
    }

    public string Description { get; set; }
    public Guid UserId { get; set; }
    public bool Concluded { get; set; }
    public DateTime ConcludedAt { get; set; }
    public DateTime Deadline { get; set; }
    
    
    public override bool Validate()
    {
        var validate = new TaskValidator();
        var validation = validate.Validate(this);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                _errors?.Add((error.ErrorMessage));
            }
            throw new Exception($"Alguns campos estão inválidos {_errors?[0]}");
        }

        return true;
    }
}