using System;
namespace NumericalAnalysis
{
    public static class Lab5
    {
        /// <summary>
        /// The kind of rectangles
        /// </summary>
        public enum Part
        {
            Left,
            Middle,
            Right
        }

        /// <summary>
        /// The way of approximate integrating
        /// </summary>
        /// <returns>The value of integral from <paramref name="a"/> to <paramref name="b"/></returns>
        /// <param name="a">The beginning of the segment</param>
        /// <param name="b">The ending of the segment</param>
        /// <param name="m">Amount of gaps</param>
        /// <param name="f">The function</param>
        /// <param name="part">Kind of rectangles</param>
        public static double Rectangle(
            double a,
            double b,
            int m,
            Function.F f,
            Part part)
        {
            var h = (b - a) / m;
            var sum = 0.0;
            var alpha = 0.0;

            switch(part)
            {
                case Part.Left:
                    alpha = a;
                    break;
                case Part.Middle:
                    alpha = a + (h / 2);
                    break;
                case Part.Right:
                    alpha = a + h;
                    break;
                default:
                    alpha = a + (h / 2);
                    break;
            }

            for (int i = 0; i < m; i++)
            {
                sum += f(alpha + (i * h));
            }

            return h * sum;
        }

        /// <summary>
        /// The way of approximate integrating
        /// </summary>
        /// <returns>The value of integral from <paramref name="a"/> to <paramref name="b"/></returns>
        /// <param name="a">The beginning of the segment</param>
        /// <param name="b">The ending of the segment</param>
        /// <param name="m">Amount of gaps</param>
        /// <param name="f">The function</param>
        public static double Trapeze(double a, double b, int m, Function.F f)
        {
            var h = (b - a) / m;
            var sum = f(a) + f(b);

            for (int i = 1; i < m; i++)
            {
                sum += 2 * f(a + (i * h));
            }

            return (h / 2) * sum;
        }

        /// <summary>
        /// The way of approximate integrating
        /// </summary>
        /// <returns>The value of integral from <paramref name="a"/> to <paramref name="b"/></returns>
        /// <param name="a">The beginning of the segment</param>
        /// <param name="b">The ending of the segment</param>
        /// <param name="m">Amount of gaps</param>
        /// <param name="f">The function</param>
        public static double Simpson(double a, double b, int m, Function.F f)
        {
            var h = (b - a) / (2 * m);
            var sum = f(a) + f(b);

            for (int i = 1; i < 2 * m; i++)
            {
                if (i % 2 == 0)
                {
                    sum += 2 * f(a + (i * h));
                }
                else
                {
                    sum += 4 * f(a + (i * h));
                }
            }

            return (h / 3) * sum;
        }
    }
}
