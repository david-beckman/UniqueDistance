//-----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="N/A">
//     Copyright © 2020 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace UniqueDistance
{
    using System.Collections.Generic;

    internal static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return YieldBatchElements(enumerator, batchSize - 1);
            }
        }

        private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
        {
            yield return source.Current;
            for (int i = 0; i < batchSize && source.MoveNext(); i++)
            {
                yield return source.Current;
            }
        }
    }
}
