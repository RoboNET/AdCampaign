using System;
using System.Collections.Generic;

namespace AdCampaign.Common.Extensions
{
    public static class EnumerableExt
    {
        private static readonly Random Rng = new Random();

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}