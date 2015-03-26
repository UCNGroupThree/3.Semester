using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Model {
    public class FlightDB : DbContext {

        public FlightDB() : base(@"Server=kraka.ucn.dk;Database=dmaa0214_3Sem_3;User Id=dmaa0214_3Sem_3;Password=IsAllowed;") {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            //Fix ForeignKey Between Route And Airport;
            modelBuilder.Entity<Route>().HasRequired(r => r.From).WithMany(a => a.Routes);
            //Make Decimals right for coordinates
            modelBuilder.Entity<Airport>().Property(a => a.Longtitude).HasPrecision(10, 6);
            modelBuilder.Entity<Airport>().Property(a => a.Latitude).HasPrecision(10, 6);
        }

        public virtual DbSet<SeatReservation> SeatReservations { get; set; }

        public virtual DbSet<Seat> Seats { get; set; }

        public virtual DbSet<Route> Routes { get; set; }
        
        public virtual DbSet<Postal> Postals { get; set; }

        public virtual DbSet<Plane> Planes { get; set; }

        public virtual DbSet<Airport> Airports { get; set; }

        public virtual DbSet<Flight> Flights { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }
        
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Administrator> Administrators { get; set; }

    }
}
