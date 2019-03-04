namespace Sem6
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Lab2
    {
        public static double[] Gauss(double[,] A, double[] b)
        {
            A = Tools.Table.Wide(A, b);
            var dim = A.GetLength(0);
            var mainRow = 0;
            var mainCol = 0;
            var visitedCols = new Stack<int>();
            var visitedRows = new Stack<int>();

            for (int i = 0; i < dim; i++)
            {
                var maxEl = MaxElement(
                    A,
                    ref mainRow,
                    ref mainCol,
                    ref visitedRows,
                    ref visitedCols);

                for (int j = 0; j < dim; j++)
                {
                    if (!visitedRows.Contains(j))
                    {
                        var factor = A[j, mainCol] / A[mainRow, mainCol];

                        for (int k = 0; k < dim + 1; k++)
                        {
                            A[j, k] -= factor * A[mainRow, k];
                        }

                        var sb = new StringBuilder();
                        Tools.Output.Print(
                            A,
                            "The main element: " +
                            "(" +
                            (mainRow + 1) +
                            " " +
                            (mainCol + 1) +
                            ")");
                    }
                }
            }

            return Express(A, visitedRows, visitedCols);
        }

        public static double MaxElement(
            double[,] A,
            ref int mainRow,
            ref int mainCol,
            ref Stack<int> visitedRows,
            ref Stack<int> visitedCols)
        {
            var dim = A.GetLength(0);
            var max = 0.0;

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (Math.Abs(A[i, j]) >= max &&
                        !visitedCols.Contains(j) &&
                        !visitedRows.Contains(i))
                    {
                        max = Math.Abs(A[i, j]);
                        mainRow = i;
                        mainCol = j;
                    }
                }
            }

            visitedRows.Push(mainRow);
            visitedCols.Push(mainCol);

            return max;
        }

        public static double[] Express(
            double[,] A,
            Stack<int> visitedRows,
            Stack<int> visitedCols)
        {
            var dim = A.GetLength(0);
            var res = new double[dim];

            for (int i = 0; i < dim; i++)
            {
                var actualRow = visitedRows.Pop();
                var actualCol = visitedCols.Pop();
                var factor = A[actualRow, actualCol];

                for (int j = 0; j < dim + 1; j++)
                {
                    A[actualRow, j] /= factor;
                }

                for (int j = 0; j < dim; j++)
                {
                    if (j != actualCol)
                    {
                        A[actualRow, dim] -= A[actualRow, j] * res[j];
                        A[actualRow, j] = 0;
                    }
                }

                res[actualCol] = A[actualRow, dim];
                Tools.Output.Print(A, "Expressing variables");
            }

            return res;
        }
    }
}


ffffff
