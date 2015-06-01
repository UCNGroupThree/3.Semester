using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirportConverter {
    //http://openflights.org/data.html use .dat file from this site to convert to SQL script
    class Controller {

        public static string GetOutput(string head, string input) {
            string ret = "";
            string[] heads = head.Split(',');
            string columsName = "";
            foreach (var s in heads.Where(s => !s.Equals("IGNORE")))
            {
                if (!columsName.Equals("")) {
                    columsName += ",";
                }
                columsName += s;
            }
            string[] lines = input.Split('\n');
            string sqlStart = "INSERT INTO Airports (" + columsName + ") VALUES ";

            foreach (var line in lines.Where(s => s.Contains("Denmark"))) {
                string[] word = SplitCSV(line).ToArray();
                string sql = "\n(";
                for (int i = 0; i < word.Length; i++) {
                    if (!heads[i].Equals("IGNORE")) {
                        sql += word[i].Replace("\"","'") + "," ;
                    }
                }
                sql = sql.Substring(0,sql.Length-1);
                
                sql += "),";
                
                ret += sql;
            }
            if (ret.Length > 0) {
                ret = sqlStart + ret.Substring(0, ret.Length - 1);
            }

            return ret;
        }

        public static IEnumerable<string> SplitCSV(string input) {
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

            foreach (Match match in csvSplit.Matches(input)) {
                yield return match.Value.TrimStart(',');
            }
        }
        
    }
}
