//-----------------------------------------------------------------------
// <copyright file="CombinationEnumerable.cs" company="N/A">
//     Copyright © 2020 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace UniqueDistance
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UniqueDistance.Properties;

    internal class CombinationEnumerable : IEnumerable<int[]>
    {
        public CombinationEnumerable(int size, int max, int min = 0)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(size), size, Resources.ExceptionMessage_ArrayTooSmallOrTooLarge);
            }

            var range = max - min;
            if (range < size)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, Resources.ExceptionMessage_MaxTooSmall);
            }

            this.Size = size;
            this.Min = min;
            this.Max = max;
        }

        public int Size { get; }

        public int Min { get; }

        public int Max { get; }

        public IEnumerator<int[]> GetEnumerator()
        {
            return new CombinationEnumerator(this.Size, this.Max, this.Min);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
