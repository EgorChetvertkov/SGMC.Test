using ErrorOr;

using MediatR;

namespace SGMC.Test.Application.Nomenclatures.Delete;
public sealed class DeleteNomenclatureRequest : IRequest<ErrorOr<string>>
{
    public long Id { get; init; }

    public DeleteNomenclatureRequest(long id)
    {
        Id = id;
    }
}
