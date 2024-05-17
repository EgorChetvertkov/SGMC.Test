using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SGMC.Test.DB;

namespace SGMC.Test.Application.Nomenclatures.GetList;
public sealed class GetNomenclaturesListRequestHandler(
    ILogger<GetNomenclaturesListRequestHandler> logger,
    ApplicationDBContext context) : IRequestHandler<GetNomenclaturesListRequest, List<NomenclaturesListItem>>
{
    public async Task<List<NomenclaturesListItem>> Handle(GetNomenclaturesListRequest request, CancellationToken cancellationToken)
    {
        return await context.Nomenclatures
            .AsNoTracking()
            .Select(x => new NomenclaturesListItem(
                x.Id,
                x.Name,
                x.Price))
            .ToListAsync(cancellationToken);
    }
}
