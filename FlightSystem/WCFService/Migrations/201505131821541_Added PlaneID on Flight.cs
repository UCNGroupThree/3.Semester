namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPlaneIDonFlight : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Flights", name: "Plane_ID", newName: "PlaneId");
            RenameIndex(table: "dbo.Flights", name: "IX_Plane_ID", newName: "IX_PlaneId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Flights", name: "IX_PlaneId", newName: "IX_Plane_ID");
            RenameColumn(table: "dbo.Flights", name: "PlaneId", newName: "Plane_ID");
        }
    }
}
