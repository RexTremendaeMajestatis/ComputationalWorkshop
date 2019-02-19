using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalAnalysis._5sem
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
                    (h *
                     f(table[i - 1, 0],
                       table[i - 1, 1]));
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
                    ((h / 2) * f(table[i - 1, 0], table[i - 1, 1]));
                table[i, 0] = x0 + (i * h);
                table[i, 1] = table[i - 1, 1] +
                    (h *
                     f(table[i - 1, 0] +
                       (h / 2),
                       ytemp));
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
                ytemp = table[i - 1, 1] +
                    (h *
                     f(table[i - 1, 0],
                       table[i - 1, 1]));
                table[i, 0] = x0 + (i * h);
                table[i, 1] = table[i - 1, 1] +
                    ((h / 2) *
                    (f(table[i - 1, 0],
                       table[i - 1, 1]) +
                     f(table[i, 0],
                       ytemp)));
            }

            return table;
        }

        public static double[,] RungeKutt(
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

            var k1 = 0.0;
            var k2 = 0.0;
            var k3 = 0.0;
            var k4 = 0.0;

            for (int i = 1; i < m + 1; i++)
            {
                k1 = h * f(table[i - 1, 0], table[i - 1, 1]);
                k2 = h *
                    f(table[i - 1, 0] +
                      (h / 2.0), table[i - 1, 1] +
                      (k1 / 2.0));
                k3 = h *
                    f(table[i - 1, 0] +
                      (h / 2.0), table[i - 1, 1] +
                      (k2 / 2.0));
                k4 = h * 
                    f(table[i - 1, 0] + h,
                      table[i - 1, 1] + k3);

                table[i, 0] = x0 + (i * h);
                table[i, 1] = table[i - 1, 1] +
                    (1 / 6.0) *
                    (k1 + 2 *
                     (k2 + k3) + k4);
            }

            return table;
        }

        public static double[,] Adams(
            Function.G f,
            double[,] nodes,
            double h,
            int k,
            int N)
        {
            int m0 = nodes.GetLength(0) - 1;
            int m1 = N - k;
            var table = new double[m0 + m1 + 2, 2];

            for (int i = 0; i < m0 + 1; i++)
            {
                table[i, 0] = nodes[i, 0];
                table[i, 1] = nodes[i, 1];
            }

            var etha = new double[m0 + m1 + 2, 5];

            for (int i = 0; i < m0 + 1; i++)
            {
                etha[i, 0] = h * f(table[i, 0], table[i, 1]);
            }

            for (int j = 1; j < 5; j++)
            {
                for (int i = 0; i < 4 - j + 1; i++)
                {
                    etha[i, j] = etha[i + 1, j - 1] - etha[i, j - 1];
                }
            }

            var delta = 0.0;

            for (int i = m0 + 1; i < m0 + m1 + 2; i++)
            {
                delta = etha[i - 1, 0] +
                    (1 / 2.0) * etha[i - 2, 1] +
                    (5 / 12.0) * etha[i - 3, 2] +
                    (3 / 8.0) * etha[i - 4, 3] +
                    (251 / 720.0) * etha[i - 5, 4];
                table[i, 1] = table[i - 1, 1] + delta;
                table[i, 0] = table[i - 1, 0] + h;

                etha[i, 0] = h * f(table[i, 0], table[i, 1]);

                for (int j = 1; j < m0 + 1; j++)
                {
                    etha[i - j, j] = etha[i - j + 1, j - 1] -
                        etha[i - j, j - 1]; 
                }
            }

            return table;
        }

    }
}
