using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Model {
    public class FlightDB : DbContext {

        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();

        public FlightDB()
            : base("Server=kraka.ucn.dk;Database=dmaa0214_3Sem_3;User Id=dmaa0214_3Sem_3;Password=IsAllowed;") {
            base.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            //Fix ForeignKey Between Route And Airport;
            modelBuilder.Entity<Route>().HasRequired(r => r.From).WithMany(a => a.Routes);//.WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>().HasRequired(f => f.Route).WithMany(r => r.Flights).WillCascadeOnDelete(true);
            
            //Delete seatReservations if delete ticket object
            modelBuilder.Entity<Ticket>().HasMany(t => t.SeatReservations).WithRequired(s => s.Ticket).WillCascadeOnDelete(true);
           
            //modelBuilder.Entity<SeatReservation>().HasRequired(s => s.Ticket).WithMany(t => t.SeatReservations).WillCascadeOnDelete(true);

            modelBuilder.Entity<Seat>().HasRequired(s => s.Plane).WithMany(s => s.Seats).WillCascadeOnDelete(true);

            //modelBuilder.Entity<SeatReservation>().HasRequired(s => s.Flight).WithMany(s => s.SeatReservations).WillCascadeOnDelete(false);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Entity<Seat>().WithMWillCascadeOnDelete(false);

           // modelBuilder.Entity<SeatReservation>().Property(s => s.Flight).HasColumnAnnotation(IndexAnnotation.AnnotationName,
           //    new IndexAnnotation(new IndexAttribute("IX_FirstNameLastName", 2) { IsUnique = true }));

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


        public void DebugSaveChanges() {
            try {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                SaveChanges();
            } catch (DbEntityValidationException e) {
                foreach (var eve in e.EntityValidationErrors) {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors) {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void DebugDetectChanges() {
            ChangeTracker.DetectChanges();
            var list = ChangeTracker.Entries().ToList();
            Debug.WriteLine("Start of DetectChanges");
            foreach (var v in list) {
                Debug.WriteLine("c: #" + list.IndexOf(v) + " - " + v.Entity + " state: " + v.State);
            }
            Debug.WriteLine("End of DetectChanges");

        }


        public void DebugLog() {
            Database.Log = m => Debug.WriteLine(m);
        }
    }
}
