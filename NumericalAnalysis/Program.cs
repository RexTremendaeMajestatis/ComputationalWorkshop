namespace NumericalAnalysis
{
    using System;

    public static class Program
    {
        /// <summary>
        /// Container for functions
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <returns>Value of image</returns>
        public delegate double Function(double x);

        /// <summary>
        /// (1 + x ^ 2) ^ (1 / 2)
        /// </summary>
        /// <param name="x">Value of preimaget</param>
        /// <returns>Value of image</returns>
        public static double F_I(double x)
        {
            return Math.Sqrt(1 + (x * x));
        }

        /// <summary>
        /// e^(6 * x)
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <returns>Value of image</returns>
        public static double F_II(double x)
        {
            return Math.Pow(Math.E, 6 * x);
        }

        /// <summary>
        /// 6e^(6 * x)
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <returns>Value of image</returns>
        public static double F_II_D(double x)
        {
            return 6 * Math.Pow(Math.E, 6 * x);
        }

        /// <summary>
        /// 36e^(6 * x)
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <returns>Value of image</returns>
        public static double F_II_DD(double x)
        {
            return 36 * Math.Pow(Math.E, 6 * x);
        }

        /// <summary>
        /// Get table set function with equidistant nodes
        /// </summary>
        /// <param name="a">Beginning of segment</param>
        /// <param name="b">End of segment</param>
        /// <param name="m">Amount of parts</param>
        /// <param name="f">Function</param>
        /// <returns>Table set function</returns>
        public static double[,] GetEquidistantTable(double a, double b, int m, Function f)
        {
            var table = new double[m + 1, 2];

            for (int i = 0; i < m + 1; i++)
            {
                table[i, 0] = a + (((b - a) / (double)m) * i);
                table[i, 1] = f.Invoke(table[i, 0]);
            }

            return table;
        }

        /// <summary>
        /// Find roots of f(x) = F on segment
        /// </summary>
        /// <param name="a">Beginning of segment</param>
        /// <param name="b">End of segment</param>
        /// <param name="p">Value of image</param>
        /// <param name="f">Function</param>
        /// <param name="eps">Error</param>
        /// <returns>Value of preimage</returns>
        public static double Solve(double a, double b, double p, Function f, double eps = 1e-8)
        {
            if (Math.Abs(f.Invoke(a) - p) < eps)
            {
                return a;
            }

            if (Math.Abs(f.Invoke(b) - p) < eps)
            {
                return b;
            }

            var mid = 0.0;

            while (Math.Abs(b - a) > eps)
            {
                mid = (b + a) / 2;

                if ((f.Invoke(a) - p) * (f.Invoke(mid) - p) < 0)
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

        public static void Main(string[] args)
        {
            // Segment bounds and split range
            var a = -1.0;
            var b = 0.0;
            var m = 10;

            // Equidistant table
            var table = GetEquidistantTable(a, b, m, F_I);
            OutputTools.Print(table);

            // Image value and polynome order
            var p = Lab4.CheckF(ref table);
            var n = Lab4.CheckN(ref table);

            // Accurate preimage value
            var expected = Solve(a, b, p, F_I);

            // Approximate preimage value
            var preimage_I = Lab4.Preimage_I(p, n, ref table);
            var preimage_II = Lab4.Preimage_II(p, n, ref table);

            Console.WriteLine(
                "I: {0}, error: {1}",
                preimage_I,
                Math.Abs(preimage_I - expected));
            Console.WriteLine(
                "II: {0}, error: {1}",
                preimage_II,
                Math.Abs(preimage_II - expected));

            Console.WriteLine();

            // New table set function
            table = GetEquidistantTable(a, b, m, F_II);

            // Derivatives and second derivatives in nodes of table set function
            var der = Lab4.Derivatives(ref table);
            var sder = Lab4.SecondDerivatives(ref table);

            // Error of derivatives in nodes of table set function
            var derror = new double[m + 1];

            for (int i = 0; i < m + 1; i++)
            {
                derror[i] = Math.Abs(F_II_D(table[i, 0]) - der[i]);
            }

            // Error of second derivatives in nodes of table set function
            var sderror = new double[m + 1];

            for (int i = 1; i < m; i++)
            {
                sderror[i] = Math.Abs(F_II_DD(table[i, 0]) - sder[i]);
            }

            OutputTools.Print(table, der, derror, sder, sderror);

            Console.ReadLine();
            Main(args);
        }
    }
}
