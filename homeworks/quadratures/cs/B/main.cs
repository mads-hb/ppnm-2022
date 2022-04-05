using static System.Console;
using System;
using static System.Math;

static class MainProgram{

    public static int Main(){
        WriteLine("Let's solve some test integrals and check how many recursive calls are needed.");
        Func<double, double> f1 = delegate(double x){return 1/Sqrt(x);};
        var int1 = new Integral(0, 1 , f1, "1/Sqrt(x)");

        Func<double, double> f2 = delegate(double x){return Log(x)/Sqrt(x);};
        var int2 = new Integral(0, 1 , f2, "Log(x)/Sqrt(x)");
        
        Integral[] ints = {int1, int2};
        foreach(var _int in ints) {
            WriteLine("Using normal integration");
            double res1 = Integrator.integrate(_int, print_calls:true);
            WriteLine($"The integral {_int.ToString()} equals {res1}.");
            WriteLine();

            WriteLine("Using Clenshaw-Curtis integration");
            double res2 = Integrator.clenshaw_curtis(_int, print_calls:true);
            WriteLine($"The integral {_int.ToString()} equals {res2}.");
            WriteLine();
        }

        return 0;
    }
}