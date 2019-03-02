namespace Sem6
{
    using System;
    using System.Collections.Generic;

    class Lab1
    {
        // for dim = 2
        public static double Determinant(double[,] A)
        {
            return (A[0, 0] * A[1, 1]) - (A[1, 0] * A[0, 1]);
        }

        public static double[,] Reverse(double[,] A)
        {
            var result = new double[2, 2];
            var determinant = Determinant(A);
            result[0, 0] = A[1, 1] / determinant;
            result[1, 1] = A[0, 0] / determinant;
            result[0, 1] = -A[0, 1] / determinant;
            result[1, 0] = -A[1, 0] / determinant;

            return result;
        }

        public static double[] Kramer(double[,] A, double[] b)
        {
            var determinantA = Determinant(A);

            var dim = A.GetLength(0);

            List<double[,]> matrixes = new List<double[,]>();

            for (int d = 0; d < dim; d++)
            {
                var temp = new double[dim, dim];

                for (int i = 0; i < dim; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (j != d)
                        {
                            temp[i, j] = A[i, j];
                        }
                        else
                        {
                            temp[i, j] = b[i];
                        }
                    }
                }

                matrixes.Add(temp);
            }

            var res = new double[dim];

            for (int i = 0; i < dim; i++)
            {
                res[i] = Determinant(matrixes[i]) / determinantA;
            }

            return res;
        }

        public static double[] Gauss(double[,] a, double[] b)
        {
            var temp = new double[2, 3];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    temp[i, j] = a[i, j];
                }
            }

            temp[0, 2] = b[0];
            temp[1, 2] = b[1];

            temp[0, 2] /= temp[0, 0];
            temp[0, 1] /= temp[0, 0];
            temp[0, 0] = 1;

            temp[1, 1] -= temp[1, 0] * temp[0, 1];
            temp[1, 2] -= temp[1, 0] * temp[0, 2];
            temp[1, 0] = 0;

            temp[1, 2] /= temp[1, 1];
            temp[1, 1] = 1;

            var res = new double[2];
            res[1] = temp[1, 2];
            res[0] = temp[0, 2] - temp[0, 1] * res[1];

            return res;
        }

        public static double Norm(double[,] A)
        {
            var dim = A.GetLength(0);
            var max = 0.0;

            for (int i = 0; i < dim; i++)
            {
                var sum = 0.0;
                for (int j = 0; j < dim; j++)
                {
                    sum += Math.Abs(A[i, j]);
                }

                if (sum >= max)
                {
                    max = sum;
                }
            }

            return max;
        }

        public static double M(double[,] A)
        {
            return Norm(A) * Norm(Reverse(A));
        }
    }
}
