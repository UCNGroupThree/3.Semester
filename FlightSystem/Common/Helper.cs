using System;
using System.Collections.Generic;
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

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
    }
}