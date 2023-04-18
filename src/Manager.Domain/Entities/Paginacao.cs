namespace Manager.Domain.Entities;

public class Paginacao
{
    public Paginacao()
    {
        
    }
    public Paginacao(int? pagAtual, int? pTake)
    {
        PagAtual = pagAtual ?? 1;
        PTake = pTake ?? 20;
    }

    public int? PagAtual { get; set; }
    public int? PTake { get; set; }
}