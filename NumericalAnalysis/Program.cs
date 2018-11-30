namespace NumericalAnalysis
{
    using System;

    public static class Program
    {
        /// <summary>
        /// Get table set function with equidistant nodes
        /// </summary>
        /// <param name="a">Beginning of segment</param>
        /// <param name="b">End of segment</param>
        /// <param name="m">Amount of parts</param>
        /// <param name="f">Function</param>
        /// <returns>Table set function</returns>
        public static double[,] GetEquidistantTable(double a, double b, int m, Function.F f)
        {
            var table = new double[m + 1, 2];

            for (int i = 0; i < m + 1; i++)
            {
                table[i, 0] = a + (((b - a) / (double)m) * i);
                table[i, 1] = f.Invoke(table[i, 0]);
            }

            return table;
        }

        public static double[,] GetEquidistantTable(
            double a,
            double h,
            int beg,
            int N,
            Function.F f)
        {
            int m = N - beg;

            var table = new double[m + 1, 2];

            for (int i = beg; i < N + 1; i++)
            {
                table[i - beg, 0] = a + (i * h);
                table[i - beg, 1] = f.Invoke(a + (i * h));
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
        public static double Solve(double a, double b, double p, Function.F f, double eps = 1e-8)
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
        
        public static double y(double x)
        {
            return 1 / (1 + Math.Exp(x));
        }

        public static double Tailor(double x)
        {
            return (1 / 2.0) +
                (-1 / 4.0) * x +
                (1 / 8.0) * Math.Pow(x, 3) +
                (-1 / 4.0) * Math.Pow(x, 5) +
                (68 / 64.0) * Math.Pow(x, 7);
        }

        public static void Main(string[] args)
        {
            var x0 = 0.0;
            var h = 0.1;
            var N = 10;

            // I)
            Console.WriteLine("I)");
            var table = GetEquidistantTable(x0, h, -2, N, y);
            OutputTools.Print(table);
            Console.WriteLine();

            // II)
            Console.WriteLine("II)");
            var tailorTable = GetEquidistantTable(x0, h, -2, 2, Tailor);
            OutputTools.Print(tailorTable);

            Console.ReadLine();
            Main(args);
        }
    }
}
