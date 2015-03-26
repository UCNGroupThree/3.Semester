namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AirportRouteFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "Airport_ID", "dbo.Airports");
            DropForeignKey("dbo.Routes", "From_ID", "dbo.Airports");
            DropIndex("dbo.Routes", new[] { "From_ID" });
            DropIndex("dbo.Routes", new[] { "Airport_ID" });
            DropColumn("dbo.Routes", "From_ID");
            RenameColumn(table: "dbo.Routes", name: "Airport_ID", newName: "From_ID");
            AlterColumn("dbo.Routes", "From_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Routes", "From_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Routes", "From_ID");
            AddForeignKey("dbo.Routes", "From_ID", "dbo.Airports", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "From_ID", "dbo.Airports");
            DropIndex("dbo.Routes", new[] { "From_ID" });
            AlterColumn("dbo.Routes", "From_ID", c => c.Int());
            AlterColumn("dbo.Routes", "From_ID", c => c.Int());
            RenameColumn(table: "dbo.Routes", name: "From_ID", newName: "Airport_ID");
            AddColumn("dbo.Routes", "From_ID", c => c.Int());
            CreateIndex("dbo.Routes", "Airport_ID");
            CreateIndex("dbo.Routes", "From_ID");
            AddForeignKey("dbo.Routes", "From_ID", "dbo.Airports", "ID");
            AddForeignKey("dbo.Routes", "Airport_ID", "dbo.Airports", "ID");
        }
    }
}
