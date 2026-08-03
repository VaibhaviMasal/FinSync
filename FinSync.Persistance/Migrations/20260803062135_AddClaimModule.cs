using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinSync.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePlans_InsuranceCompanies_InsuranceCompanyCompanyId",
                table: "InsurancePlans");

            migrationBuilder.DropIndex(
                name: "IX_InsurancePlans_InsuranceCompanyCompanyId",
                table: "InsurancePlans");

            migrationBuilder.DropColumn(
                name: "InsuranceCompanyCompanyId",
                table: "InsurancePlans");

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimNumber = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IncidentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ClaimStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK_Claims_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimNumber",
                table: "Claims",
                column: "ClaimNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyId",
                table: "Claims",
                column: "PolicyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceCompanyCompanyId",
                table: "InsurancePlans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePlans_InsuranceCompanyCompanyId",
                table: "InsurancePlans",
                column: "InsuranceCompanyCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePlans_InsuranceCompanies_InsuranceCompanyCompanyId",
                table: "InsurancePlans",
                column: "InsuranceCompanyCompanyId",
                principalTable: "InsuranceCompanies",
                principalColumn: "CompanyId");
        }
    }
}
