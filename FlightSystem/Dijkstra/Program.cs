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
                try {
                    Console.Write("ID1: ");
                    int id1 = int.Parse(Console.ReadLine());
                    Console.Write("ID2: ");
                    int id2 = int.Parse(Console.ReadLine());

                    Console.WriteLine("---------------------------");

                    PrintStuff(id1, id2);

                    Console.ReadKey();
                }catch(Exception){}

            }
        }


        static void PrintStuff(int id1, int id2) {
            decimal dm = 0;
            var watch = Stopwatch.StartNew();
            Airport a1;
            Airport a2;
            using (AirportServiceClient client = new AirportServiceClient()) {
                a1 = client.GetAirport(id1);
                a2 = client.GetAirport(id2);
            }
            
            List<Route> aps;

            using (DijkstraClient client = new DijkstraClient()) {
                aps = client.DijkstraStuff(a1, a2);
            }

            if (aps != null && aps.Count > 0) {
                foreach (var route in aps) {
                    Console.WriteLine(route.From.ID + ":" + route.From.Name + " -> " + route.To.Name + ":" + route.To.ID + " - Price: " + route.Price);
                    dm += route.Price;
                }

                Console.WriteLine("Total Price: " + dm);
            } else {
                Console.WriteLine("Empty Result");
            }

            watch.Stop();
            Console.WriteLine("\nTime: " + watch.ElapsedMilliseconds + "ms\n");
        }

        static void PrintStuff2(int id1, int id2) {
            decimal dm = 0;
            Airport a1;
            Airport a2;
            using (AirportServiceClient client = new AirportServiceClient()) {
                a1 = client.GetAirport(id1);
                a2 = client.GetAirport(id2);
            }

            List<Route> aps;

            using (DijkstraClient client = new DijkstraClient()) {
                aps = client.DijkstraStuff(a1, a2);
            }

            if (aps != null && aps.Count > 0) {
                foreach (var route in aps) {
                    Console.WriteLine(route.From.ID + ":" + route.From.Name + " -> " + route.To.Name + ":" + route.To.ID + " - Price: " + route.Price);
                    dm += route.Price;
                }

                Console.WriteLine("Total Price: " + dm);
            } else {
                Console.WriteLine("Empty Result");
            }
        }
    }
}
