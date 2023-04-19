using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels.TasksViewModel;

public class SearchViewModel
{
    [MaxLength(50, ErrorMessage = "O nome deve conter no máximo 50 letras.")]
    public string? Name { get; set; }
    
    [MaxLength(200, ErrorMessage = "A descrição só pode ter no máximo 200 letras")]
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