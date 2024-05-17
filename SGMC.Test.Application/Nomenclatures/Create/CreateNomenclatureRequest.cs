using ErrorOr;

using MediatR;

namespace SGMC.Test.Application.Nomenclatures.Create;
public sealed class CreateNomenclatureRequest : IRequest<ErrorOr<string>> //NOTE : вместо строки можно возвращать какой-то содержательный объект
{
    public string Name { get; init; }
    public decimal Price { get; init; }
    public Dictionary<string, string> Properties { get; init; }

    public CreateNomenclatureRequest(string name, decimal price, Dictionary<string, string> properties)
    {
        Name = name.Trim();
        Price = price;
        Properties = properties;
    }
}
