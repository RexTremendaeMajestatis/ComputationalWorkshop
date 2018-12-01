using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalAnalysis
{
    public static class Function
    {
        public delegate double F(double x);

        public delegate double G(double y, double x);
    }
}
