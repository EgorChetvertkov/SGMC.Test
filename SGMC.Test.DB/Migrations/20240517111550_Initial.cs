using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SGMC.Test.DB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nomenclatures",
                columns: table => new
                {
                    nomenclature_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nomenclatures", x => x.nomenclature_id);
                    table.CheckConstraint("CK_nomenclatures_price", "price >= 0.0");
                });

            migrationBuilder.CreateTable(
                name: "links",
                columns: table => new
                {
                    link_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomenclature_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    count_parents = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_links", x => x.link_id);
                    table.CheckConstraint("CK_links_count_parents", "count_parents >= 0");
                    table.ForeignKey(
                        name: "FK_links_nomenclatures_nomenclature_id",
                        column: x => x.nomenclature_id,
                        principalTable: "nomenclatures",
                        principalColumn: "nomenclature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_links_nomenclatures_quantity",
                        column: x => x.quantity,
                        principalTable: "nomenclatures",
                        principalColumn: "nomenclature_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_metadata",
                columns: table => new
                {
                    product_metadata_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    property_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    value = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    NomenclatureId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_metadata", x => x.product_metadata_id);
                    table.ForeignKey(
                        name: "FK_product_metadata_nomenclatures_NomenclatureId",
                        column: x => x.NomenclatureId,
                        principalTable: "nomenclatures",
                        principalColumn: "nomenclature_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_links_nomenclature_id",
                table: "links",
                column: "nomenclature_id");

            migrationBuilder.CreateIndex(
                name: "UQ_links_parent_id_nomenclature_id",
                table: "links",
                columns: new[] { "quantity", "nomenclature_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_nomenclatures_name",
                table: "nomenclatures",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_product_metadata_nomenclature_property_name",
                table: "product_metadata",
                columns: new[] { "NomenclatureId", "property_name" },
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "value" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "links");

            migrationBuilder.DropTable(
                name: "product_metadata");

            migrationBuilder.DropTable(
                name: "nomenclatures");
        }
    }
}
