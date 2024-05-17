using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SGMC.Test.DB.Common;
using SGMC.Test.DB.Entities;

namespace SGMC.Test.DB.Configurations;
internal sealed class ProductMetaDataConfiguration : EntityTypeConfiguration<ProductMetaData>
{
    public override void Configure(EntityTypeBuilder<ProductMetaData> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id).HasColumnName("product_metadata_id");

        builder.Property(x => x.PropertyName).IsRequired().HasMaxLength(128)
            .HasColumnName("property_name");
        builder.Property(x => x.Value).IsRequired().HasMaxLength(1024)
            .HasColumnName("value");

        // NOTE : Можно внедрить следующий индекс в случае потребности поиска значения конкретного свойства конкретного объекта
        builder.HasIndex(x => new { x.NomenclatureId, x.PropertyName })
            .IsUnique().IncludeProperties(x => x.Value)
            .HasDatabaseName("UQ_product_metadata_nomenclature_property_name");

        builder.ToTable("product_metadata");
    }
}
