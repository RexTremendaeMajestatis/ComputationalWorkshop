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
    }
}
