﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SGMC.Test.DB.Common;
using SGMC.Test.DB.Entities;

namespace SGMC.Test.DB.Configurations;
internal sealed class LinkConfiguration : EntityTypeConfiguration<Link>
{
    public override void Configure(EntityTypeBuilder<Link> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id).HasColumnName("link_id");

        builder.Property(x => x.Quantity).IsRequired()
            .HasColumnName("count_parents");
        builder.Property(x => x.ParentId).IsRequired()
            .HasColumnName("quantity");
        builder.Property(x => x.NomenclatureId).IsRequired()
            .HasColumnName("nomenclature_id");

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Child)
            .HasForeignKey(x => x.ParentId);
        builder.HasOne(x => x.Nomenclature)
            .WithMany(x => x.Parents)
            .HasForeignKey(x => x.NomenclatureId);

        builder.HasIndex(x => new { x.ParentId, x.NomenclatureId })
            .IsUnique().HasDatabaseName("UQ_links_parent_id_nomenclature_id");

        builder.ToTable("links", table =>
        {
            table.HasCheckConstraint("CK_links_count_parents", "count_parents >= 0");
        });
    }
}
