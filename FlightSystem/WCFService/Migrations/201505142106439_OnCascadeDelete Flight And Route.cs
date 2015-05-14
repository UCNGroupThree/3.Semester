namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnCascadeDeleteFlightAndRoute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flights", "Route_ID", "dbo.Routes");
            AddForeignKey("dbo.Flights", "Route_ID", "dbo.Routes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "Route_ID", "dbo.Routes");
            AddForeignKey("dbo.Flights", "Route_ID", "dbo.Routes", "ID");
        }
    }
}
