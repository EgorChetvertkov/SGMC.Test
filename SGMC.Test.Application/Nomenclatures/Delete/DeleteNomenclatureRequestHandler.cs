using ErrorOr;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SGMC.Test.DB;

namespace SGMC.Test.Application.Nomenclatures.Delete;
public sealed class DeleteNomenclatureRequestHandler(
    ILogger<DeleteNomenclatureRequestHandler> logger,
    ApplicationDBContext context) : IRequestHandler<DeleteNomenclatureRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(DeleteNomenclatureRequest request, CancellationToken cancellationToken)
    {
        var nomenclature = await context.Nomenclatures
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (nomenclature is null)
        {
            logger.LogError("Nomenclatures {Id} not found", request.Id);
            return NomenclatureErrors.NotFound;
        }

        context.Nomenclatures.Remove(nomenclature);

        await context.SaveChangesAsync(cancellationToken);

        return $"Продукция {nomenclature.Name} успешно удалена";
    }
}
