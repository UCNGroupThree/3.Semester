﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json;

namespace Common {
    public static class Helper {
        public static string ToFineString(this TimeSpan span) {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source) {
            return new HashSet<T>(source);
        }

        public static void ReplaceWithHash<T>(this HashSet<T> source, T newObj) {
            source.Remove(newObj);
            source.Add(newObj);
        }

        public static void Replace<T>(this HashSet<T> source, T newObj, T oldObj) {
            source.Remove(oldObj);
            source.Add(newObj);
        }

        public static T Clone<T>(this T source) {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null)) {
                return default(T);
            }

            T ret = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));

            return ret;
        }

        public static void DebugGetLine(this Exception ex) {
#if DEBUG
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Trace.WriteLine("\n\n\n ##### DEBUG EXCEPTION ##### \n");
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            Trace.WriteLine(String.Format("Exception: {0}", ex));

            var stackFrames = st.GetFrames();
            if (stackFrames != null)
                foreach (var stackFrame in stackFrames) {
                    Trace.WriteLine(String.Format("File: {0} Line: {1} Exception Cought", stackFrame.GetFileName(), stackFrame.GetFileLineNumber()));
                }
            Trace.WriteLine("\n ########################### \n\n");
            //Debug.WriteLine(String.Format("File: {0} Line: {1} Exception Cought", frame.GetFileName(),frame.GetFileLineNumber()));
#endif
        }
    }
}