namespace Manager.Domain.Entities;

public class Filtros
{

    public Filtros()
    {
        
    }
    public Filtros(string? name, string? description, bool? concluded, bool? orderByTop, bool? orderByFall)
    {
        Name = name;
        Description = description;
        Concluded = concluded;
        OrderByTop = orderByTop;
        OrderByFall = orderByFall;
    }

    public string? Name;
    public string? Description;
    public bool? Concluded;
    public bool? OrderByTop;
    public bool? OrderByFall;

    // public DateTime? Deadline;
    /*public DateTime? CreatedAt;
    public DateTime? UpdatedAt;*/
}