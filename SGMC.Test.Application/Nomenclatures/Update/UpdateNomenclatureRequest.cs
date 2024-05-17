using ErrorOr;

using MediatR;

namespace SGMC.Test.Application.Nomenclatures.Update;
public sealed class UpdateNomenclatureRequest : IRequest<ErrorOr<string>>
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public Dictionary<string, string> Properties { get; init; }

    public UpdateNomenclatureRequest(
        long id,
        string name, 
        decimal price, 
        Dictionary<string, string> properties)
    {
        Id = id;
        Name = name.Trim();
        Price = price;
        Properties = properties;
    }
}
