using Microsoft.EntityFrameworkCore.Migrations;

namespace RunningJac.IDP.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConcurrencyStamp", "IsActive", "Password", "Subject", "Username" },
                values: new object[] { "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf", null, true, "#&okodf1t90ejyuhDF", "e7cd9d8e-38e9-4b6a-a7b0-8c17b4cf10d1", "Odira" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConcurrencyStamp", "IsActive", "Password", "Subject", "Username" },
                values: new object[] { "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6", null, true, "#%6wr90ujkjwmklOLw", "f9f95db5-82b3-4aa6-84b7-35c586be8042", "Somto" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { "b37d1681-6d0c-4cb5-8d98-0a446f9765e1", null, "given_name", "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf", "Odira" },
                    { "c03d7464-2cf2-4f3c-81b0-3f7da739e383", null, "family_name", "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf", "Ike" },
                    { "a0f1b4f0-3809-4c4f-a3f0-17787d2c013c", null, "role", "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf", "admin" },
                    { "9a0bfa87-df71-4b07-a3c3-9ec61b7ec2a5", null, "country", "dab7b0a2-54a9-4d32-94e4-9a7f8d26ecdf", "ng" },
                    { "c8c66c9f-b309-417b-9b4b-785c02f6f3d8", null, "given_name", "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6", "Somto" },
                    { "4bdfec12-318a-4df6-a1a7-94272905724e", null, "family_name", "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6", "Ikewelugo" },
                    { "4ef412d3-8e06-4ab3-b2a1-7e2246824cd9", null, "role", "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6", "regular_user" },
                    { "946bc5b4-0a3a-4f47-946d-94a149e24256", null, "country", "8e48e3d7-11f9-4ecb-9cd5-9514fe4a09b6", "ng" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
