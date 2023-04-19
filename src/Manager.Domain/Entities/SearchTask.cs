namespace Manager.Domain.Entities;

public class SearchTask
{
    public SearchTask()
    {
        PAtual = 1;
        PTake = 20;
    }

    public SearchTask(string? name, string? description, bool? concluded, int? pAtual, int? pTake, bool? orderByA, bool? orderByZ, bool? maiorQue, bool? menorQue, DateTime? createdTime)
    {
        Name = name;
        Description = description;
        Concluded = concluded;
        PAtual = pAtual ?? 1;
        PTake = pTake ?? 20;
        OrderByA = orderByA;
        OrderByZ = orderByZ;
        MaiorQue = maiorQue;
        MenorQue = menorQue;
        CreatedTime = createdTime;
    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Concluded { get; set; }
    public int? PAtual { get; set; }
    public int? PTake { get; set; }
    public bool? OrderByA { get; set; }
    public bool? OrderByZ { get; set; }
    public bool? MaiorQue { get; set; }
    public bool? MenorQue { get; set; }
    public DateTime? CreatedTime { get; set; }
}