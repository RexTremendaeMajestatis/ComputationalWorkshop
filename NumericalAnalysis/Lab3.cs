namespace NumericalAnalysis
{
    using System;

    /// <summary>
    /// Laboratory work #3
    /// </summary>
    public static class Lab3
    {
        /// <summary>
        /// Value of error
        /// </summary>
        private static readonly double Eps = 1e-8;

        /// <summary>
        /// Partition of table set function
        /// </summary>
        private enum PartOfTable
        {
            Beginning,
            Middle,
            End,
            None
        }

        /// <summary>
        /// Read and check degree of interpoation polynome
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>Degree of interpolation polynome</returns>
        public static int CheckN(ref double[,] table)
        {
            var m = table.GetLength(0) - 1;

            Console.WriteLine(
                "Print a degree of polynome no greater than m ({0})",
                m);
            var n = int.Parse(Console.ReadLine());

            if ((n <= m) && (n >= 1))
            {
                return n;
            }

            Console.WriteLine("Something went wrong, try again");
            return CheckN(ref table);
        }

        /// <summary>
        /// Read and check value of preimage
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <param name="n">Order of interpoation polynome</param>
        /// <returns>Value of preimage</returns>
        public static double CheckX(ref double[,] table, int n)
        {
            var m = table.GetLength(0);
            Console.WriteLine(">Enter a point");
            Console.WriteLine(
                "[{0};{1}] V [{2};{3}] V [{4};{5}]",
                table[0, 0],
                table[1, 0],
                table[(n + 1) / 2, 0],
                table[m - 1 - ((n + 1) / 2), 0],
                table[m - 2, 0],
                table[m - 1, 0]);
            var x = double.Parse(Console.ReadLine());
            var part = Part(x, n, ref table);

            if (part == PartOfTable.None)
            {
                Console.WriteLine(
                    "It is impossible to calculate the value in the point");
                return CheckX(ref table, n);
            }

            Console.WriteLine("This point is in the {0} of the table", part);
            return x;
        }

        /// <summary>
        /// Get value of image by value of preimage
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="n">Order of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Value of preimage</returns>
        public static double Evaluate(double x, int n, ref double[,] table)
        {
            var part = Part(x, n, ref table);

            if (part == PartOfTable.Beginning)
            {
                return BegValue(x, n, ref table);
            }

            if (part == PartOfTable.End)
            {
                return EndValue(x, n, ref table);
            }

            if (part == PartOfTable.Middle)
            {
                return MidValue(x, n, ref table);
            }

            return 0;
        }

        /// <summary>
        /// Get part of table which value of preimage is related to
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="n">Order of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Part of table</returns>
        private static PartOfTable Part(double x, int n, ref double[,] table)
        {
            var a = table[0, 0];
            var b = table[table.GetLength(0) - 1, 0];
            var m = table.GetLength(0) - 1;
            var h = (b - a) / (double)m;

            if ((x >= a) && (x <= a + h))
            {
                return PartOfTable.Beginning;
            }

            if ((x >= b - h) && (x <= b))
            {
                return PartOfTable.End;
            }

            if ((x >= a + (((n + 1) / 2) * h)) &&
                (x <= b - (((n + 1) / 2) * h)))
            {
                return PartOfTable.Middle;
            }

            if (Math.Abs(x - (a + (((n + 1) / 2) * h))) <= Eps)
            {
                return PartOfTable.Middle;
            }

            if (Math.Abs(x - (b - (((n + 1) / 2) * h))) <= Eps)
            {
                return PartOfTable.Middle;
            }

            return PartOfTable.None;
        }

        /// <summary>
        /// Get upper index in table for value of preimage
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="table">Table set function</param>
        /// <returns>High bound index</returns>
        private static int Bounds(double x, double[,] table)
        {
            var result = 0;

            for (int i = 0; i < table.GetLength(0); i++)
            {
                if (x <= table[i, 0])
                {
                    result = i;
                    break;
                }
            }

            if (Math.Abs(x - table[result, 0]) < Eps)
            {
                return result;
            }

            return result - 1;
        }

        /// <summary>
        /// Get image value for preimages from beginning of table
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="n">Order of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Value of interpolation polynome image</returns>
        private static double BegValue(double x, int n, ref double[,] table)
        {
            var fdtable = TableTools.FiniteDifferences(table);
            var h = table[1, 0] - table[0, 0];
            var t = (x - table[0, 0]) / h;
            var mas = new double[n];
            var result = fdtable[0, 0];
            var factorial = 1;
            mas[0] = t;

            for (int i = 1; i < n; i++)
            {
                mas[i] = mas[i - 1] * (t - i);
            }

            for (int i = 0; i < n; i++)
            {
                factorial *= i + 1;
                result += mas[i] * fdtable[0, i + 1] / factorial;
            }

            return result;
        }

        /// <summary>
        /// Get image value for preimages from end of table
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="n">Order of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Value of interpolation polynome image</returns>
        private static double EndValue(double x, int n, ref double[,] table)
        {
            var fdtable = TableTools.FiniteDifferences(table);
            var m = table.GetLength(0) - 1;
            var h = table[1, 0] - table[0, 0];
            var t = (x - table[m, 0]) / h;
            var mas = new double[n];
            var result = fdtable[m, 0];
            var factorial = 1;
            mas[0] = t;

            for (int i = 1; i < n; i++)
            {
                mas[i] = mas[i - 1] * (t + i);
            }

            for (int i = 0; i < n; i++)
            {
                factorial *= i + 1;
                result += mas[i] * fdtable[m - i - 1, i + 1] / factorial;
            }

            return result;
        }

        /// <summary>
        /// Get image value for preimages from middle of table
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="n">Order of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Value of interpolation polynome image</returns>
        private static double MidValue(double x, int n, ref double[,] table)
        {
            var fdtable = TableTools.FiniteDifferences(table);
            var z0 = Bounds(x, table);
            var m = table.GetLength(0) - 1;
            var h = table[1, 0] - table[0, 0];
            var t = (x - table[z0, 0]) / h;
            var mas = new double[n];
            var indexes = GenerateIndex(n);
            var factorial = 1;
            var result = fdtable[z0, 0];
            mas[0] = t;

            for (int i = 1; i < n; i++)
            {
                mas[i] = mas[i - 1] * (t + indexes[i]);
            }

            for (int i = 0; i < n; i++)
            {
                factorial *= i + 1;
                result += (mas[i] * fdtable[z0 - ((i + 1) / 2), i + 1]) /
                    factorial;
            }

            return result;
        }

        /// <summary>
        /// Generate array of indexes for interpolation polynome
        /// </summary>
        /// <param name="n">Order of interpolation polynode</param>
        /// <returns>Array of indexes</returns>
        private static int[] GenerateIndex(int n)
        {
            var res = new int[n];

            var t = 1;
            for (int i = 0; i < n; i++)
            {
                res[i] = t * ((i + 1) / 2);
                t *= -1;
            }

            return res;
        }
    }
}
