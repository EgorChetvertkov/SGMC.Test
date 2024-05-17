using ErrorOr;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SGMC.Test.DB;
using SGMC.Test.DB.Entities;

namespace SGMC.Test.Application.Nomenclatures.GetOne;
public sealed class GetNomenclatureRequestHandler(
    ILogger<GetNomenclatureRequestHandler> logger,
    ApplicationDBContext context) : IRequestHandler<GetNomenclatureRequest, ErrorOr<NomenclatureData>>
{
    public async Task<ErrorOr<NomenclatureData>> Handle(GetNomenclatureRequest request, CancellationToken cancellationToken)
    {
        //NOTE : можно кэшировать результат, например с IMemoryCache
        // Еще как вариант оптимизации использовать Dapper или хранимую процедуру на стороне БД
        var allNomenclatures = await context.Nomenclatures
            .Include(x => x.Child)
            .ThenInclude(x => x.Nomenclature).ThenInclude(x => x.MetaData)
            .ToListAsync(cancellationToken);
        var root = allNomenclatures.SingleOrDefault(n => n.Id == request.Id);
        if (root is null)
        {
            logger.LogError("Nomenclatures {Id} not found", request.Id);
            return NomenclatureErrors.NotFound;
        }

        return new NomenclatureData(
            root.Id,
            root.Name,
            root.Price,
            root.MetaData.ToDictionary(x => x.PropertyName, x => x.Value),
            GetChild(root.Id, allNomenclatures));
    }

    private List<NomenclatureChild> GetChild(long id, List<Nomenclature> allNomenclatures)
    {
        return allNomenclatures
            .Where(x => x.Child.Any(x => x.ParentId == id))
            .Select(x => new NomenclatureChild(
                x.Id,
                x.Name, 
                x.Price, 
                x.Child.FirstOrDefault(x => x.ParentId == id)?.Quantity ?? 0, 
                GetChild(x.Id, allNomenclatures)))
            .ToList();
    }
}
