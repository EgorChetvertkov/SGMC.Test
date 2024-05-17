using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SGMC.Test.DB.Common;
using SGMC.Test.DB.Entities;

namespace SGMC.Test.DB.Configurations;
internal sealed class NomenclatureConfiguration : EntityTypeConfiguration<Nomenclature>
{
    public override void Configure(EntityTypeBuilder<Nomenclature> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id).HasColumnName("nomenclature_id");

        builder.Property(x => x.Name).IsRequired().HasMaxLength(256)
            .HasColumnName("name");
        builder.Property(x => x.Price).IsRequired()
            .HasColumnName("price");

        builder.HasIndex(x => x.Name)
            .IsUnique().HasDatabaseName("UQ_nomenclatures_name");

        builder.ToTable("nomenclatures", table =>
        {
            table.HasCheckConstraint("CK_nomenclatures_price", "price >= 0.0");
        });
    }
}
