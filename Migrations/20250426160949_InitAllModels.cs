using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotificationService_Kisen.Migrations
{
    /// <inheritdoc />
    public partial class InitAllModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    SubscriberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriberType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiveNewAlerts = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.SubscriberId);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberRegions",
                columns: table => new
                {
                    SubscriberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberRegions", x => new { x.SubscriberId, x.RegionId });
                    table.ForeignKey(
                        name: "FK_SubscriberRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriberRegions_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "SubscriberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionId", "Name" },
                values: new object[,]
                {
                    { 1, "Kyiv" },
                    { 2, "Kharkiv" },
                    { 3, "Odesa" },
                    { 4, "Dnipro" },
                    { 5, "Donetsk" },
                    { 6, "Lviv" },
                    { 7, "Zaporizhzhia" },
                    { 8, "Kryvyi Rih" },
                    { 9, "Mykolaiv" },
                    { 10, "Mariupol" },
                    { 11, "Luhansk" },
                    { 12, "Vinnytsia" },
                    { 13, "Sevastopol" },
                    { 14, "Simferopol" },
                    { 15, "Kherson" },
                    { 16, "Poltava" },
                    { 17, "Chernihiv" },
                    { 18, "Cherkasy" },
                    { 19, "Zhytomyr" },
                    { 20, "Sumy" },
                    { 21, "Khmelnytskyi" },
                    { 22, "Chernivtsi" },
                    { 23, "Rivne" },
                    { 24, "Ivano-Frankivsk" },
                    { 25, "Kropyvnytskyi" },
                    { 26, "Kamianske" },
                    { 27, "Lutsk" },
                    { 28, "Kremenchuk" },
                    { 29, "Bila Tserkva" },
                    { 30, "Melitopol" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberRegions_RegionId",
                table: "SubscriberRegions",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriberRegions");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}
