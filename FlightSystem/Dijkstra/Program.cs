using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dijkstra.MainService;

namespace Dijkstra {
    class Program {
        static void Main(string[] args) {

            while (true) {
                //try {
                    Console.Write("ID1: ");
                    int id1 = int.Parse(Console.ReadLine());
                    Console.Write("ID2: ");
                    int id2 = int.Parse(Console.ReadLine());

                    Console.WriteLine("---------------------------");
                    DateTime dateTime = DateTime.Now.AddHours(-10);

                    PrintStuff(id1, id2, 1, dateTime);

                    Console.ReadKey();
                //}catch(Exception){}

            }
        }


        static void PrintStuff(int id1, int id2, int seats, DateTime startTime) {
            decimal dm = 0;
            var watch = Stopwatch.StartNew();
            //Airport a1;
            //Airport a2;
            //using (AirportServiceClient client = new AirportServiceClient()) {
            //    a1 = client.GetAirport(id1);
            //    a2 = client.GetAirport(id2);
            //}

            List<Flight> aps;

            using (DijkstraClient client = new DijkstraClient()) {
                aps = client.DijkstraStuff(id1, id2, seats, startTime);
            }

            if (aps != null && aps.Count > 0) {
                foreach (var flight in aps) {
                    Console.WriteLine(flight.Route.From.ID + ":" + flight.Route.From.Name + " -> " + flight.Route.To.Name + ":" + flight.Route.To.ID + " - Price: " + flight.Route.Price);
                    dm += flight.Route.Price;
                }

                Console.WriteLine("Total Price: " + dm);
            } else {
                Console.WriteLine("Empty Result");
            }

            watch.Stop();
            Console.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
        }
    }
}
