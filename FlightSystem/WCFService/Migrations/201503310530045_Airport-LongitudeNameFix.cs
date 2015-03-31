namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AirportLongitudeNameFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Airports", "Longitude", c => c.Double(nullable: false));
            DropColumn("dbo.Airports", "Longtitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Airports", "Longtitude", c => c.Double(nullable: false));
            DropColumn("dbo.Airports", "Longitude");
        }
    }
}
