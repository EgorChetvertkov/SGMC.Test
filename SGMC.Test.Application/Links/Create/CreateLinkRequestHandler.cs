using ErrorOr;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SGMC.Test.DB;

namespace SGMC.Test.Application.Links.Create;
public sealed class CreateLinkRequestHandler(
    ILogger<CreateLinkRequestHandler> logger,
    ApplicationDBContext context) : IRequestHandler<CreateLinkRequest, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(CreateLinkRequest request, CancellationToken cancellationToken)
    {
        bool isExits = await context.Links
            .AnyAsync(x => x.ParentId == request.ParentId && x.NomenclatureId == request.ChildId);
        if (isExits)
        {
            logger.LogError("Link already exists");
            return LinkErrors.LinkAlreadyExists;
        }

        var parent = await context.Nomenclatures
            .SingleOrDefaultAsync(x => x.Id == request.ParentId, cancellationToken);
        if (parent is null)
        {
            logger.LogError("Parent not found");
            return LinkErrors.ParentNotFound;
        }

        var child = await context.Nomenclatures
            .SingleOrDefaultAsync(x => x.Id == request.ChildId, cancellationToken);
        if (child is null)
        {
            logger.LogError("Child not found");
            return LinkErrors.ChildNotFound;
        }

        context.Links.Add(new()
        {
            ParentId = request.ParentId,
            NomenclatureId = request.ChildId,
            Quantity = request.Quantity
        });
        await context.SaveChangesAsync(cancellationToken);
        return $"Связь между {parent.Name} и  {child.Name} добавлена";
    }
}
