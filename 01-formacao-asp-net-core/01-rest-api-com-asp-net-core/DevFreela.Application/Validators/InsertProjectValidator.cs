using DevFreela.Application.Commands.InsertProject;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
{
    public InsertProjectValidator()
    {
        RuleFor(project => project.Title)
            .NotEmpty()
                .WithMessage("O campo precisa ser fornecido")
            .MaximumLength(50)
                .WithMessage("O campo deve ter o tamanho máximo de 50 caracteres");

        RuleFor(project => project.TotalCost)
            .GreaterThanOrEqualTo(1000)
                .WithMessage("O projeto deve custar ao menos R$1000");
    }
}