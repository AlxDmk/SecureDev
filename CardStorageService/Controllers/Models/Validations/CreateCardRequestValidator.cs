using CardStorageService.Controllers.Models.Requests;
using FluentValidation;

namespace CardStorageService.Controllers.Models.Validations;

public class CreateCardRequestValidator: AbstractValidator<CreateCardRequest>
{
    public CreateCardRequestValidator()
    {
        int i = 0;
        RuleFor(x => x.Name).NotNull().Length(5, 255).Matches("^[a-zA-Z]+$");
        RuleFor(x => x.CardNo).CreditCard();
        RuleFor(x => x.ClientId).Must(x => x > 0);
        RuleFor(x => x.CVV2).NotNull().Length(3, 3).Must(x => int.TryParse(x, out i));
        
    }
}