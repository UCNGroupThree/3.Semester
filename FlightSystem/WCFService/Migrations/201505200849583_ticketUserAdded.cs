namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketUserAdded : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tickets", new[] { "User_ID" });
            AlterColumn("dbo.Tickets", "User_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "User_ID");
            DropColumn("dbo.SeatReservations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeatReservations", "Name", c => c.String());
            DropIndex("dbo.Tickets", new[] { "User_ID" });
            AlterColumn("dbo.Tickets", "User_ID", c => c.Int());
            CreateIndex("dbo.Tickets", "User_ID");
        }
    }
}
