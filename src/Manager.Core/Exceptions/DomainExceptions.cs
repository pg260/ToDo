namespace Manager.Core.Exceptions;

public class DomainExceptions : Exception
{
    internal List<string> _errors;
    public List<string> Errors => _errors;

    public DomainExceptions()
    {
        
    }

    public DomainExceptions(List<string> errors, string message) : base(message)
    {
        _errors = errors;
    }

    public DomainExceptions(string message) : base(message)
    {
        
    }

    public DomainExceptions(string message, Exception innerExceptions) : base(message, innerExceptions)
    {
        
    }
}