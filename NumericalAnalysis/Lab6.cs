using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalAnalysis
{
    public static class Lab6
    {
        private static readonly double step = 1e-5;

        public static double[] Moments(double a, double b, int N, Function.F w)
        {
            var moments = new double[2 * N];

            for (int i = 0; i < 2 * N; i++)
            {
                moments[i] = Moment(a, b, i, w);
            }

            return moments;
        }
        private static double Moment(double a, double b, int k, Function.F w)
        {
            var h = (int)((b - a) / step);

            return MomentSimpson(a, b, h, k, w);
        }

        private static double MomentSimpson(
            double a,
            double b,
            int m,
            int k,
            Function.F w)
        {
            var h = (b - a) / (2 * m);
            var sum = (w(a + 0.00001) * Math.Pow(b, k)) + (w(b) * Math.Pow(b, k));

            for (int i = 1; i < 2 * m; i++)
            {
                if (i % 2 == 0)
                {
                    sum += 2 * w(a + (i * h)) * Math.Pow(a + (i * h), k);
                }
                else
                {
                    sum += 4 * w(a + (i * h)) * Math.Pow(a + (i * h), k);
                }
            }

            return (h / 3) * sum;
        }
    }
}
