namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeatReservationIndexFix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SeatReservations", new[] { "Flight_ID" });
            DropIndex("dbo.SeatReservations", new[] { "Seat_ID" });
            CreateIndex("dbo.SeatReservations", new[] { "Flight_ID", "Seat_ID" }, unique: true, name: "Index_FlightSeat");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SeatReservations", "Index_FlightSeat");
            CreateIndex("dbo.SeatReservations", "Seat_ID");
            CreateIndex("dbo.SeatReservations", "Flight_ID");
        }
    }
}
