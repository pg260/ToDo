using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels.filtro;

public class FiltroViewModel
{
    [MaxLength(50, ErrorMessage = "O nome deve conter no máximo 50 letras.")]
    public string? Name;
    
    [MaxLength(200, ErrorMessage = "A descrição só pode ter 200 caracteres.")]
    public string? Description;
    
    public bool? Concluded;
    
    public DateTime? Deadline;
    
    public DateTime? CreatedAt;
    
    public DateTime? UpdatedAt;
}