using DevFreela.Application.Commands.InsertUser;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateUserValidator : AbstractValidator<InsertUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.Email)
            .EmailAddress()
                .WithMessage("E-mail inválido");

        RuleFor(user => user.BirthDate)
            .Must(birthDate => birthDate < DateTime.Today.AddYears(-18))
                .WithMessage("Deve ser maior de idade");
    }
}