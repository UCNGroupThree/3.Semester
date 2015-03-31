namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AirportLongLatiAltiFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Airports", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Airports", "Longtitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Airports", "Altitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Airports", "Altitude", c => c.Int(nullable: false));
            AlterColumn("dbo.Airports", "Longtitude", c => c.Decimal(nullable: false, precision: 10, scale: 6));
            AlterColumn("dbo.Airports", "Latitude", c => c.Decimal(nullable: false, precision: 10, scale: 6));
        }
    }
}
