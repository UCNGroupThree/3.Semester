namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AirportInit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Airports", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Airports", "ShortName", c => c.String(maxLength: 3));
            AlterColumn("dbo.Airports", "City", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Airports", "Country", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Airports", "Latitude", c => c.Decimal(nullable: false, precision: 10, scale: 6));
            AlterColumn("dbo.Airports", "Longtitude", c => c.Decimal(nullable: false, precision: 10, scale: 6));
            AlterColumn("dbo.Airports", "Altitude", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Airports", "Altitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Airports", "Longtitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Airports", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Airports", "Country", c => c.String());
            AlterColumn("dbo.Airports", "City", c => c.String());
            AlterColumn("dbo.Airports", "ShortName", c => c.String());
            AlterColumn("dbo.Airports", "Name", c => c.String());
        }
    }
}
