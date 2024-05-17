using SGMC.Test.DB.Common;

namespace SGMC.Test.DB.Entities;
public sealed class Nomenclature : Entity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public ICollection<Link> Child { get; set; } = null!;
    public ICollection<Link> Parents { get; set; } = null!;
    public ICollection<ProductMetaData> MetaData { get; set; } = null!;
}
