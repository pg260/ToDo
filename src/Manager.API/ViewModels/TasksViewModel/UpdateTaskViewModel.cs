using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace;

public class UpdateTaskViewModel
{
    [Required(ErrorMessage = "É preciso informar o Id da task")]
    public Guid Id { get; set; }
    
    [MaxLength(50, ErrorMessage = "O nome deve conter no máximo 50 letras.")]
    public string? Name { get; set; }
    
    [MaxLength(200, ErrorMessage = "A descrição só pode ter no máximo 200 letras")]
    public string? Description { get; set; }
    public bool? Concluded { get; set; }
    public DateTime? Deadline { get; set; }
}