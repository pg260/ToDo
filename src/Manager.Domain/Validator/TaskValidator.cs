
using System.Data;
using FluentValidation;
using Task = Manager.Domain.Entities.Task;

namespace Manager.Domain.Validator;

public class TaskValidator : AbstractValidator<Task>
{
    public TaskValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("A tarefa não pode ser nula")

            .NotEmpty()
            .WithMessage("A tarefa não pode ser vazia");

        RuleFor(x => x.Description)
            .MaximumLength(200)
            .WithMessage("A descrição deve conter no máximo 200 caracteres");

        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("O id de usuário não pode ser nulo")

            .NotEmpty()
            .WithMessage("O id de usuário não pode ser vazio");
        
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("O nome não pode ser nulo")

            .NotEmpty()
            .WithMessage("O nome não pode ser vazio")

            .MaximumLength(50)
            .WithMessage("O nome não pode passar de 50 caracteres");
    }
    
}