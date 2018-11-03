using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalAnalysis
{
    public static class Lab3
    {
        /// <summary>
        /// Value of error
        /// </summary>
        static readonly double eps = 1e-8;

        /// <summary>
        /// Tabe partition
        /// </summary>
        private enum PartOfTable
        {
            Beginning,
            Middle,
            End,
            None
        }

        /// <summary>
        /// Get point in the right way
        /// </summary>
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
                Console.WriteLine("It is impossible to calculate the value in the point");
                return CheckX(ref table, n);
            }

            Console.WriteLine("This point is in the {0} of the table", part);
            return x;
        }

        /// <summary>
        /// Compute the value
        /// </summary>
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
        /// Get part of a table which point is related
        /// </summary>
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

            if ((x >= a + (((n + 1) / 2) * h)) && (x <= b - (((n + 1) / 2) * h)))
            {
                return PartOfTable.Middle;
            }

            if (Math.Abs(x - (a + (((n + 1) / 2) * h))) <= 0.00000000001)
            {
                return PartOfTable.Middle;
            }

            if (Math.Abs(x - (b - (((n + 1) / 2) * h))) <= 0.00000000001)
            {
                return PartOfTable.Middle;
            }

            return PartOfTable.None;
        }

        /// <summary>
        /// Get highest index of x in the table
        /// </summary>
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

            if (Math.Abs(x - table[result, 0]) < 0.00000001)
            {
                return result;
            }

            return result - 1;
        }

        /// <summary>
        /// Compute a value in the beginning of the table
        /// </summary>
        private static double BegValue(double x, int n, ref double[,] table)
        {
            var fdtable = TableTools.FiniteDifferences(ref table);
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
        /// Compute a value in the end of the table
        /// </summary>
        private static double EndValue(double x, int n, ref double[,] table)
        {
            var fdtable = TableTools.FiniteDifferences(ref table);
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
        /// Compute a value in the middle of the table
        /// </summary>
        private static double MidValue(double x, int n, ref double[,] table)
        {
            var fdtable = TableTools.FiniteDifferences(ref table);
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
                result += (mas[i] * fdtable[z0 - ((i + 1) / 2), i + 1]) / factorial;
            }

            return result;
        }

        /// <summary>
        /// Generate aray of indexes for z
        /// </summary>
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
