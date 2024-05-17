using ErrorOr;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SGMC.Test.DB;
using SGMC.Test.DB.Entities;

namespace SGMC.Test.Application.Nomenclatures.Create;
public sealed class CreateNomenclatureRequestHandler(
    ILogger<CreateNomenclatureRequestHandler> logger,
    ApplicationDBContext context) : IRequestHandler<CreateNomenclatureRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CreateNomenclatureRequest request, CancellationToken cancellationToken)
    {
        var nameExists = await context.Nomenclatures
            .AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (nameExists)
        {
            logger.LogError("Nomenclature with name {Name} already exists", request.Name);
            return NomenclatureErrors.NameAlreadyExists;
        }

        context.Nomenclatures.Add(new()
        {
            Name = request.Name,
            Price = request.Price,
            MetaData = request.Properties
                .Select(x => new ProductMetaData()
                {
                    PropertyName = x.Key,
                    Value = x.Value,
                }).ToList()
        });

        await context.SaveChangesAsync(cancellationToken);

        return $"Продукция {request.Name} добавлена";
    }
}
