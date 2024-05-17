using ErrorOr;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SGMC.Test.DB;

namespace SGMC.Test.Application.Links.Delete;
public sealed class DeleteLinkRequestHandler(
    ILogger<DeleteLinkRequestHandler> logger,
    ApplicationDBContext context) : IRequestHandler<DeleteLinkRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(DeleteLinkRequest request, CancellationToken cancellationToken)
    {
        var link = await context.Links
            .Where(x => x.ParentId == request.ParentId && x.NomenclatureId == request.ChildId)
            .Include(x => x.Parent)
            .Include(x => x.Nomenclature)
            .SingleOrDefaultAsync(cancellationToken);
        if (link is null)
        {
            logger.LogError("Link with parent {ParentId} and child {ChildId} not found", request.ParentId, request.ChildId);
            return LinkErrors.NotFound;
        }

        context.Links.Remove(link);
        await context.SaveChangesAsync(cancellationToken);

        return $"Связь между {link.Parent.Name} и  {link.Nomenclature.Name} удалена";
    }
}
