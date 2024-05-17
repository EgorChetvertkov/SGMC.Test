using ErrorOr;

using MediatR;

namespace SGMC.Test.Application.Links.Create;
public sealed class CreateLinkRequest : IRequest<ErrorOr<string>>
{
    public long ParentId { get; init; }
    public long ChildId { get; init; }
    public long Quantity { get; init; }

    public CreateLinkRequest(long parentId, long childId, long quantity)
    {
        ParentId = parentId;
        ChildId = childId;
        Quantity = quantity;
    }
}
