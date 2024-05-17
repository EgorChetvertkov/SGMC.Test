using SGMC.Test.DB.Common;

namespace SGMC.Test.DB.Entities;
public sealed class ProductMetaData : Entity
{
    // NOTE : По заданию требуется использование отдельной сущности, на практике при использовании PostgreSQL эффективнее было бы использовать jsonb-тип как атрибут сущности Nomenclature
    public string PropertyName { get; set; } = null!;
    public string Value { get; set; } = null!;
    public long NomenclatureId { get; set; }

    public Nomenclature Nomenclature { get; set; } = null!;
}
