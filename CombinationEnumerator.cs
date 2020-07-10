//-----------------------------------------------------------------------
// <copyright file="CombinationEnumerator.cs" company="N/A">
//     Copyright © 2020 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace UniqueDistance
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UniqueDistance.Properties;

    internal class CombinationEnumerator : IEnumerator<int[]>
    {
        private readonly int max;
        private readonly int min;
        private readonly int size;

        private bool disposed;
        private bool done;
        private int[]? values;

        public CombinationEnumerator(int size, int max, int min = 0)
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

            this.size = size;
            this.min = min;
            this.max = max;
        }

        public int[] Current
        {
            get
            {
                if (this.values == null)
                {
                    throw new InvalidOperationException(this.done
                        ? Resources.ExceptionMessage_EnumerationFinished
                        : Resources.ExceptionMessage_EnumerationNotStarted);
                }

                var copy = new int[this.size];
                Array.Copy(this.values, copy, this.size);
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
                this.values = new int[this.size];

                for (int i = this.min; i < this.values.Length; i++)
                {
                    this.values[i] = this.min + i;
                }

                return true;
            }

            var maxIndex = this.size - 1;
            this.values[maxIndex]++;
            for (
                var reverseOffset = 0;
                reverseOffset < maxIndex && this.values[maxIndex - reverseOffset] >= (this.max - reverseOffset);
                reverseOffset++)
            {
                var index = maxIndex - reverseOffset;
                this.values[index - 1]++;
                for (int offset = 0; (index + offset) < this.size; offset++)
                {
                    this.values[index + offset] = this.values[index + offset - 1] + 1;
                }
            }

            if (this.values[maxIndex] >= this.max)
            {
                this.done = true;
                this.values = null;
            }

            return !this.done;
        }

        public void Reset()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            this.values = null;
            this.done = false;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;

            this.done = true;
            this.values = null;
        }
    }
}
