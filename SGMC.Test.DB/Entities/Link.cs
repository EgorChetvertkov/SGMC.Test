using SGMC.Test.DB.Common;

namespace SGMC.Test.DB.Entities;
public sealed class Link : Entity
{
    public long NomenclatureId { get; set; }
    public long ParentId { get; set; }
    public long Quantity { get; set; }

    public Nomenclature Nomenclature { get; set; } = null!;
    public Nomenclature Parent { get; set; } = null!;
}
