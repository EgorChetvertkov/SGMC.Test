namespace SGMC.Test.Application.Nomenclatures.GetList;

public class NomenclaturesListItem
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }

    public NomenclaturesListItem(long id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}