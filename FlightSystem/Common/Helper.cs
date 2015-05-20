using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Common {
    public static class Helper {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source) {
            return new HashSet<T>(source);
        }

        public static T Clone<T>(this T source) {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null)) {
                return default(T);
            }

            T ret = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));

            return ret;
        }

        public static bool HasProperty(this object obj, string propertyName) {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        public static void DebugGetLine(this Exception ex) {
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            foreach (var stackFrame in st.GetFrames()) {
                Debug.WriteLine(String.Format("File: {0} Line: {1} Exception Cought", stackFrame.GetFileName(), stackFrame.GetFileLineNumber()));
            }
            //Debug.WriteLine(String.Format("File: {0} Line: {1} Exception Cought", frame.GetFileName(),frame.GetFileLineNumber()));
        }
    }
}