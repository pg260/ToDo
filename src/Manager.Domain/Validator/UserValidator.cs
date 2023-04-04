using FluentValidation;
using Manager.Domain.Entities;

namespace Manager.Domain.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("A entidade não pode ser vazia")
            
            .NotNull()
            .WithMessage("A entidade não pode ser nula");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio")

            .NotNull()
            .WithMessage("O nome não pode ser nulo")

            .MinimumLength(3)
            .WithMessage("O nome deve ter mais que 3 caracteres")

            .MaximumLength(60)
            .WithMessage("O nome deve ter no máximo 60 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("A senha não pode ser vazia")

            .NotNull()
            .WithMessage("A senha não pode ser nula")

            .MinimumLength(6)
            .WithMessage("A senha deve ter no mínimo 6 caracteres")

            .MaximumLength(88)
            .WithMessage("A senha deve ter no máximo 40 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("O email não pode ser vazio")

            .NotNull()
            .WithMessage("O email não pode ser nulo")

            .MinimumLength(7)
            .WithMessage("O email deve ter no mínimo 7 caracteres")

            .MaximumLength(60)
            .WithMessage("O email deve ter no máximo 60 caracteres")
            
            .EmailAddress()
            .WithMessage("Email inválido");
    }
}