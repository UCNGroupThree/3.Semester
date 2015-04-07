using System;
using System.Collections.Generic;
using System.ServiceModel;
using FlightAdmin.Exceptions;
//using System.Runtime.InteropServices;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    public class PlaneCtr {

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

        public void DeletePlane(Plane plane) {
            using (var client = new PlaneServiceClient()) {
               client.DeletePlane(plane);
            }
        }

        //TODO: other get methods for plane
    
        public Plane GetPlaneByID(int id) {
            Plane foundPlane = null;

            try {
                using (PlaneServiceClient client = new PlaneServiceClient()) {
                    foundPlane = client.GetPlane(id);
                }
            } catch (Exception e) {
                Console.WriteLine("Error in finding plane: " + e.Message);
            }
            

            return foundPlane;
        }

        public List<Plane> GetAllPlanes() { //TODO Error handeling
            List<Plane> planes = new List<Plane>();

            using (PlaneServiceClient client = new PlaneServiceClient()) {
                planes = client.GetAllPlanes();
            }

            return planes;
        }

    }
}