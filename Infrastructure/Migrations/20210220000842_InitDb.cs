using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cities_of_baku",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities_of_baku", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "photo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    src = table.Column<string>(type: "nvarchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_portfolio",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_portfolio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "region",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sale_manager",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    msisdn = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    sale_segment = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_manager", x => x.id);
                    table.ForeignKey(
                        name: "FK_sale_manager_user_id",
                        column: x => x.id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    role_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_manager_region",
                columns: table => new
                {
                    sale_manager_id = table.Column<Guid>(nullable: false),
                    region_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_manager_region", x => new { x.region_id, x.sale_manager_id });
                    table.ForeignKey(
                        name: "FK_sale_manager_region_region_region_id",
                        column: x => x.region_id,
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_manager_region_sale_manager_sale_manager_id",
                        column: x => x.sale_manager_id,
                        principalTable: "sale_manager",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_point",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sale_segment = table.Column<int>(nullable: false),
                    code_name = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    region_id = table.Column<int>(nullable: false),
                    city = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    village = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    gps_x = table.Column<decimal>(nullable: false),
                    gps_y = table.Column<decimal>(nullable: false),
                    shop_type = table.Column<int>(nullable: false),
                    shop_classification = table.Column<int>(nullable: false),
                    corporate_segment = table.Column<int>(nullable: false),
                    construction_type = table.Column<int>(nullable: false),
                    beton_supplier = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    juridical_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    OwnerOrDirector = table.Column<string>(nullable: true),
                    owner_msisdn = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    owner_date_of_birth = table.Column<DateTime>(nullable: true),
                    contact_person = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    contact_person_msisdn = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    contact_person_date_of_birth = table.Column<DateTime>(nullable: true),
                    shop_name = table.Column<string>(nullable: true),
                    shop_ownership = table.Column<int>(nullable: false),
                    shop_cement = table.Column<decimal>(nullable: false),
                    shop_beton = table.Column<decimal>(nullable: false),
                    shop_agg = table.Column<decimal>(nullable: false),
                    comment = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    photo_id = table.Column<int>(nullable: true),
                    sale_manager_id = table.Column<Guid>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_point", x => x.id);
                    table.ForeignKey(
                        name: "FK_sale_point_photo_photo_id",
                        column: x => x.photo_id,
                        principalTable: "photo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sale_point_region_region_id",
                        column: x => x.region_id,
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_point_sale_manager_sale_manager_id",
                        column: x => x.sale_manager_id,
                        principalTable: "sale_manager",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "crm_task",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sale_point_id = table.Column<int>(nullable: false),
                    purpose_of_visit = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    meeting_result = table.Column<string>(type: "nvarchar(3000)", nullable: true),
                    inserted_dt = table.Column<DateTime>(nullable: false),
                    visit_dt = table.Column<DateTime>(nullable: true),
                    closed_dt = table.Column<DateTime>(nullable: true),
                    inserted_user_id = table.Column<Guid>(nullable: false),
                    assigned_user_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_crm_task_user_inserted_user_id",
                        column: x => x.inserted_user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_crm_task_sale_manager_assigned_user_id",
                        column: x => x.assigned_user_id,
                        principalTable: "sale_manager",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_crm_task_sale_point_sale_point_id",
                        column: x => x.sale_point_id,
                        principalTable: "sale_point",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_point_photo",
                columns: table => new
                {
                    sale_point_id = table.Column<int>(nullable: false),
                    photo_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_point_photo", x => new { x.sale_point_id, x.photo_id });
                    table.ForeignKey(
                        name: "FK_sale_point_photo_photo_photo_id",
                        column: x => x.photo_id,
                        principalTable: "photo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_point_photo_sale_point_sale_point_id",
                        column: x => x.sale_point_id,
                        principalTable: "sale_point",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_point_portfolio",
                columns: table => new
                {
                    sale_pointId = table.Column<int>(nullable: false),
                    protfolio_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_point_portfolio", x => new { x.protfolio_id, x.sale_pointId });
                    table.ForeignKey(
                        name: "FK_sale_point_portfolio_product_portfolio_protfolio_id",
                        column: x => x.protfolio_id,
                        principalTable: "product_portfolio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_point_portfolio_sale_point_sale_pointId",
                        column: x => x.sale_pointId,
                        principalTable: "sale_point",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_point_seller",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fullname = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    date_of_birth = table.Column<DateTime>(nullable: true),
                    msisdn = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    sale_point_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_point_seller", x => x.id);
                    table.ForeignKey(
                        name: "FK_sale_point_seller_sale_point_sale_point_id",
                        column: x => x.sale_point_id,
                        principalTable: "sale_point",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "crm_task_photo",
                columns: table => new
                {
                    crm_task_id = table.Column<int>(nullable: false),
                    photo_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crm_task_photo", x => new { x.crm_task_id, x.photo_id });
                    table.ForeignKey(
                        name: "FK_crm_task_photo_crm_task_crm_task_id",
                        column: x => x.crm_task_id,
                        principalTable: "crm_task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_crm_task_photo_photo_photo_id",
                        column: x => x.photo_id,
                        principalTable: "photo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    content = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    sale_point_id = table.Column<int>(nullable: true),
                    task_id = table.Column<int>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    from_id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    to_id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:Collation", "latin1_swedish_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.id);
                    table.ForeignKey(
                        name: "FK_notification_crm_task_task_id",
                        column: x => x.task_id,
                        principalTable: "crm_task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notification_user_from_id",
                        column: x => x.from_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notification_sale_point_sale_point_id",
                        column: x => x.sale_point_id,
                        principalTable: "sale_point",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notification_user_to_id",
                        column: x => x.to_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[] { 2, "sale_manager" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "password", "username" },
                values: new object[] { new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"), "admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_crm_task_inserted_user_id",
                table: "crm_task",
                column: "inserted_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_crm_task_assigned_user_id",
                table: "crm_task",
                column: "assigned_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_crm_task_sale_point_id",
                table: "crm_task",
                column: "sale_point_id");

            migrationBuilder.CreateIndex(
                name: "IX_crm_task_photo_photo_id",
                table: "crm_task_photo",
                column: "photo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notification_task_id",
                table: "notification",
                column: "task_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notification_from_id",
                table: "notification",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_sale_point_id",
                table: "notification",
                column: "sale_point_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_to_id",
                table: "notification",
                column: "to_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_manager_region_sale_manager_id",
                table: "sale_manager_region",
                column: "sale_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_point_photo_id",
                table: "sale_point",
                column: "photo_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_point_region_id",
                table: "sale_point",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_point_sale_manager_id",
                table: "sale_point",
                column: "sale_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_point_photo_photo_id",
                table: "sale_point_photo",
                column: "photo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sale_point_portfolio_sale_pointId",
                table: "sale_point_portfolio",
                column: "sale_pointId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_point_seller_sale_point_id",
                table: "sale_point_seller",
                column: "sale_point_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_id",
                table: "user_role",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cities_of_baku");

            migrationBuilder.DropTable(
                name: "crm_task_photo");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "sale_manager_region");

            migrationBuilder.DropTable(
                name: "sale_point_photo");

            migrationBuilder.DropTable(
                name: "sale_point_portfolio");

            migrationBuilder.DropTable(
                name: "sale_point_seller");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "crm_task");

            migrationBuilder.DropTable(
                name: "product_portfolio");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "sale_point");

            migrationBuilder.DropTable(
                name: "photo");

            migrationBuilder.DropTable(
                name: "region");

            migrationBuilder.DropTable(
                name: "sale_manager");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
