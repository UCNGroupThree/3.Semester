namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", "Plane_ID", "dbo.Planes");
            DropIndex("dbo.Flights", new[] { "Plane_ID" });
            DropIndex("dbo.Flights", new[] { "Route_ID" });
            DropIndex("dbo.Seats", new[] { "Plane_ID" });
            AlterColumn("dbo.Flights", "Plane_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Flights", "Route_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Seats", "Plane_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Flights", "Plane_ID");
            CreateIndex("dbo.Flights", "Route_ID");
            CreateIndex("dbo.Seats", "Plane_ID");
            AddForeignKey("dbo.Seats", "Plane_ID", "dbo.Planes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seats", "Plane_ID", "dbo.Planes");
            DropIndex("dbo.Seats", new[] { "Plane_ID" });
            DropIndex("dbo.Flights", new[] { "Route_ID" });
            DropIndex("dbo.Flights", new[] { "Plane_ID" });
            AlterColumn("dbo.Seats", "Plane_ID", c => c.Int());
            AlterColumn("dbo.Flights", "Route_ID", c => c.Int());
            AlterColumn("dbo.Flights", "Plane_ID", c => c.Int());
            CreateIndex("dbo.Seats", "Plane_ID");
            CreateIndex("dbo.Flights", "Route_ID");
            CreateIndex("dbo.Flights", "Plane_ID");
            AddForeignKey("dbo.Seats", "Plane_ID", "dbo.Planes", "ID");
        }
    }
}
