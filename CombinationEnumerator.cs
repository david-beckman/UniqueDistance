using System;
using System.Collections;
using System.Collections.Generic;

namespace UniqueDistance
{
    internal class CombinationEnumerator : IEnumerator<int[]>
    {
        private int[]? values;
        private bool done;

        public CombinationEnumerator(int size, int max, int min = 0)
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

        public int[] Current
        {
            get
            {
                if (this.values == null)
                {
                    throw new InvalidOperationException(this.done ? "Enumeration already finished." : "Enumeration has not started. Call MoveNext.");
                }

                var copy = new int[this.Size];
                Array.Copy(this.values, copy, this.Size);
                return copy;
            }
        }

        object IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            if (this.done)
            {
                return false;
            }

            if (this.values == null)
            {
                this.values = new int[this.Size];

                for (int i = this.Min; i < this.values.Length; i++)
                {
                    this.values[i] = this.Min + i;
                }

                return true;
            }

            values[values.Length - 1]++;
            for (int reverseOffset = 0; reverseOffset < (values.Length - 1) && values[values.Length - 1 - reverseOffset] >= (this.Max - reverseOffset); reverseOffset++)
            {
                var index = values.Length - 1 - reverseOffset;
                values[index - 1]++;
                for (int offset = 0; (index + offset) < values.Length; offset++)
                {
                    values[index + offset] = values[index + offset - 1] + 1;
                }
            }

            if (values[values.Length - 1] >= this.Max)
            {
                this.done = true;
                this.values = null;
            }

            return !this.done;
        }

        public void Reset()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            this.values = null;
            this.done = false;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            this.disposed = true;

            this.done = true;
            this.values = null;
        }

        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
    }
}
