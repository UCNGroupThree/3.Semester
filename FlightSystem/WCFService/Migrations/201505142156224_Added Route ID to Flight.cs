namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRouteIDtoFlight : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Flights", name: "Route_ID", newName: "RouteID");
            RenameIndex(table: "dbo.Flights", name: "IX_Route_ID", newName: "IX_RouteID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Flights", name: "IX_RouteID", newName: "IX_Route_ID");
            RenameColumn(table: "dbo.Flights", name: "RouteID", newName: "Route_ID");
        }
    }
}
