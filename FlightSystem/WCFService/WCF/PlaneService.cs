using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF
{
    class PlaneService : IPlaneService {

        private readonly FlightDB db = new FlightDB(); 

        public int AddPlane(Plane plane){   

            if (plane == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }
            
            try {
                db.Planes.Add(plane);
                db.SaveChanges();
            } 
            catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException("The database was unable to insert the record");
            }

            return plane.ID;
        }

        public Plane UpdatePlane(Plane plane)
        {
            if (plane == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }

            // tjek senere
            //if (db.Seats.Any(plane) )

            try {
                db.Planes.Attach(plane);
                db.Entry(plane).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (OptimisticConcurrencyException exception)
            {
                throw new FaultException("Concurrency exception?!"); //TODO Concurrency Exception
            }
            catch (UpdateException exception)
            {
                throw new FaultException("The database was unable to update the record");
            }

            return plane;
        }

        public void DeletePlane(Plane plane) {

            db.Planes.Remove(plane);
            //db.Entry(plane).State == EntityState.Deleted;
            db.SaveChanges();
        }    

         // get methods
        public Plane GetPlane(int id)
        {
            return db.Planes.SingleOrDefault(plane => plane.ID == id);
        }

        // find planes with a seat number equal to input parameter
        public List<Plane> GetPlanesWithEqualSeatNumber(int seats) {

            return db.Planes.Where(plane => plane.Seats.Count == seats).ToList();
        }

        // find planes with a seat number with less or equal to input parameter
        public List<Plane> GetPlaneswithLessThanOrEqualSeatNumber(int seats) {

            return db.Planes.Where(plane => plane.Seats.Count <= seats).ToList();
        }

        // find planes with a seat number with more or equal to input parameter
        public List<Plane> GetPlaneswithMoreOrEqualSeatNumber(int seats)
        {

            return db.Planes.Where(plane => plane.Seats.Count >= seats).ToList();
        }
       
    }
}
