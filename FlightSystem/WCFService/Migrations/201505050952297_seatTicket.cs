namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seatTicket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeatReservations", "Ticket_ID", "dbo.Tickets");
            DropIndex("dbo.SeatReservations", new[] { "Ticket_ID" });
            AlterColumn("dbo.SeatReservations", "Ticket_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "OrderState", c => c.Int(nullable: false));
            CreateIndex("dbo.SeatReservations", "Ticket_ID");
            AddForeignKey("dbo.SeatReservations", "Ticket_ID", "dbo.Tickets", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeatReservations", "Ticket_ID", "dbo.Tickets");
            DropIndex("dbo.SeatReservations", new[] { "Ticket_ID" });
            AlterColumn("dbo.Tickets", "OrderState", c => c.String());
            AlterColumn("dbo.SeatReservations", "Ticket_ID", c => c.Int());
            CreateIndex("dbo.SeatReservations", "Ticket_ID");
            AddForeignKey("dbo.SeatReservations", "Ticket_ID", "dbo.Tickets", "ID");
        }
    }
}
