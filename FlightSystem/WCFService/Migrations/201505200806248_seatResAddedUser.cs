namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seatResAddedUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeatReservations", "User_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.SeatReservations", "User_ID");
            AddForeignKey("dbo.SeatReservations", "User_ID", "dbo.Users", "ID");
            DropColumn("dbo.SeatReservations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeatReservations", "Name", c => c.String());
            DropForeignKey("dbo.SeatReservations", "User_ID", "dbo.Users");
            DropIndex("dbo.SeatReservations", new[] { "User_ID" });
            DropColumn("dbo.SeatReservations", "User_ID");
        }
    }
}
