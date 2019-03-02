namespace Sem5
{
    /// <summary>
    /// Laboratory work #2
    /// </summary>
    public static class Lab2
    {
        /// <summary>
        /// Get value of Lagrange interpolation polynome
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="n">Degree of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Value of interpolation polynome image</returns>
        public static double Lagrange(double x, int n, ref double[,] table)
        {
            var sorted = Tools.TableTools.SortTable(x, ref table);
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

        /// <summary>
        /// Get value of Newton interpolation polynome
        /// </summary>
        /// <param name="x">Value of preimage</param>
        /// <param name="n">Degree of interpolation polynome</param>
        /// <param name="table">Table set function</param>
        /// <returns>Value of interpolation polynome image</returns>
        public static double Newton(double x, int n, ref double[,] table)
        {
            var sorted = Tools.TableTools.SortTable(x, ref table);
            var ftable = Tools.TableTools.DividedDifferences(sorted, n);

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
