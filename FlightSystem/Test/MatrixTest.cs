using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Test.MainService;

namespace Test {
    public class MatrixTest {
        public void Run() {

            while (true) {
               
                Console.Write("ID1: ");
                int id1 = int.Parse(Console.ReadLine());
                Console.Write("ID2: ");
                int id2 = int.Parse(Console.ReadLine());

                Console.WriteLine("---------------------------");

                DateTime dateTime = DateTime.Now.AddHours(-5);

                PrintStuff(1, 3, 1, dateTime);

                Console.ReadKey();
                

            }
        }


        static void PrintStuff(int id1, int id2, int seats, DateTime startTime) {
            decimal dm = 0;
            var watch = Stopwatch.StartNew();

            List<Flight> aps;

            using (DijkstraClient client = new DijkstraClient()) {
                aps = client.GetShortestPath(id1, id2, seats, startTime);
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
