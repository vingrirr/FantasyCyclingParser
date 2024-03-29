﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FantasyCyclingParser
{
    public static class Extensions
    {
        private static readonly ThreadLocal<Random> RandomThreadLocal =
            new ThreadLocal<Random>(() => new Random());

        public static void Shuffle<T>(this IList<T> list, int seed = -1)
        {

            var r = seed >= 0 ? new Random(seed) : RandomThreadLocal.Value;
            var len = list.Count;
            for (var i = len - 1; i >= 1; --i)
            {
                var j = r.Next(i);
                var tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

    }
}
