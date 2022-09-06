using FluentValidation;

namespace CardStorageService.Controllers.Models.Validations;

public class CreateClientRequestValidator: AbstractValidator<CreateClientRequest>
{
    public CreateClientRequestValidator()
    {
        RuleSet("Names", () =>
        {
            RuleFor(x => x.Patronymic).NotNull().Length(5, 255).Matches("^[a-zA-Z]+$");
            RuleFor(x => x.FirstName).NotNull().Length(5, 255).Matches("^[a-zA-Z]+$");
            RuleFor(x => x.Surname).NotNull().Length(5, 255).Matches("^[a-zA-Z]+$");

        });
    }
}