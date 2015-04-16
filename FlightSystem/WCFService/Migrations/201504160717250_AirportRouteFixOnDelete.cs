namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AirportRouteFixOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "From_ID", "dbo.Airports");
            AddForeignKey("dbo.Routes", "From_ID", "dbo.Airports", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "From_ID", "dbo.Airports");
            AddForeignKey("dbo.Routes", "From_ID", "dbo.Airports", "ID", cascadeDelete: true);
        }
    }
}
