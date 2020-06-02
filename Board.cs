using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniqueDistance
{
    internal struct Board : IEquatable<Board>
    {
        private readonly Point[] tokens;

        internal Board(int[] combination)
        {
            if (combination == null)
            {
                throw new ArgumentNullException(nameof(combination));
            }

            var points = new Point[this.Size = combination.Length];

            Array.Sort(combination);
            for (int i = 0; i < Size; i++)
            {
                points[i] = new Point(combination[i] % Size, combination[i] / Size);
            }

            this.tokens = points;

            /* The LINQ version takes ~9x
            this.Tokens = combination?
                .Select(reference => new Point(reference % combination.Length, reference / combination.Length))
                .OrderBy(token => token)
                .ToArray()
                ?? throw new ArgumentNullException(nameof(combination));
            */
        }

        internal Board(Point[] tokens)
        {
            this.tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            this.Size = tokens.Length;
            Array.Sort(tokens);
        }

        public IReadOnlyList<Point> Tokens => tokens;
        public int Size { get; }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.tokens);
        }

        public override bool Equals(object? obj)
        {
            return obj is Board board && this.Equals(board);
        }

        public bool Equals(Board other)
        {
            if (object.ReferenceEquals(this, other)) return true;
            if (this.Size != other.Size) return false;

            for (int i = 0; i < this.Size; i++)
            {
                if (!this.tokens[i].Equals(other.tokens[i])) return false;
            }

            return true;
        }

        public Board ToCanonicalTranslation()
        {
            return GetAllTranslations().First();
        }

        public IOrderedEnumerable<Board> GetAllTranslations()
        {
            var translations = new[] {
                Translation.None,
                Translation.ReflectDiagonallyDownward,
                Translation.ReflectDiagonallyUpward,
                Translation.ReflectHorizontally,
                Translation.Rotate180DegreesClockwise,
                Translation.ReflectVertically,
                Translation.Rotate90DegreesClockwise,
                Translation.Rotate270DegreesClockwise
            };

            Func<Translation, Board> translate = this.Translate;
            var size = this.Size;

            return translations
                .Select(translation => translate(translation))
                // Sum
                .OrderBy(board => board.tokens.Select(node => node.ToInt(size)).Sum())
                // Product
                .ThenBy(board => board.tokens.Select(node => node.ToInt(size) + 1.0).Aggregate(1.0, (acc, val) => acc * val));
        }

        public Board Translate(Translation translation)
        {
            var size = this.Size;
            return new Board(this.tokens
                .Select(token => token.Translate(translation, size))
                .OrderBy(token => token)
                .ToArray());
        }

        public bool AreNodesAllDifferentDistances
        {
            get
            {
                var distances = new double[this.Size * this.Size];
                var distanceIndex = 0;

                for (int i = 0; i < this.Size; i++)
                {
                    for (int j = i + 1; j < this.Size; j++)
                    {
                        var distance = this.tokens[i].Distance(this.tokens[j]);
                        if (distance == 0 || distances.AsSpan(0, distanceIndex).Contains(distance))
                        {
                            return false;
                        }

                        distances[distanceIndex] = distance;
                        distanceIndex++;
                    }
                }

                return true;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int y = 0; y < this.Size; y++)
            {
                for (int x = 0; x < this.Size; x++)
                {
                    builder.Append(this.tokens.Contains(new Point(x, y)) ? 'X' : '-');
                }
                builder.Append('\n');
            }

            return builder.ToString(0, builder.Length - 1);
        }
    }
}
