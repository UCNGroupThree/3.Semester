namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIDnames : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Routes", new[] { "FromId" });
            DropIndex("dbo.Routes", new[] { "ToId" });
            CreateIndex("dbo.Routes", "FromID");
            CreateIndex("dbo.Routes", "ToID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Routes", new[] { "ToID" });
            DropIndex("dbo.Routes", new[] { "FromID" });
            CreateIndex("dbo.Routes", "ToId");
            CreateIndex("dbo.Routes", "FromId");
        }
    }
}
