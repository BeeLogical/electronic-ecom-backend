using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend_root.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:sale_status_enum", "completed,pending")
                .Annotation("Npgsql:Enum:status_enum", "pending,active,inactive");

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("regions_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    status = table.Column<int>(type: "status_enum", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    region_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ImagePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_regions_region_id",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_transactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    sale_status = table.Column<int>(type: "sale_status_enum", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    region_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sales_transactions_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_sales_transactions_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sales_transactions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_region_id",
                table: "products",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_transactions_product_id",
                table: "sales_transactions",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_transactions_user_id",
                table: "sales_transactions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "users_email_key",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "sales_transactions");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "regions");
        }
    }
}
