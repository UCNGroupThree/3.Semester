﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using Common.Exceptions;
using WCFService.Model;
using WCFService.WCF.Interface;

namespace WCFService.WCF {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class BookingService : IBookingService {

        int count = 0;
        private bool finish = false;
        private List<Flight> flights;
        private List<SeatReservation> seatReservations = new List<SeatReservation>();
        private FlightDB db = new FlightDB();

        public BookingService() {
            OperationContext.Current.InstanceContext.Closed += InstanceContext_Closed;
            
            // get the callback instance here if needed
            // OperationContext.Current.GetCallbackChannel<IServiceCallback>()
        }

        private void InstanceContext_Closed(object sender, EventArgs e) {
            // Session closed here
            count = 0;
            Debug.WriteLine("End session: {0}, {1}, {2}", sender, e, finish);
        }


        public int AddOne() {
            return ++count;
        }

        public List<Flight> GetFlights(int fromId, int toId, int seats, DateTime dateTime) {
            try {
                flights = new Dijkstra().DijkstraStuff(fromId, toId, seats, dateTime);
                return flights;
            } catch (Exception) {
                //throw;
            }
            return null;
        }

        public List<SeatReservation> MakeSeatsOccupiedRandom(List<Flight> flights2, int seatsCount) {
            //TODO sikker at den fjerner booking, hvis den allerede er kørt en gang
            //db.Database.Log = m => Debug.WriteLine(m);
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            seatsCount = 5;
            //flights = new Dijkstra().DijkstraStuff(1, 3, seatsCount, DateTime.Now);
            //List<Seat> seatList = new List<Seat>();
            List<SeatReservation> seatResList;
            //TODO While loop
            try {
                seatResList = GetRandomSeatReservations(flights2, seatsCount);
                //db.SeatReservations.AddRange(seatReservations);
                //db.SaveChanges();
            } catch (Exception) {
                
                throw;
            }

            seatReservations = seatResList;

            //List<Seat> seatList = query.ToList();
            //seatList.ForEach(s => Debug.WriteLine(s.ID));

            return seatReservations;
        }

        private List<SeatReservation> GetRandomSeatReservations(List<Flight> flights2, int seatsCount) {
            List<SeatReservation> ret = new List<SeatReservation>();
            IQueryable<Flight> flights1 = db.Flights.Where(x => x.ID == 228 || x.ID == 229)
                .Include(f => f.Plane.Seats)
                .Include(f => f.SeatReservations);//.ToList();
            //.Include(f => f.Plane).Include(f => f.SeatReservations.Select(s=>s.Seat)).ToList();
            //flights = db.Flights.Where(x => x.ID == 228 || x.ID == 229).ToList();
            //Debug.WriteLine("flights: " + flights);
            //Debug.WriteLine("queryableFlights: " + queryableFlights.ToList().Count);
            foreach (var f in flights1) {
                //Debug.WriteLine("First: " + f.ID);
                List<Seat> takenSeats = f.SeatReservations.Select(x => x.Seat).ToList();

                List<Seat> freeSeats = f.Plane.Seats.Except(takenSeats).ToList();
                Debug.WriteLine("Lists?: flight: {0} freeSeats: {1} takenSeats: {2}", f.ID, freeSeats.Count, takenSeats.ToList().Count());
                if (freeSeats.Count() < seatsCount) {
                    throw new NotEnouthException("Not Enough seats free");
                }

                List<Seat> seatsToRes = freeSeats.OrderBy(s => Guid.NewGuid()).Take(seatsCount).ToList();

                //Debug.WriteLine("inside flight foreach");
                //Debug.WriteLine("Seats to Res: {0}, Founded seatsCount: {1}", seatsCount, seatsToRes.Count());
                foreach (var s in seatsToRes) {
                    //Debug.WriteLine("inside seat loop");
                    SeatReservation seatRes = new SeatReservation { Flight = f, Seat = s, State = SeatState.Occupied };
                    Debug.WriteLine("seatRes: flight: {0} Seat: {1} State: {2}", seatRes.Flight.ID, seatRes.Seat.ID, seatRes.State);
                    ret.Add(seatRes);
                }
            }
            return ret;
        }

        public void Complete() {
            Debug.WriteLine("Completed started!");
            OperationContext.Current.InstanceContext.Closed -= InstanceContext_Closed;
            Debug.WriteLine("Completed ended!");
            finish = true;
            //throw new NotImplementedException();
        }
    }
}