namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoutePrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            Sql("Update dbo.Routes set Price = 500");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "Price");
        }
    }
}
