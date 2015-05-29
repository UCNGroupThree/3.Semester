using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.Model;

namespace WCFService.Dijkstra.Test {
    public class DotGenerator {

        // http://graphs.grevian.org/graph

        public static void GenerateDots(List<Airport> aps, List<Path> paths) {
            List<string> strings = new List<string>();
            strings.Add("graph {");
            strings.Add("rankdir=LR;");
            foreach (var airport in aps) {
                var pathss = paths.Where(p => p.From.Equals(airport)).ToList();
                if (pathss.Count > 1) {
                    string t = String.Format("{0} -> {{ ", airport.Name.Replace(" ", ""));
                    foreach (var path in pathss) {
                        t += String.Format("{0} ", path.To.Name.Replace(" ", ""));
                    }

                    t += "};";
                    strings.Add(t);
                } else if (pathss.Count == 1) {
                    string t = String.Format("{0} -> {1};", airport.Name.Replace(" ", ""), pathss[0].To.Name.Replace(" ", ""));
                    strings.Add(t);
                }
                
            }
            strings.Add("}");
            Debug.WriteLine("Dot Generated");
            foreach (var s in strings) {
                Debug.WriteLine(s);
            }
            Debug.WriteLine("Dot Generated");
        }

    }
}
