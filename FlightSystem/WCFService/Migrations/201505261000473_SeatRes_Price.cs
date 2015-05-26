namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeatRes_Price : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeatReservations", "Price", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Routes", "Price", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Routes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.SeatReservations", "Price");
        }
    }
}
