using System.Collections.Generic;
using System.Linq;

namespace MapLocationShared.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IList<T> lst) =>
            lst == null || lst.Count == 0;

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> lst) =>
           lst == null || lst.Count() == 0;
    }
}
