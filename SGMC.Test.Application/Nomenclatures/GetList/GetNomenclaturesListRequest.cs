using MediatR;

namespace SGMC.Test.Application.Nomenclatures.GetList;
public sealed class GetNomenclaturesListRequest : IRequest<List<NomenclaturesListItem>>
{
    public GetNomenclaturesListRequest() { }
}
