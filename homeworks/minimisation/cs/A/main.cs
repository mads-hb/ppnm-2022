using static System.Console;
using System;
using static System.Math;

static class MainProgram{

    public static int Main(){
        WriteLine("------------------------------------------------");
        WriteLine("Testing the implementation on a few examples:");

        WriteLine("\nFinding extremum of Rosenbrock's valley function f(x,y) = (1-x)^2 + 100(y-x^2)^2");
        Func<vector,double> f1 = x => Pow(1-x[0], 2) + 100 * Pow(x[1]-x[0]*x[0], 2);
        vector x01 = new double[2] {0,0};
        vector res1 = Minimisation.QuasiNewton(f1, x01);
        WriteLine("This extremum is found at:");
        WriteLine($"\tx = {res1[0]}, y = {res1[1]}");
        WriteLine("\tAnalytical result is (1,1)");


        WriteLine("\nFinding extremum of Himmelblau's function f(x,y) = f(x,y)=(x^2+y-11)^2 + (x+y^2-7)^2");
        WriteLine("This function has several local minima. Consider starting at x0={0,0}");
        Func<vector,double> f2 = x => Pow(x[0]*x[0]+x[1]-11, 2) + Pow(x[0]+x[1]*x[1]-7, 2);
        vector x02 = new double[2] {0,0};
        vector res2 = Minimisation.QuasiNewton(f2, x02);
        WriteLine("This extremum is found at:");
        WriteLine($"\tx = {res2[0]}, y = {res2[1]}");
        WriteLine("\tAnalytical result is (3,2)");

        WriteLine("Starting at x0={-2,3}");
        vector x03 = new double[2] {-2,3};
        vector res3 = Minimisation.QuasiNewton(f2, x03);
        WriteLine("This extremum is found at:");
        WriteLine($"\tx = {res3[0]}, y = {res3[1]}");
        WriteLine("\tAnalytical result is approx (-2.8, 3.1)");
        
        WriteLine("------------------------------------------------");
        return 0;
    }
}