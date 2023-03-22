using Manager.Core.Exceptions;
using Manager.Domain.Validator;

namespace Manager.Domain.Entities;

public class User : Base
{
    public User()
    {
        
    }
    
    public User(Guid id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        _errors = new List<string>();
    }

    //Propriedades dos usuários
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    
    //Funções que afetam a classe Users
    public void ChangeName(string name)
    {
        Name = name;
        Validate();
    }

    public void ChangePassword(string password)
    {
        Password = password;
        Validate();
    }

    public void ChangeEmail(string email)
    {
        Email = email;
        Validate();
    }

    
    //Sistema de validação de usuário
    public override bool Validate()
    {
        //Cria uma instância da validação
        var validator = new UserValidator();
        //Inicia a validação
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                _errors?.Add((error.ErrorMessage));
            }
            throw new DomainExceptions($"Alguns campos estão inválidos {_errors?[0]}");
        }

        return true;
    }
}