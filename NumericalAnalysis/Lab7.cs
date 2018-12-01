using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalAnalysis
{
    public static class Lab7
    {
        public static double[,] EulerI(
            double x0,
            double y0,
            double h,
            int k,
            int N,
            Function.G f)
        {
            var m = N - k + 1;
            var table = new double[m + 1, 2];

            table[0, 0] = x0;
            table[0, 1] = y0;

            for (int i = 1; i < m + 1; i++)
            {
                table[i, 0] = x0 + (i * h);
                table[i, 1] = table[i - 1, 1] + 
                    h * f(table[i - 1, 0], table[i - 1, 1]);
            }

            return table;
        }

        public static double[,] EulerII(
            double x0,
            double y0,
            double h,
            int k,
            int N,
            Function.G f)
        {
            var m = N - k + 1;
            var table = new double[m + 1, 2];

            table[0, 0] = x0;
            table[0, 1] = y0;

            var ytemp = 0.0;

            for (int i = 1; i < m + 1; i++)
            {
                ytemp = table[i - 1, 1] +
                    (h / 2) * f(table[i - 1, 0], table[i - 1, 1]);
                table[i, 0] = x0 + (i * h);
                table[i, 1] = table[i - 1, 1] + h * f(table[i - 1, 0] + (h / 2), ytemp);
            }

            return table;
        }

        public static double[,] EulerIII(
            double x0,
            double y0,
            double h,
            int k,
            int N,
            Function.G f)
        {
            var m = N - k + 1;
            var table = new double[m + 1, 2];

            table[0, 0] = x0;
            table[0, 1] = y0;

            var ytemp = 0.0;

            for (int i = 1; i < m + 1; i++)
            {
                ytemp = table[i - 1, 1] + h * f(table[i - 1, 0], table[i - 1, 1]);
                table[i, 0] = x0 + (i * h);
                table[i, 1] = table[i - 1, 1] + (h / 2) *
                    (f(table[i - 1, 0], table[i - 1, 1]) + f(table[i, 0], ytemp));
            }

            return table;
        }
    }
}
