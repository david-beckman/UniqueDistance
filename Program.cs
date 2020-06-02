using System;
using System.Diagnostics;
using System.Linq;

namespace UniqueDistance
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args == null || args.Length != 1 || !int.TryParse(args[0], out int size) || size <= 0)
            {
                Console.Error.WriteLine("Must pass 1 argument: the board size");
                return 1;
            }

            try
            {
                var stopwatch = Stopwatch.StartNew();
                /*
                 * Warning - PLINQ only supports Int32.MaxValue items. The are (size * size)!/size! items in the
                 * CombinationEnumerable ... by adding the batch, this will limit the number of simultaneous items
                 * to that limit. This will not go into effect until size >= 8.
                 */
                var solutions = new CombinationEnumerable(size, size * size)
                    .Batch(Int32.MaxValue)
                    .SelectMany(batch => batch
                        .AsParallel() // 3%
                        .Select(combination => new Board(combination)) // 47%
                        .Where(board => board.AreNodesAllDifferentDistances) // 45%
                        .Select(board => board.ToCanonicalTranslation().ToString()) // 0.1%
                    )
                    .Distinct()
                    .ToArray();

                Console.WriteLine($"{size} => {solutions.Length} solution(s):");
                foreach (var solution in solutions)
                {
                    Console.WriteLine(solution);
                    Console.WriteLine();
                }

                Console.WriteLine(stopwatch.Elapsed);
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return 2;
            }
        }
    }
}
