namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AirportTimezone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Airports", "TimeZone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Airports", "TimeZone", c => c.String());
        }
    }
}
