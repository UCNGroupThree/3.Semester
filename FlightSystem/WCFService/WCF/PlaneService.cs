using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.ServiceModel;
using Common.Exceptions;
using WCFService.Model;
using WCFService.WCF.Faults;
using WCFService.WCF.Interface;

namespace WCFService.WCF
{
    public class PlaneService : IPlaneService {

        private readonly FlightDB db = new FlightDB();

        #region add plane
        public int AddPlane(Plane plane) {   

            if (plane == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }
            if (plane.Seats != null && !(plane.Seats.Count > 0)) {
                throw new FaultException<NotEnouthFault>(new NotEnouthFault(new NotEnouthException("Invalid Seats")), new FaultReason(new FaultReasonText("Invalid Seats")));
            }

            try {
                db.Planes.Add(plane);
                db.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message); //TODO DEBUG MODE?
                throw new FaultException("The database was unable to insert the record");
            }

            return plane.ID;
        }
        #endregion

        #region update plane
        public Plane UpdatePlane(Plane plane) {
            if (plane == null) {
                throw new FaultException("Nullpointer Exception"); //TODO vores egen Nullpointer Exception?
            }

            try {
                db.Planes.Attach(plane);
                db.Entry(plane).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                throw new FaultException("Concurrency exception?!"); //TODO Concurrency Exception
            }
            catch (UpdateException)
            {
                throw new FaultException("The database was unable to update the record");
            }

            return plane;
        }
        #endregion

        #region delete plane
        public void DeletePlane(Plane plane) {

            db.Planes.Attach(plane);
            //db.Entry(plane).State == EntityState.Deleted; 
            db.SaveChanges();
        }    

        #endregion

        #region get methods

        public List<Plane> GetPlanesByName(string name) {
            return MakePlanes(db.Planes.Include(p => p.Seats).Where(p => p.Name.Contains(name)).ToList());;
        }
      
        // id
        public Plane GetPlaneByID(int id) {
            return db.Planes.Include(p => p.Seats).SingleOrDefault(plane => plane.ID.Equals(id));
        }

        // find planes with a seat number equal to input parameter
        public List<Plane> GetPlanesWithSeatNumber(int seats) {
            return MakePlanes(db.Planes.Include(p => p.Seats).Where(plane => plane.Seats.Count == seats).ToList());;
        }

        // find planes with a seat number with less or equal to input parameter
        public List<Plane> GetPlanesWithLessOrEqualSeatNumber(int seats) {
            return MakePlanes(db.Planes.Include(p => p.Seats).Where(plane => plane.Seats.Count <= seats).ToList());
        }

        // find planes with a seat number with more or equal to input parameter
        public List<Plane> GetPlanesWithMoreOrEqualSeatNumber(int seats) {
            return MakePlanes(db.Planes.Include(p => p.Seats).Where(plane => plane.Seats.Count >= seats).ToList());
        }
     
        // get all planes
        public List<Plane> GetAllPlanes() {
            return MakePlanes(db.Planes.Include(p => p.Seats).ToList());
        }

        public List<Plane> MakePlanes(List<Plane> planes) {

            foreach (var plane in planes) {
                plane.SeatCount = plane.Seats.Count;
                plane.Seats = null;
            }

            return planes;
        } 

        #endregion
    }
}
