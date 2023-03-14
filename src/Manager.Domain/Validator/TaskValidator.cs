
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
            .NotNull()
            .WithMessage("A descrição não pode ser nula")

            .NotEmpty()
            .WithMessage("A descrição não pode ser vazia")

            .MaximumLength(150)
            .WithMessage("A descrição deve conter no máximo 150 caracteres");

        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("O id de usuário não pode ser nulo")

            .NotEmpty()
            .WithMessage("O id de usuário não pode ser vazio");
    }
}