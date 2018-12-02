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
        public static double[,] GetEquidistantTable(
            double a,
            double b,
            int m,
            Function.F f)
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
        public static double Solve(
            double a,
            double b,
            double p,
            Function.F f,
            double eps = 1e-8)
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

        public static double f(double x, double y)
        {
            return -y + (y * y);
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
            var y0 = 0.5;
            var h = 0.1;
            var N = 10;
            var beg = -2;
            var end = 2;
            var k0 = 1;
            var k1 = 3;

            Console.WriteLine(
                "y' = -y + y^2\ny({0}) = {1}",
                x0, y0);
            Console.WriteLine(
                "h = {0}\nN = {1}\n{2} <= k <= {3}\n",
                h,
                N,
                beg,
                end);
            
            var table = GetEquidistantTable(x0, h, beg, N, y);
            var tailorTable = GetEquidistantTable(x0, h, beg, end, Tailor);
            var rungeKuttTable = Lab7.RungeKutt(x0, y0, h, k0, N, f);
            var adamsTable = Lab7.Adams(f, tailorTable, h, k1, N);
            var eulerTableI = Lab7.EulerI(x0, y0, h, k0, N, f);
            var eulerTableII = Lab7.EulerII(x0, y0, h, k0, N, f);
            var eulerTableIII = Lab7.EulerIII(x0, y0, h, k0, N, f);

            var m0 = end - beg;
            var m1 = N;
            var m2 = N - beg;

            var errorsTailor = new double[m0 + 1];
            var errorsRungeKutt = new double[m1 + 1];
            var errorsAdams = new double[m2 + 1];
            var errorsEulerI = new double[m1 + 1];
            var errorsEulerII = new double[m1 + 1];
            var errorsEulerIII = new double[m1 + 1];

            for (int i = 0; i < m0 + 1; i++)
            {
                errorsTailor[i] = Math.Abs(tailorTable[i, 1] - table[i, 1]);
            }

            for (int i = 0; i < m1 + 1; i++)
            {
                errorsRungeKutt[i] = Math.Abs(rungeKuttTable[i, 1] -
                                              table[i - beg, 1]);
                errorsEulerI[i] = Math.Abs(eulerTableI[i, 1] -
                                           table[i - beg, 1]);
                errorsEulerII[i] = Math.Abs(eulerTableII[i, 1] -
                           table[i - beg, 1]);
                errorsEulerIII[i] = Math.Abs(eulerTableIII[i, 1] -
                           table[i - beg, 1]);
            }

            for (int i = 0; i < m2 + 1; i++)
            {
                errorsAdams[i] = Math.Abs(adamsTable[i, 1] - table[i, 1]);
            }

            OutputTools.Print(table, "Expected");
            OutputTools.Print(tailorTable, "Tailor", errorsTailor);
            OutputTools.Print(rungeKuttTable, "Runge-Kutt", errorsRungeKutt);
            OutputTools.Print(adamsTable, "Adams", errorsAdams);
            OutputTools.Print(eulerTableI, "Euler I", errorsEulerI);
            OutputTools.Print(eulerTableII, "Euler II", errorsEulerII);
            OutputTools.Print(eulerTableIII, "Euler III", errorsEulerIII);

            Console.ReadLine();
            Main(args);
        }
    }
}
