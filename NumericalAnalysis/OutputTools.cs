namespace NumericalAnalysis
{
    using System;

    /// <summary>
    /// Tools for printing tables
    /// </summary>
    public static class OutputTools
    {
        /// <summary>
        /// Print all table values
        /// </summary>
        /// <typeparam name="T">Type that table contains</typeparam>
        /// <param name="table">Table</param>
        public static void Print<T>(T[,] table)
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                Console.Write("{0}\t ", i + 1);

                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(string.Format("| {0}\t ", table[i, j]));
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Print all vector values
        /// </summary>
        /// <typeparam name="T">Type that vector contains</typeparam>
        /// <param name="vector">Vector</param>
        public static void Print<T>(T[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                Console.WriteLine(string.Format("{0}\t | {1}\t ", i + 1, vector[i]));
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Print all table and vectors values
        /// </summary>
        /// <typeparam name="T">Type that table and vectors contain</typeparam>
        /// <param name="table">Table</param>
        /// <param name="list">List of vectors</param>
        public static void Print<T>(double[,] table, params T[][] list)
        {
            int m = table.GetLength(0) - 1;
            
            for (int i = 0; i < m + 1; i++)
            {
                Console.Write(
                    "{0}\t | {1}\t | {2}",
                    i + 1,
                    table[i, 0],
                    table[i, 1]);
                for (int j = 0; j < list.Length; j++)
                {
                    Console.Write(" | {0}\t", list[j][i]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
