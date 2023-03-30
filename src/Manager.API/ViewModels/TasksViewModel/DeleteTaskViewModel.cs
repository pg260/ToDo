using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels.TasksViewModel;

public class DeleteTaskViewModel
{
    [Required(ErrorMessage = "É preciso informar o id da task.")]
    public Guid Id { get; set; }
}