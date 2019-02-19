namespace NumericalAnalysis
{
    using System;

    public static class Program
    {
        


        public static void Main(string[] args)
        {
            const int dim = 2;
            var A = new double[dim, dim] { { -401.43, 200.19 }, { 1201.14, -601.62 } };
            var b = new double[dim] { 200, -600 };
            var x = NumericalAnalysis._6sem.Lab1.Kramer(A, b);
            Console.WriteLine("{0}; {1}", x[0], x[1]);
            OutputTools.Print(NumericalAnalysis._6sem.Lab1.Reverse(A));
            Console.WriteLine(NumericalAnalysis._6sem.Lab1.M(A));
            
            Console.ReadLine();
            Main(args);
        }
    }
}
