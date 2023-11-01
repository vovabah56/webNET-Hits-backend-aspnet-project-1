using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Delivery.Migrations
{
    /// <inheritdoc />
    public partial class addAdderessGar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsAddrObjs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectId = table.Column<long>(type: "bigint", nullable: false),
                    ObjectGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: false),
                    OperTypeId = table.Column<int>(type: "integer", nullable: true),
                    PrevId = table.Column<long>(type: "bigint", nullable: true),
                    NextId = table.Column<long>(type: "bigint", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActual = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsAddrObjs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AsAdmHierarchies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectId = table.Column<long>(type: "bigint", nullable: true),
                    ParentObjectId = table.Column<long>(type: "bigint", nullable: true),
                    ChangeId = table.Column<long>(type: "bigint", nullable: true),
                    RegionCode = table.Column<string>(type: "text", nullable: false),
                    AreaCode = table.Column<string>(type: "text", nullable: false),
                    CityCode = table.Column<string>(type: "text", nullable: false),
                    PlaceCode = table.Column<string>(type: "text", nullable: false),
                    PlanCode = table.Column<string>(type: "text", nullable: false),
                    StreetCode = table.Column<string>(type: "text", nullable: false),
                    PrevId = table.Column<long>(type: "bigint", nullable: true),
                    NextId = table.Column<long>(type: "bigint", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsAdmHierarchies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AsHouses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectId = table.Column<long>(type: "bigint", nullable: false),
                    ObjectGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeId = table.Column<long>(type: "bigint", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: false),
                    AddNum1 = table.Column<string>(type: "text", nullable: false),
                    AddNum2 = table.Column<string>(type: "text", nullable: false),
                    HouseType = table.Column<int>(type: "integer", nullable: true),
                    AddType1 = table.Column<int>(type: "integer", nullable: true),
                    AddType2 = table.Column<int>(type: "integer", nullable: true),
                    OperTypeId = table.Column<int>(type: "integer", nullable: true),
                    PrevId = table.Column<long>(type: "bigint", nullable: true),
                    NextId = table.Column<long>(type: "bigint", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActual = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsHouses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsAddrObjs");

            migrationBuilder.DropTable(
                name: "AsAdmHierarchies");

            migrationBuilder.DropTable(
                name: "AsHouses");
        }
    }
}
