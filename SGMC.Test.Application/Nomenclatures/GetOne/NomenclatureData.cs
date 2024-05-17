namespace SGMC.Test.Application.Nomenclatures.GetOne;

public sealed class NomenclatureData
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public Dictionary<string, string> Properties { get; init; }
    public List<NomenclatureChild> Children { get; init; }
    public decimal FullPrice => Price + Children.Sum(c => c.GetPrice());

    public NomenclatureData(long id, string name, decimal price, Dictionary<string, string> properties, List<NomenclatureChild> children)
    {
        Id = id;
        Name = name;
        Price = price;
        Properties = properties;
        Children = children;
    }
}