using System;
namespace NumericalAnalysis
{
    public static class Lab5
    {
        public delegate double Function(double x);

        public enum Part
        {
            Left,
            Middle,
            Right
        }

        public static double Rectangle(
            double a,
            double b,
            int m,
            Function f,
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

        public static double Trapeze(double a, double b, int m, Function f)
        {
            var h = (b - a) / m;
            var sum = f(a) + f(b);

            for (int i = 1; i < m; i++)
            {
                sum += 2 * f(a + (i * h));
            }

            return (h / 2) * sum;
        }

        public static double Simpson(double a, double b, int m, Function f)
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
