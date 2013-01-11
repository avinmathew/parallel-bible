using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleDownload.Utils
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Slices a sequence into a sub-sequences each containing maxItemsPerSlice, except for the last
        /// which will contain any items left over
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Slice<T>(this IEnumerable<T> sequence, int maxItemsPerSlice)
        {
            if (maxItemsPerSlice <= 0)
            {
                throw new ArgumentOutOfRangeException("maxItemsPerSlice", "maxItemsPerSlice must be greater than 0");
            }

            List<T> slice = new List<T>(maxItemsPerSlice);

            foreach (var item in sequence)
            {
                slice.Add(item);

                if (slice.Count == maxItemsPerSlice)
                {
                    yield return slice.ToArray();
                    slice.Clear();
                }
            }

            // return the "crumbs" that
            // didn't make it into a full slice
            if (slice.Count > 0)
            {
                yield return slice.ToArray();
            }
        }
    }
}
