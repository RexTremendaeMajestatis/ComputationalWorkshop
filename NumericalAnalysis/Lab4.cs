namespace NumericalAnalysis
{
    using System;

    /// <summary>
    /// Laboratory work #4
    /// </summary>
    public static class Lab4
    {
        /// <summary>
        /// Value of error
        /// </summary>
        private static readonly double Eps = 1e-8;

        /// <summary>
        /// Read and check degree of interpoation polynome
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>Degree of interpolation polynome</returns>
        public static int CheckN(ref double[,] table)
        {
            var m = table.GetLength(0) - 1;

            Console.WriteLine("Print a degree of polynome no greater than m ({0})", m);
            var n = int.Parse(Console.ReadLine());

            if ((n <= m) && (n >= 1))
            {
                return n;
            }

            Console.WriteLine("Something went wrong, try again");
            return CheckN(ref table);
        }

        /// <summary>
        /// Read and check value of image
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>Value of image</returns>
        public static double CheckF(ref double[,] table)
        {
            var m = table.GetLength(0) - 1;

            Console.WriteLine(
                "Print a value of preimage in range of [{0}, {1}]",
                table[0, 1],
                table[m, 1]);
            var p = double.Parse(Console.ReadLine());

            if ((p >= table[0, 1]) && (p <= table[m, 1]))
            {
                return p;
            }

            Console.WriteLine("Something went wrong, try again");
            return CheckF(ref table);
        }

        /// <summary>
        /// Get value of preimage by value of image
        /// </summary>
        /// <param name="p">Value of image</param>
        /// <param name="n">Degree of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Value of preimage</returns>
        public static double Preimage_I(double p, int n, ref double[,] table)
        {
            var swappedTable = TableTools.SwapColumns(table);
            return Lab2.Lagrange(p, n, ref swappedTable);
        }

        /// <summary>
        /// Get value of preimage by value of image
        /// </summary>
        /// <param name="p">Value of image</param>
        /// <param name="n">Degree of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Value of preimage</returns>
        public static double Preimage_II(double p, int n, ref double[,] table)
        {
            var m = table.GetLength(0) - 1;
            var a = table[0, 0];
            var b = table[m, 0];

            if (Math.Abs(Lab2.Lagrange(a, n, ref table) - p) < Eps)
            {
                return a;
            }

            if (Math.Abs(Lab2.Lagrange(b, n, ref table) - p) < Eps)
            {
                return b;
            }

            var mid = 0.0;

            while (Math.Abs(b - a) > Eps)
            {
                mid = (b + a) / 2;

                if ((Lab2.Lagrange(a, n, ref table) - p) * (Lab2.Lagrange(mid, n, ref table) - p) < 0)
                {
                    b = mid;
                }
                else
                {
                    a = mid;
                }
            }

            return mid;
        }

        /// <summary>
        /// Get derivatives in nodes of table set function
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>Vector of derivatives</returns>
        public static double[] Derivatives(ref double[,] table)
        {
            var m = table.GetLength(0) - 1;
            var derivatives = new double[m + 1];

            derivatives[0] = ((-3 * table[0, 1]) + (4 * table[1, 1]) - table[2, 1]) /
                Math.Abs(table[2, 0] - table[0, 0]);

            for (int i = 1; i < m; i++)
            {
                derivatives[i] = (table[i + 1, 1] - table[i - 1, 1]) /
                    Math.Abs(table[i + 1, 0] - table[i - 1, 0]);
            }

            derivatives[m] = ((3 * table[m, 1]) - (4 * table[m - 1, 1]) + table[m - 2, 1]) /
                Math.Abs(table[m - 2, 0] - table[m, 0]);

            return derivatives;
        }

        /// <summary>
        /// Get second derivatives in nodes of table set function
        /// </summary>
        /// <param name="table">Table set function</param>
        /// <returns>Vector of second derivatives</returns>
        public static double[] SecondDerivatives(ref double[,] table)
        {
            var m = table.GetLength(0) - 1;
            var h = table[1, 0] - table[0, 0];
            var secondDerivatives = new double[m + 1];

            for (int i = 1; i < m; i++)
            {
                secondDerivatives[i] = (table[i + 1, 1] - (2 * table[i, 1]) + table[i - 1, 1]) /
                    (h * h);
            }

            return secondDerivatives;
        }
    }
}
