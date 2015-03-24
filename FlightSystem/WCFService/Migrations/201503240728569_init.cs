namespace WCFService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        Concurrency = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longtitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Altitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeZone = c.String(),
                        Concurrency = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Concurrency = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        From_ID = c.Int(),
                        To_ID = c.Int(),
                        Airport_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Airports", t => t.From_ID)
                .ForeignKey("dbo.Airports", t => t.To_ID)
                .ForeignKey("dbo.Airports", t => t.Airport_ID)
                .Index(t => t.From_ID)
                .Index(t => t.To_ID)
                .Index(t => t.Airport_ID);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ArrivalTime = c.DateTime(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        Plane_ID = c.Int(),
                        Route_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Planes", t => t.Plane_ID)
                .ForeignKey("dbo.Routes", t => t.Route_ID)
                .Index(t => t.Plane_ID)
                .Index(t => t.Route_ID);
            
            CreateTable(
                "dbo.Planes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PosX = c.Int(nullable: false),
                        PosY = c.Int(nullable: false),
                        Plane_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Planes", t => t.Plane_ID)
                .Index(t => t.Plane_ID);
            
            CreateTable(
                "dbo.Postals",
                c => new
                    {
                        PostCode = c.Int(nullable: false),
                        City = c.String(),
                        Concurrency = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.PostCode);
            
            CreateTable(
                "dbo.SeatReservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        State = c.Int(nullable: false),
                        Flight_ID = c.Int(),
                        Seat_ID = c.Int(),
                        Ticket_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Flights", t => t.Flight_ID)
                .ForeignKey("dbo.Seats", t => t.Seat_ID)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID)
                .Index(t => t.Flight_ID)
                .Index(t => t.Seat_ID)
                .Index(t => t.Ticket_ID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderState = c.String(),
                        Concurrency = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        Concurrency = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Postal_PostCode = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Postals", t => t.Postal_PostCode)
                .Index(t => t.Postal_PostCode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "Postal_PostCode", "dbo.Postals");
            DropForeignKey("dbo.SeatReservations", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.SeatReservations", "Seat_ID", "dbo.Seats");
            DropForeignKey("dbo.SeatReservations", "Flight_ID", "dbo.Flights");
            DropForeignKey("dbo.Routes", "Airport_ID", "dbo.Airports");
            DropForeignKey("dbo.Routes", "To_ID", "dbo.Airports");
            DropForeignKey("dbo.Routes", "From_ID", "dbo.Airports");
            DropForeignKey("dbo.Flights", "Route_ID", "dbo.Routes");
            DropForeignKey("dbo.Flights", "Plane_ID", "dbo.Planes");
            DropForeignKey("dbo.Seats", "Plane_ID", "dbo.Planes");
            DropIndex("dbo.Users", new[] { "Postal_PostCode" });
            DropIndex("dbo.Tickets", new[] { "User_ID" });
            DropIndex("dbo.SeatReservations", new[] { "Ticket_ID" });
            DropIndex("dbo.SeatReservations", new[] { "Seat_ID" });
            DropIndex("dbo.SeatReservations", new[] { "Flight_ID" });
            DropIndex("dbo.Seats", new[] { "Plane_ID" });
            DropIndex("dbo.Flights", new[] { "Route_ID" });
            DropIndex("dbo.Flights", new[] { "Plane_ID" });
            DropIndex("dbo.Routes", new[] { "Airport_ID" });
            DropIndex("dbo.Routes", new[] { "To_ID" });
            DropIndex("dbo.Routes", new[] { "From_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Tickets");
            DropTable("dbo.SeatReservations");
            DropTable("dbo.Postals");
            DropTable("dbo.Seats");
            DropTable("dbo.Planes");
            DropTable("dbo.Flights");
            DropTable("dbo.Routes");
            DropTable("dbo.Airports");
            DropTable("dbo.Administrators");
        }
    }
}
