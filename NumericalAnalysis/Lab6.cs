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

        public static double[] FindPolynome(double[] moments)
        {
            var N = moments.Length / 2;
            var matrix = new double[N, N];
            var vector = new double[N];
            var polynome = new double[N + 1];
            polynome[0] = 1.0;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[j, N - i - 1] = moments[j + i];
                }
            }

            for (int i = 0; i < N; i++)
            {
                vector[N - i - 1] = -1 * moments[2 * N - i - 1];
            }

            vector =  AlgebraTools.Cramer(matrix, vector);

            for (int i = 0; i < N; i++)
            {
                polynome[i + 1] = vector[i];
            }

            return polynome;
        }

        public static double Gauss2Nodes(
            double a,
            double b,
            int m,
            Function.F f)
        {
            var h = (b - a) / m;
            var hh = h / 2;
            var sum = 0.0;
            var t1 = -Math.Sqrt(3) / 3;
            var t2 = Math.Sqrt(3) / 3;

            for (int i = 0; i < m; i++)
            {
                var temp = a + i * h;
                var f1 = f(hh * t1 + temp + hh);
                var f2 = f(hh * t2 + temp + hh);
                sum += hh * (f1 + f2);
            }

            return sum;
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
