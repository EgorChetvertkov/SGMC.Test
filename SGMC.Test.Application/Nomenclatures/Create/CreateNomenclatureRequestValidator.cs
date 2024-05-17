using FluentValidation;

namespace SGMC.Test.Application.Nomenclatures.Create;
public sealed class CreateNomenclatureRequestValidator : AbstractValidator<CreateNomenclatureRequest>
{
    public CreateNomenclatureRequestValidator()
    {
        RuleFor(x => x.Price).GreaterThan(0).NotNull();
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}
