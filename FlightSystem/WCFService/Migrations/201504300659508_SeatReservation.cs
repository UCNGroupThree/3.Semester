namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeatReservation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SeatReservations", new[] { "Flight_ID" });
            DropIndex("dbo.SeatReservations", new[] { "Seat_ID" });
            AlterColumn("dbo.SeatReservations", "Flight_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.SeatReservations", "Seat_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.SeatReservations", "Flight_ID");
            CreateIndex("dbo.SeatReservations", "Seat_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SeatReservations", new[] { "Seat_ID" });
            DropIndex("dbo.SeatReservations", new[] { "Flight_ID" });
            AlterColumn("dbo.SeatReservations", "Seat_ID", c => c.Int());
            AlterColumn("dbo.SeatReservations", "Flight_ID", c => c.Int());
            CreateIndex("dbo.SeatReservations", "Seat_ID");
            CreateIndex("dbo.SeatReservations", "Flight_ID");
        }
    }
}
