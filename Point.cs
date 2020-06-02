using System;

namespace UniqueDistance
{
    internal struct Point : IEquatable<Point>, IComparable<Point>
    {
        public Point(int x, int y)
        {
            if (x < 0) throw new ArgumentOutOfRangeException(nameof(x), x, "Value must be a non-negative integer.");
            if (y < 0) throw new ArgumentOutOfRangeException(nameof(y), y, "Value must be a non-negative integer.");

            this.X = x;
            this.Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public int ToInt(int size)
        {
            return this.Y * size + this.X;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }

        public int CompareTo(Point other)
        {
            var value = this.Y.CompareTo(other.Y);
            if (value != 0) return value;
            return this.X.CompareTo(other.X);
        }

        public bool Equals(Point other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Point node && this.Equals(node);
        }

        public double Distance(Point other)
        {
            long xDistance = Math.Abs(this.X - other.X);
            long yDistance = Math.Abs(this.Y - other.Y);
            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }

        internal Point Translate(Translation translation, int size)
        {
            int max = size - 1;
            switch (translation)
            {
                case Translation.None:
                    return this;

                case Translation.ReflectDiagonallyDownward:
                    return new Point(this.Y, this.X);

                case Translation.ReflectDiagonallyUpward:
                    return new Point(max - this.Y, max - this.X);

                case Translation.ReflectHorizontally:
                    return new Point(max - this.X, this.Y);

                case Translation.ReflectThroughCenter:
                case Translation.Rotate180DegreesClockwise:
                    return new Point(max - this.X, max - this.Y);

                case Translation.ReflectVertically:
                    return new Point(this.X, max - this.Y);

                case Translation.Rotate90DegreesClockwise:
                    return new Point(this.Y, max - this.X);

                case Translation.Rotate270DegreesClockwise:
                    return new Point(max - this.Y, this.X);

                default:
                    throw new NotSupportedException($"Unknown translation: {translation}.");
            }
        }
    }
}
