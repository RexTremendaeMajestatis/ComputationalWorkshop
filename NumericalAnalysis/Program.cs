namespace NumericalAnalysis
{
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            const int dim = 4;
            var A = new double[dim, dim] { { 0.3, 3.1, 4.4, 2.5 }, 
                                           { -3.2, 0.5, 7.2, 7.9 }, 
                                           { 0.0, 3.9, 6.4, -1.7 }, 
                                           { 0.0, 0.0, -2.9, -4.2 } };
            var b = new double[dim] { 4, -13, 1, -5 };
            OutputTools.Print(A);
            OutputTools.Print(b);
            OutputTools.Print(TableTools.Wide(A, b));
            OutputTools.Print(NumericalAnalysis._6sem.Lab2.Gauss(A, b));

            Console.ReadLine();
            Main(args);
        }
    }
}
