namespace SGMC.Test.Contracts;

public sealed class CreateOrUpdateNomenclature
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Dictionary<string, string> Properties { get; set; }
}
