namespace Sem5
{
    using System;

    public static class Lab6
    {
        private static readonly double step = 1e-7;

        public static double[] Moments(double a, double b, int N, ComputationalWorkShop.Function.F w)
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

            vector =  Tools.AlgebraTools.Cramer(matrix, vector);

            for (int i = 0; i < N; i++)
            {
                polynome[i + 1] = vector[i];
            }

            return polynome;
        }

        public static double Gauss2NodesCompound(
            double a,
            double b,
            int m,
            ComputationalWorkShop.Function.F f)
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

        public static double GaussType(
            double a,
            double b,
            int N,
            ComputationalWorkShop.Function.F w,
            ComputationalWorkShop.Function.F f)
        {
            var moments = Moments(a, b, N, w);
            var polynome = Lab6.FindPolynome(moments);
            var x = Tools.AlgebraTools.SolveSquare(polynome);
            Array.Sort(x);
            var matrix = new double[2, 2] { { 1, 1 }, { x[0], x[1] } };
            var vector = new double[2] { moments[0], moments[1] };
            var A = Tools.AlgebraTools.Cramer(matrix, vector);
            return A[0] * f(x[0]) + A[1] * f(x[1]);
        }

        private static double Moment(double a, double b, int k, ComputationalWorkShop.Function.F w)
        {
            var h = (int)((b - a) / step);

            return MomentMiddle(a, b, h, k, w);
        }

        private static double MomentMiddle(
            double a,
            double b,
            int m,
            int k,
            ComputationalWorkShop.Function.F w)
        {
            var h = (b - a) / m;
            var sum = 0.0;
            var alpha = a + (h / 2);

            for (int i = 0; i < m; i++)
            {
                sum += w(alpha + (i * h)) * Math.Pow(alpha + (i * h), k);
            }

            return h * sum;
        }


    }
}
