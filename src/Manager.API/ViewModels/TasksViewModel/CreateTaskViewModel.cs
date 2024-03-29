using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels.TasksViewModel;

public class CreateTaskViewModel
{

    [Required(ErrorMessage = "É preciso dar um nome para a task.")]
    [MaxLength(50, ErrorMessage = "O nome deve conter no máximo 50 letras.")]
    public string Name { get; set; }
    
    [MaxLength(200, ErrorMessage = "A descrição só pode ter no máximo 200 letras")]
    public string? Description { get; set; }
    
    public DateTime? Deadline { get; set; }
}