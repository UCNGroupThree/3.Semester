using System;
using System.IO;

namespace WCFService {
    internal class Program {
        public int n;
        public int infinity = 999;
        public int[,] arr;
        private static int[] distArray;
        private static int[] prevArray;
        private static bool[] knownArray;
        public int source;

        /// ///////////////////////////////////////////////////////////
//Below are functions used in the program
        public void print(int dist) {
            {
                if (prevArray[dist] != 0) {
                    print(prevArray[dist]);
                }
                Console.Write("--->" + dist);
            }
        }

//////////////////////////////////////////////////////////////
        public void read() {
            using (var reader = new StreamReader("D:\\test3.txt")) {
                string size;
                string line = null;
                string[] pars;
                string last;
                size = reader.ReadLine();
                n = int.Parse(size);
                arr = new int[n + 1, n + 1];

                for (var i = 1; i <= n; i++) {
                    line = reader.ReadLine();
                    for (var j = 1; j <= n; j++) {
                        char[] delimiters = {',', '\n'};
                        pars = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        arr[i, j] = int.Parse(pars[j - 1]);
                    }
                }
                last = reader.ReadLine();
                source = int.Parse(last);
            }
        }

//////////////////////////////
        public void initialize() {
            distArray = new int[n + 1];
            prevArray = new int[n + 1];
            knownArray = new bool[n + 1];
            for (var i = 1; i <= n; i++) {
                knownArray[i] = false;
                prevArray[i] = 0;
                distArray[i] = 999;
                distArray[source] = 0;
            }
        }

/////////////////////////////////
        public void dijkstra() {
            var close1 = 0;
            for (var i = 1; i <= n; i++) {
                var min = 9999;
                for (var jj = 1; jj <= n; jj++) {
                    if (!knownArray[jj] && distArray[jj] != 999) {
                        if (distArray[jj] < min) {
                            min = distArray[jj];
                            close1 = jj;
                        }
                    }
                }
                knownArray[close1] = true;
//////////////////////////////////
                for (var a = 1; a <= n; a++) {
                    if (arr[close1, a] != 999 && !knownArray[a]) {
                        if (distArray[a] > arr[close1, a] + distArray[close1]) {
                            distArray[a] = arr[close1, a] + distArray[close1];
                            prevArray[a] = close1;
                        }
                    }
                }
            }
        }

////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////
        private static void Main(string[] args) {
            int choice;
            char chr;
// char c;
            string st;
            int destin;
            var pg = new Program();
            pg.read();
            pg.initialize();
            pg.dijkstra();
//////////////////////////////
            do {
                Console.Clear();
                Console.WriteLine("------------------------------");
                Console.WriteLine("DIJKSTRA'S ALGORITHM FOR WEIGHTED SHORTEST PATH ");
                Console.WriteLine("The Source Vertex is:" + pg.source);
                Console.WriteLine("-------------------------------");
                Console.WriteLine("1.View Shortest Path From Source To A Specific Vertex");
                Console.WriteLine("2.View Shortest Path From Source To All Vertices");
                Console.WriteLine("-------------------------------");
                Console.Write("Please enter your choice\n");

                choice = int.Parse(Console.ReadLine());
                if (choice == 1) {
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("Enter the destination vertex between 1-8:");
                    st = Console.ReadLine();
                    destin = int.Parse(st);
                    Console.WriteLine("The Shortest Path is: ");
                    Console.WriteLine("-------------------------------------------");
                    pg.print(destin);
                }
///////////////////////// 
                if (choice == 2) {
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("The Shortest Paths from Source to all vertices are:");

                    Console.WriteLine("-------------------------------------------");
                    for (var k = 1; k <= pg.n; k++) {
                        pg.print(k);
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("-------------------------------------------");

                Console.Write("Do you want to continue: Enter Y or N\n");
                chr = char.Parse(Console.ReadLine());
            } while (chr != 'n' && chr != 'N');
        }
    }
}