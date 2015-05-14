namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AirportIDsonRoute : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Routes", new[] { "To_ID" });
            RenameColumn(table: "dbo.Routes", name: "From_ID", newName: "FromId");
            RenameColumn(table: "dbo.Routes", name: "To_ID", newName: "ToId");
            RenameIndex(table: "dbo.Routes", name: "IX_From_ID", newName: "IX_FromId");
            AlterColumn("dbo.Routes", "ToId", c => c.Int(nullable: false));
            CreateIndex("dbo.Routes", "ToId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Routes", new[] { "ToId" });
            AlterColumn("dbo.Routes", "ToId", c => c.Int());
            RenameIndex(table: "dbo.Routes", name: "IX_FromId", newName: "IX_From_ID");
            RenameColumn(table: "dbo.Routes", name: "ToId", newName: "To_ID");
            RenameColumn(table: "dbo.Routes", name: "FromId", newName: "From_ID");
            CreateIndex("dbo.Routes", "To_ID");
        }
    }
}
