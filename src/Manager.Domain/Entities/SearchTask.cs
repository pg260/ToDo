namespace Manager.Domain.Entities;

public class SearchTask
{
    public SearchTask()
    {
        PAtual = 1;
        PTake = 20;
    }

    public SearchTask(string name, string description, bool? concluded, int? pAtual, int? pTake)
    {
        Name = name;
        Description = description;
        Concluded = concluded;
        PAtual = pAtual ?? 1;
        PTake = pTake ?? 20;
    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Concluded { get; set; }
    public int? PAtual { get; set; }
    public int? PTake { get; set; }
}