using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.ReservationService;

namespace Test {
    class ReservationTest {
        // ReSharper disable once InconsistentNaming
        private List<Flight> flight;
        private Ticket ticket;

        public ReservationServiceClient ResClient { get; set; }

        public ReservationTest() {
            ResClient = new ReservationServiceClient();
            GetFlight();
            

            Sleep(10);
            try {
                GetFlight();
            } catch (Exception ex) {
                //var detail = ex as System.ServiceModel.Security.MessageSecurityException;

                Console.WriteLine("Exception");
                Console.WriteLine(ex);
                Console.WriteLine(ex.Message);
                var inner = ex.InnerException as FaultException;
                if (inner != null) {
                    Console.WriteLine("----");
                    Console.WriteLine(inner);
                    Console.WriteLine(inner.Message);
                    var code = inner.Code;
                    Debug.WriteLine("#" + code + "#");
                    Console.WriteLine("#" + code + "#");
                    Console.WriteLine("----");
                }
                Console.WriteLine("Exception end!");
                Debug.WriteLine("");
                Debug.WriteLine(ResClient.State);
                ResClient.Abort();
                Debug.WriteLine(ResClient.State);
                Debug.WriteLine("aborted");
            }
            
            

            //MakeSeatReservation();

            Console.WriteLine();
            Console.WriteLine("Done - terminated");
            Console.ReadLine();
        }

        private void Sleep(int sec) {
            Console.WriteLine();
            Console.WriteLine("Thread sleeping");
            for (int i = 0; i < sec; ++i) {
                Console.Write("\r{0}%   ", i);
                Thread.Sleep(1000);
            }
            
            Console.WriteLine();
            Console.WriteLine("Thread Awakened");
            Console.WriteLine();
        }

        private void MakeSeatReservation() {
            var seatResList = ResClient.MakeSeatsOccupiedRandom();
            Console.WriteLine("### Print SeatRervation ###");
            seatResList.ForEach(PrintSeatRervation);
            Console.WriteLine("### Print End ###");
            Console.WriteLine();
        }

        private void PrintSeatRervation(SeatReservation sr) {
            Console.WriteLine("#{0}, flight: #{1}, seat: #{2}, state: {3}",sr.ID, sr.Flight_ID, sr.Seat_ID, sr.State);
        }

        private void GetFlight() {
            flight = ResClient.GetFlights(1, 3, 1, DateTime.Now);
            Console.WriteLine("### Print Flights ###");

            flight.ForEach(PrintFlight);
            Console.WriteLine("### Print End ###");
            Console.WriteLine();
        }
        
        private void PrintFlight(Flight flight) {
            Console.WriteLine(flight.Route.From.ID + ":" + flight.Route.From.Name + " -> " + flight.Route.To.Name + ":" + flight.Route.To.ID + " - Price: " + flight.Route.Price);
        }
    }
}
