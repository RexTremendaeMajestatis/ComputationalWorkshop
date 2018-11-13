using System;
namespace NumericalAnalysis
{
    public static class lab5
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
            var temp = a;
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
                temp += h;
            }

            return h * sum;
        }


    }
}
