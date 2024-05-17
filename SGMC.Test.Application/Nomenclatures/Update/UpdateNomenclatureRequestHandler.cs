using ErrorOr;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SGMC.Test.DB;
using SGMC.Test.DB.Entities;

namespace SGMC.Test.Application.Nomenclatures.Update;
public sealed class UpdateNomenclatureRequestHandler(
    ILogger<UpdateNomenclatureRequestHandler> logger,
    ApplicationDBContext context) : IRequestHandler<UpdateNomenclatureRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(UpdateNomenclatureRequest request, CancellationToken cancellationToken)
    {
        var nameExists = await context.Nomenclatures
            .AnyAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);
        if (nameExists)
        {
            logger.LogError("Nomenclature with name {Name} already exists", request.Name);
            return NomenclatureErrors.NameAlreadyExists;
        }

        var nomenclature = await context.Nomenclatures
            .Where(x => x.Id == request.Id)
            .Include(x => x.MetaData)
            .SingleOrDefaultAsync(cancellationToken);
        if (nomenclature is null)
        {
            logger.LogError("Nomenclature with id {Id} not found", request.Id);
            return NomenclatureErrors.NotFound;
        }

        context.ProductMetaData.RemoveRange(nomenclature.MetaData);

        nomenclature.Name = request.Name;
        nomenclature.Price = request.Price;

        context.ProductMetaData.AddRange(request.Properties
            .Select(x => new ProductMetaData()
            {
                NomenclatureId = nomenclature.Id,
                PropertyName = x.Key,
                Value = x.Value
            }));

        await context.SaveChangesAsync(cancellationToken);

        return $"Продукция {request.Name} обновлена";
    }
}
