using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
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

        public List<SeatReservation> MakeSeatsOccupiedRandom(List<Flight> flights2, int seats) {
            //TODO sikker at den fjerner booking, hvis den allerede er kørt en gang
            //db.Database.Log = m => Debug.WriteLine(m);
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            seats = 1;
            //flights = new Dijkstra().DijkstraStuff(1, 3, seats, DateTime.Now);
            //List<Seat> seatList = new List<Seat>();
            IQueryable<Flight> flights1 = db.Flights.Where(x => x.ID == 228 || x.ID == 229)
                .Include(f => f.Plane.Seats);
                //.Include(f=> f.SeatReservations);//.ToList();
            //.Include(f => f.Plane).Include(f => f.SeatReservations.Select(s=>s.Seat)).ToList();
            //flights = db.Flights.Where(x => x.ID == 228 || x.ID == 229).ToList();
            //Debug.WriteLine("flights: " + flights);
            //Debug.WriteLine("queryableFlights: " + queryableFlights.ToList().Count);
            foreach (var f in flights1) {
                //Debug.WriteLine("First: " + f.ID);
                /*
                IEnumerable<Seat> takenSeats = f.SeatReservations.Select(x=>x.Seat);
                IEnumerable<Seat> freeSeats = f.Plane.Seats.Except(takenSeats);

                IEnumerable<Seat> seatsToRes = freeSeats.OrderBy(s => Guid.NewGuid()).Take(seats);
                */
                IEnumerable<Seat> takenSeats = f.SeatReservations.Select(x => x.Seat);

                //TODO smid exception ved for få sæder
                IEnumerable<Seat> freeSeats = f.Plane.Seats.Except(takenSeats);

                IEnumerable<Seat> seatsToRes = freeSeats.OrderBy(s => Guid.NewGuid()).Take(seats);
                //Debug.WriteLine("inside flight foreach");
                
                foreach (var s in seatsToRes) {
                    //Debug.WriteLine("inside seat loop");
                    SeatReservation seatRes = new SeatReservation { Flight = f, Seat = s, State = SeatState.Occupied };
                    Debug.WriteLine("seatRes: flight: {0} Seat: {1} State: {2}", seatRes.Flight.ID,seatRes.Seat.ID,seatRes.State);
                    seatReservations.Add(seatRes);
                }
                
            }

            //List<Seat> seatList = query.ToList();
            //seatList.ForEach(s => Debug.WriteLine(s.ID));

            return null;
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