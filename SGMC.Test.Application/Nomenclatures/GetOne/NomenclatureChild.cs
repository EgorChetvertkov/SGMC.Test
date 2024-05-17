namespace SGMC.Test.Application.Nomenclatures.GetOne;
public sealed class NomenclatureChild
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public long Quantity { get; init; }
    public List<NomenclatureChild> Children { get; init; }
    
    public NomenclatureChild(long id, string name, decimal price, long quantity, List<NomenclatureChild> children)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        Children = children;
    }

    public decimal GetPrice()
    {
        decimal selfPrice = Price * Quantity;

        decimal childrenPrice = Children.Sum(c => c.GetPrice());

        return selfPrice + childrenPrice;
    }
}
