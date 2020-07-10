//-----------------------------------------------------------------------
// <copyright file="Board.cs" company="N/A">
//     Copyright © 2020 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace UniqueDistance
{
    using System;
    using System.Linq;
    using System.Text;

    internal struct Board : IEquatable<Board>
    {
        private readonly Point[] tokens;

        internal Board(int[] combination)
        {
            if (combination == null)
            {
                throw new ArgumentNullException(nameof(combination));
            }

            var size = combination.Length;
            var points = new Point[size];

            Array.Sort(combination);
            for (int i = 0; i < size; i++)
            {
                points[i] = new Point(combination[i] % size, combination[i] / size);
            }

            this.tokens = points;

#pragma warning disable S125 // Sections of code should not be commented out
            /* The LINQ version takes ~9x
            this.Tokens = combination?
                .Select(reference => new Point(reference % combination.Length, reference / combination.Length))
                .OrderBy(token => token)
                .ToArray()
                ?? throw new ArgumentNullException(nameof(combination));
            */
#pragma warning restore S125 // Sections of code should not be commented out
        }

        internal Board(Point[] tokens)
        {
            this.tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            Array.Sort(tokens);
        }

        public bool AreNodesAllDifferentDistances
        {
            get
            {
                var size = this.tokens?.Length ?? 0;
                var distances = new double[size * size];
                var distanceIndex = 0;

                for (int i = 0; i < size; i++)
                {
                    for (int j = i + 1; j < size; j++)
                    {
                        var distance = this.tokens?[i].Distance(this.tokens[j]) ?? 0;
#pragma warning disable S1244 // Floating point numbers should not be tested for equality
                        if (distance == 0d || distances.AsSpan(0, distanceIndex).Contains(distance))
#pragma warning restore S1244 // Floating point numbers should not be tested for equality
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
            if (this.tokens == other.tokens)
            {
                return true;
            }

            if (this.tokens == null || other.tokens == null || this.tokens.Length != other.tokens.Length)
            {
                return false;
            }

            for (int i = 0; i < this.tokens.Length; i++)
            {
                if (!this.tokens[i].Equals(other.tokens[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public Board ToCanonicalTranslation()
        {
            return this.GetAllTranslations().First();
        }

        public IOrderedEnumerable<Board> GetAllTranslations()
        {
            var translations = new[]
            {
                Translation.None,
                Translation.ReflectDiagonallyDownward,
                Translation.ReflectDiagonallyUpward,
                Translation.ReflectHorizontally,
                Translation.Rotate180DegreesClockwise,
                Translation.ReflectVertically,
                Translation.Rotate90DegreesClockwise,
                Translation.Rotate270DegreesClockwise,
            };

            Func<Translation, Board> translate = this.Translate;
            var size = this.tokens?.Length ?? 0;

            return translations
                .Select(translation => translate(translation))
                .OrderBy(board => board.tokens?.Select(node => node.ToInt(size)).Sum() ?? 0)
                .ThenBy(board => board.tokens
                    ?.Select(node => node.ToInt(size) + 1.0)
                    .Aggregate(1.0, (acc, val) => acc * val) ?? 0);
        }

        public Board Translate(Translation translation)
        {
            var size = this.tokens?.Length ?? 0;
            return new Board(this.tokens
                ?.Select(token => token.Translate(translation, size))
                .OrderBy(token => token)
                .ToArray() ?? Array.Empty<Point>());
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var size = this.tokens?.Length ?? 0;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    builder.Append(this.tokens.Contains(new Point(x, y)) ? 'X' : '-');
                }

                builder.Append('\n');
            }

            return builder.ToString(0, builder.Length - 1);
        }
    }
}
