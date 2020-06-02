using System;
using System.Collections;
using System.Collections.Generic;

namespace UniqueDistance
{
    internal class CombinationEnumerable : IEnumerable<int[]>
    {
        public CombinationEnumerable(int size, int max, int min = 0)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, "Array dimensions exceeded supported range.");
            }
            var range = max - min;
            if (range < size)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, "Max must be must be large enough such that the range is larger than the size.");
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
            return GetEnumerator();
        }
    }
}
