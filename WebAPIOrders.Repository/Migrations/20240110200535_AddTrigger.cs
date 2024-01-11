using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIOrders.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Number",
                table: "Orders",
                column: "Number",
                unique: true);

			migrationBuilder.Sql(@"
                CREATE TRIGGER UpdateTotalAmount
                    ON OrderItems
                    AFTER INSERT, UPDATE, DELETE
                    AS
                    BEGIN
                        UPDATE Orders
                        SET TotalAmount = (
                            SELECT COALESCE(SUM(TotalPrice),0)
                            FROM OrderItems
                            WHERE Orders.Id = OrderItems.OrderId
                        )
                        WHERE Orders.Id IN (
                            SELECT DISTINCT OrderId FROM INSERTED
                            UNION
                            SELECT DISTINCT OrderId FROM DELETED
                        );
                    END;
                    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_Number",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

		    migrationBuilder.Sql(@"DROP TRIGGER UpdateTotalAmount"); 
        }


    }
}
