namespace Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Tools for work with tables
    /// </summary>
    public static class Table
    {
        /// <summary>
        /// Sort table by distance to x
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="table">Table set function</param>
        /// <returns>Sorted by distance to x table</returns>
        public static double[,] SortTable(double x, ref double[,] table)
        { 
            var m = table.GetLength(0) - 1;
            var tempDiv = new Dictionary<double, double>();

            for (int i = 0; i < m + 1; i++)
            {
                tempDiv.Add(table[i, 0], table[i, 1]);
            }

            var dic = new Dictionary<double, double>();

            foreach (var key in tempDiv.Keys.OrderBy(y => Math.Abs(y - x)))
            {
                dic.Add(key, tempDiv[key]);
            }

            var sortedTable = new double[m + 1, 2];
            var j = 0;

            foreach (var item in dic)
            {
                sortedTable[j, 0] = item.Key;
                sortedTable[j, 1] = item.Value;
                j++;
            }

            return sortedTable;
        }

        /// <summary>
        /// Swap coulumns of table
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>Table with swapped columns</returns>
        public static double[,] SwapColumns(double[,] table)
        {
            var rows = table.GetLength(0);
            var columns = table.GetLength(1);
            double[,] swapTable = new double[rows, columns];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    swapTable[j, i] = table[j, columns - i - 1];
                }
            }

            return swapTable;
        }

        /// <summary>
        /// Get all finite differences
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>Table of finite differences</returns>
        public static double[,] FiniteDifferences(double[,] table)
        {
            var n = table.GetLength(0);
            var result = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                result[i, 0] = table[i, 1];
            }

            for (int j = 1; j < n; j++)
            {
                for (int i = 0; i < n - j; i++)
                {
                    result[i, j] = result[i + 1, j - 1] - result[i, j - 1];
                }
            }

            return result;
        }

        /// <summary>
        /// Get finite differences up to order n
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <param name="n">Order of the last finite difference</param>
        /// <returns>Table of finite differences</returns>
        public static double[,] FiniteDifferences(double[,] table, int n)
        {
            var result = new double[n + 1, n + 1];

            for (int i = 0; i < n + 1; i++)
            {
                result[i, 0] = table[i, 1];
            }

            for (int j = 1; j < n + 1; j++)
            {
                for (int i = 0; i < n - j + 1; i++)
                {
                    result[i, j] = result[i + 1, j - 1] - result[i, j - 1];
                }
            }

            return result;
        }

        /// <summary>
        /// Get all divided differences
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>Table of divided differences</returns>
        public static double[,] DividedDifferences(double[,] table)
        {
            var n = table.GetLength(0);
            var result = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                result[i, 0] = table[i, 1];
            }

            for (int j = 1; j < n; j++)
            {
                for (int i = 0; i < n - j; i++)
                {
                    result[i, j] = (result[i + 1, j - 1] - result[i, j - 1]) / (table[i + 1, 0] - table[i, 0]);
                }
            }

            return result;
        }

        /// <summary>
        /// Get divided differences up to order n
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <param name="n">Orded of the last divided difference</param>
        /// <returns>Table of divided differences</returns>
        public static double[,] DividedDifferences(double[,] table, int n)
        {
            var result = new double[n + 1, n + 1];

            for (int i = 0; i < n + 1; i++)
            {
                result[i, 0] = table[i, 1];
            }

            for (int j = 1; j < n + 1; j++)
            {
                for (int i = 0; i < n - j + 1; i++)
                {
                    result[i, j] = (result[i + 1, j - 1] - result[i, j - 1]) / (table[i + j, 0] - table[i, 0]);
                }
            }

            return result;
        }

        /// <summary>
        /// Get gaps of monotony of table set function
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>List of table set functions</returns>
        public static List<double[,]> GetGapsOfMonotony(double[,] table)
        {
            var gaps = new List<double[,]>();
            var indexes = new List<int>();
            var m = table.GetLength(0);

            indexes.Add(0);

            for (int i = 0; i < m - 2; i++)
            {
                if (((table[i + 1, 1] - table[i, 1]) / (table[i + 1, 0] - table[i, 0]))
                    * ((table[i + 2, 1] - table[i + 1, 1]) / (table[i + 2, 0] - table[i + 1, 0])) <= 0)
                {
                    indexes.Add(i + 1);
                }
            }

            indexes.Add(m - 1);

            for (int i = 0; i < indexes.Count - 1; i++)
            {
                var length = indexes[i + 1] - indexes[i] + 1;
                var temp = new double[length, 2];

                for (int j = 0; j < length; j++)
                {
                    temp[j, 0] = table[indexes[i] + j, 0];
                    temp[j, 1] = table[indexes[i] + j, 1];
                }

                gaps.Add(temp);
            }

            return gaps;
        }

        public static double[,] Wide(double[,] A, double[] b)
        {
            var rows = A.GetLength(0);
            var columns = A.GetLength(1);
            var result = new double[rows, columns + 1];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = A[i, j];
                }
            }

            for (int i = 0; i < rows; i++)
            {
                result[i, columns] = b[i];
            }

            return result;
        }
    }
}
