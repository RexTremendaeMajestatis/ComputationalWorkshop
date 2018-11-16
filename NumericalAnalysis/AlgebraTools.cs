using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalAnalysis
{
    public static class AlgebraTools
    {
        public static double Determinant(double [,] matrix)
        {
            return (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
        }

        public static double [] Cramer(double [,] matrix, double[] vector)
        {
            var det = Determinant(matrix);

            var xmatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            var ymatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];

            var res = new double[vector.Length];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                xmatrix[i, 0] = vector[i];
                xmatrix[i, 1] = matrix[i, 1];

                ymatrix[i, 1] = vector[i];
                ymatrix[i, 0] = matrix[i, 0];
            }

            var xdet = Determinant(xmatrix);
            var ydet = Determinant(ymatrix);

            res[0] = xdet / det;
            res[1] = ydet / det;

            return res;
        }

        public static double Gorner(double x, double[] coefficients)
        {
            var n = coefficients.Length;
            var res = new double[n];
            Array.Reverse(coefficients);
            res[0] = coefficients[0];

            for (int i = 1; i < n; i++)
            {
                res[i] = coefficients[i] + (res[i - 1] * x);
            }

            return res[n - 1];
        }

        public static double[] SolveSquare(double[] c)
        {
            var D = c[1] * c[1] - 4 * c[0] * c[2];
            var res = new double[2];

            res[0] = (-c[1] + Math.Sqrt(D)) / (2 * c[0]);
            res[1] = (-c[1] - Math.Sqrt(D)) / (2 * c[0]);

            return res;
        }
    }
}
