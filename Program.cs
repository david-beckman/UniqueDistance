//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="N/A">
//     Copyright © 2020 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace UniqueDistance
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    using UniqueDistance.Properties;

    internal static class Program
    {
        private enum ReturnCode
        {
            Success,
            ArgumentException,
            InternalError,
        }

        [SuppressMessage(
            "Microsoft.Design",
            "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "The main entry-points are the exception to the rule.")]
        [SuppressMessage(
            "SonarAnalyzer",
            "S2221:ExceptionShouldNotBeCaughtWhenNotRequiredByCalledMethods",
            Justification = "The main entry-points are the exception to the rule.")]
        private static int Main(string[] args)
        {
            if (args == null || args.Length != 1 || !int.TryParse(args[0], out int size) || size <= 0)
            {
                Console.Error.WriteLine(Resources.ConsoleMessage_MissingSize);
                return (int)ReturnCode.ArgumentException;
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
                    .Batch(int.MaxValue)
                    .SelectMany(batch => batch
                        .AsParallel() // 3%
                        .Select(combination => new Board(combination)) // 47%
                        .Where(board => board.AreNodesAllDifferentDistances) // 45%
                        .Select(board => board.ToCanonicalTranslation().ToString())) // 0.1%
                    .Distinct()
                    .ToArray();

                Console.WriteLine(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.ConsoleMessageFormat_SolutionsPerSize,
                    size,
                    solutions.Length));
                foreach (var solution in solutions)
                {
                    Console.WriteLine(solution);
                    Console.WriteLine();
                }

                Console.WriteLine(stopwatch.Elapsed);
                return (int)ReturnCode.Success;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return (int)ReturnCode.InternalError;
            }
        }
    }
}
