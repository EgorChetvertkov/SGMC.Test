using ErrorOr;

using MediatR;

namespace SGMC.Test.Application.Nomenclatures.GetOne;
public sealed class GetNomenclatureRequest : IRequest<ErrorOr<NomenclatureData>>
{
    public long Id { get; init; }

    public GetNomenclatureRequest(long id)
    {
        Id = id;
    }
}
