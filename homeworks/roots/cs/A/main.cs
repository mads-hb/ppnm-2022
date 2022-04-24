using static System.Console;
using System;
using static System.Math;

static class MainProgram{

    public static int Main(){
        WriteLine("------------------------------------------------");
        WriteLine("Testing the implementation on a few examples:");


        Func<vector,vector> f1 = x => new vector(2*x[0]*Log(x[0])+x[0]);
        vector x01 = new double[1] {0.5};
        vector res1 = RootFinding.Newton(f1, x01);
        WriteLine("\nFinding extremum of f(x) = log(x)*x^2");
        WriteLine("This is done by searching for roots of its gradient f'(x) = 2*x*log(x) + x");
        WriteLine("This extremum is found at:");
        WriteLine($"\tx = {res1[0]}");
        WriteLine("\tAnalytical result is 0.6065");

        Func<vector,vector> f2 = x => new vector(2*(200*Pow(x[0],3) - 200*x[0]*x[1] + x[0] - 1), 200*(x[1] - Pow(x[0],2)));
        vector x02 = new double[2] {0,0};
        vector res2 = RootFinding.Newton(f2, x02);
        WriteLine("\nFinding extremum of Rosenbrock's valley function f(x,y) = (1-x)^2 + 100(y-x^2)^2");
        WriteLine("This is done by searching for roots of its gradient f'(x,y) = [2*(200*x^3 - 200*x*y + x - 1), 200*(y - x^2)]");
        WriteLine("This extremum is found at:");
        WriteLine($"\tx = {res2[0]}, y = {res2[1]}");
        WriteLine("\tAnalytical result is (1,1)");
        
        WriteLine("------------------------------------------------");
        return 0;
    }
}