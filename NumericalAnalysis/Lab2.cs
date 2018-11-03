using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalAnalysis
{
    public static class Lab2
    {
        public static double Lagrange(double x, int n, ref double[,] table)
        {
            var sorted = TableTools.SortTable(x, ref table);
            var result = 0.0;

            for (int i = 0; i < n + 1; i++)
            {
                var l = 1.0;

                for (int j = 0; j < n + 1; j++)
                {
                    if (j != i)
                    {
                        l *= (x - sorted[j, 0]) / (sorted[i, 0] - sorted[j, 0]);
                    }

                }

                result += l * sorted[i, 1];
            }

            return result;
        }

        public static double Newton(double x, int n, ref double[,] table)
        {
            var sorted = TableTools.SortTable(x, ref table);
            var ftable = TableTools.DividedDifferences(sorted, n);

            var m = new double[n + 1];
            m[0] = 1;

            for (int i = 1; i < n + 1; i++)
            {
                m[i] = m[i - 1] * (x - sorted[i - 1, 0]);
            }

            var result = 0.0;

            for (int i = 0; i < n + 1; i++)
            {
                result += ftable[0, i] * m[i];
            }

            return result;
        }
    }
}
