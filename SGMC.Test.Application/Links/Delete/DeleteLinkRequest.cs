using ErrorOr;

using MediatR;

namespace SGMC.Test.Application.Links.Delete;
public sealed class DeleteLinkRequest : IRequest<ErrorOr<string>>
{
    public long ParentId { get; init; }
    public long ChildId { get; init; }

    public DeleteLinkRequest(long parentId, long childId)
    {
        ParentId = parentId;
        ChildId = childId;
    }
}
