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

        public static double df(double x)
        {
            return -1 * Math.Cos(x);
        }

        public static void Main(string[] args)
        {
            var A = 0.0;
            var B = 1.0;
            var m = 1000;
            Lab5.Function f = Math.Sin;
            var J = df(B) - df(A);
            Console.WriteLine("Expected: {0}", J);
            var J1 = Lab5.Rectangle(A, B, m, f, Lab5.Part.Left);
            var J2 = Lab5.Rectangle(A, B, m, f, Lab5.Part.Middle);
            var J3 = Lab5.Rectangle(A, B, m, f, Lab5.Part.Right);
            var J4 = Lab5.Trapeze(A, B, m, f);
            var J5 = Lab5.Simpson(A, B, m, f);
            Console.WriteLine(
                "Left rectangles:\t{0}\terror:\t{1}",
                J1, Math.Abs(J - J1));
            Console.WriteLine(
                "Middle rectangles:\t{0}\terror:\t{1}",
                J2, Math.Abs(J - J2));
            Console.WriteLine(
                "Rights rectangles:\t{0}\terror:\t{1}",
                J3, Math.Abs(J - J3));
            Console.WriteLine(
                "Trapeze:\t{0}\terror:\t{1}",
                J4, Math.Abs(J - J4));
            Console.WriteLine(
                "Simpson:\t{0}\terror:\t{1}",
                J5, Math.Abs(J - J5));
            Console.ReadLine();
            Main(args);
        }
    }
}
