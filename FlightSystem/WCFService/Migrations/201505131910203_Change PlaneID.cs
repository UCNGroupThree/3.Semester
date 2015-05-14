namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlaneID : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Flights", new[] { "PlaneId" });
            CreateIndex("dbo.Flights", "PlaneID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Flights", new[] { "PlaneID" });
            CreateIndex("dbo.Flights", "PlaneId");
        }
    }
}
