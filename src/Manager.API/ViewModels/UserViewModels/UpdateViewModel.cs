using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels.UserViewModels;

public class UpdateViewModel
{
    [Required(ErrorMessage = "O nome não pode ser vazio.")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
    [MaxLength(60, ErrorMessage = "O nome deve ter no máximo 60 caracteres.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O email não pode ser vazio.")]
    [MinLength(7, ErrorMessage = "O email deve ter no mínimo 7 caracteres.")]
    [MaxLength(60, ErrorMessage = "O email deve ter no máximo 60 caracteres.")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "A senha não pode ser vazia.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    [MaxLength(40, ErrorMessage = "A senha deve ter no máximo 40 caracteres.")]
    public string NewPassword { get; set; }
    
    [Required(ErrorMessage = "A senha não pode ser vazia.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    [MaxLength(40, ErrorMessage = "A senha deve ter no máximo 40 caracteres.")]
    public string NewPasswordConfirme { get; set; }
    
    [Required(ErrorMessage = "A senha não pode ser vazia.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    [MaxLength(40, ErrorMessage = "A senha deve ter no máximo 40 caracteres.")]
    public string Password { get; set; }
}