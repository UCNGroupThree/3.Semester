using System;
using System.Collections.Generic;
using System.ServiceModel;
using Common.Exceptions;
//using System.Runtime.InteropServices;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    public class PlaneCtr {

        #region create plane

        // TODO: take account for rows and columns for seat count
        public Plane CreatePlane(string PlaneName, int SeatCount) {
            Plane plane = null;

            // TODO: check for if plane information is correct
            
            // generate plane seats
            List<Seat> PlaneSeats = GeneratePlaneSeats(SeatCount);
            plane = new Plane() { Name = PlaneName, Seats = PlaneSeats };

            try {
                using (var client = new PlaneServiceClient()) {

                    plane.ID = client.AddPlane(plane);
                }
             } 
             catch (FaultException<DatabaseInsertFault> dbException) {
                    throw new Exception(dbException.Message);
             } 
             catch (Exception e) {
                    throw new ConnectionException("WCF Service Exception", e);
             }

            return plane;
        }

        #endregion

        #region generate seats

        private List<Seat> GeneratePlaneSeats(int Seats) {
                 List<Seat> PlaneSeats = new List<Seat>();

            // premade column count of 10
            // TODO: better seat generator
            int rows = Seats/10;
            int columns = 10;

            for (int i = 0; i >= rows; i++) {
                for (int j = 0; j >= columns; j++) {
                    
                    Seat NewPlaneSeat = new Seat();

                    NewPlaneSeat.PosX = i;
                    NewPlaneSeat.PosY = j;

                    PlaneSeats.Add(NewPlaneSeat);
                }
            }

            return PlaneSeats;
        }

        #endregion

        #region update plane

        public Plane UpdatePlane(Plane plane, string name) {

            Plane updatedPlane = null;

            using (var client = new PlaneServiceClient()) {

                try {
                    plane.Name = name;

                    updatedPlane = client.UpdatePlane(plane);
                }
                catch (FaultException<OptimisticConcurrencyFault> concurrencyException) {
                    throw new Exception(concurrencyException.Message);
                }
                catch (FaultException<DatabaseUpdateFault> updateException) {
                    throw new Exception(updateException.Message);
                }
                catch (Exception e){
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }

            return updatedPlane;
        }

        #endregion

        #region delete plane

        public void DeletePlane(Plane plane) {
            using (var client = new PlaneServiceClient()) {
               client.DeletePlane(plane);
            }
        }

        #endregion
        
        #region get methods

        // name
        public List<Plane> GetPlaneByName(string name)
        {
            List<Plane> foundPlanes = null;

            try
            {
                using (PlaneServiceClient client = new PlaneServiceClient()) {
                    foundPlanes = client.GetPlanesByName(name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in finding plane with name: {0}. \nError message: {1}", name, e.Message);
            }

            return foundPlanes;
        }

        // id
        public Plane GetPlaneByID(int id) {
            Plane foundPlane = null;

            try {
                using (PlaneServiceClient client = new PlaneServiceClient()) {
                    //foundPlane = client.GetPlane(id);
                }
            } catch (Exception e) {
                Console.WriteLine("Error in finding plane: " + e.Message);
            }

            return foundPlane;
        }

        // Get planes with seat number equal to input parameter
        public List<Plane> GetPlanesWithSeatNumber(int seats) {

            List<Plane> foundPlanes = null;

            try
            {
                using (PlaneServiceClient client = new PlaneServiceClient()) {
                    foundPlanes = client.GetPlanesWithSeatNumber(seats);
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error in finding planes with seats number equal to: " + seats + ". Error: " + e.Message);
            }

            return foundPlanes;
        }

        // Get planes with seat number less or equal to input parameter
        public List<Plane> GetPlanesWithLessOrEqualSeatNumber(int seats)
        {

            List<Plane> foundPlanes = null;

            try
            {
                using (PlaneServiceClient client = new PlaneServiceClient()) {
                    foundPlanes = client.GetPlanesWithLessOrEqualSeatNumber(seats);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in finding planes with seats number less or equal to: " + seats + ". Error: " + e.Message);
            }

            return foundPlanes;
        }

        // Get planes with seat number more or equal to input parameter
        public List<Plane> blah1(int seats)
        {
            List<Plane> foundPlanes = null;

            try
            {
                using (PlaneServiceClient client = new PlaneServiceClient()) {
                    foundPlanes = client.GetPlanesWithMoreOrEqualSeatNumber(seats);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in finding planes with seats number with more or equal to: " + seats + ". Error: " + e.Message);
            }

            return foundPlanes;
        } 

        // all planes
        public List<Plane> GetAllPlanes()
        { 
            List<Plane> planes = new List<Plane>();

            try
            {
                using (PlaneServiceClient client = new PlaneServiceClient())
                {

                    planes = client.GetAllPlanes();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                planes = null;
            }

            return planes;
        }
        #endregion

    }
}