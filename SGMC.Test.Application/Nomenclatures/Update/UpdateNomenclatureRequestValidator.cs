using FluentValidation;

namespace SGMC.Test.Application.Nomenclatures.Update;
public sealed class UpdateNomenclatureRequestValidator : AbstractValidator<UpdateNomenclatureRequest>
{
    public UpdateNomenclatureRequestValidator()
    {
        RuleFor(x => x.Price).GreaterThan(0).NotNull();
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}
