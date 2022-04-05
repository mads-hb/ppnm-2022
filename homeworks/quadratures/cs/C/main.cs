using static System.Console;
using static System.Double;
using System;
using static System.Math;

static class MainProgram{

    public static int Main(){
        WriteLine("Let's solve some test integrals and check how many recursive calls are needed.");
        Func<double, double> f1 = delegate(double x){return 1/(x*x);};
        var int1 = new Integral(1, double.PositiveInfinity , f1, "1/x^2");
        
        Func<double, double> f2 = delegate(double x){return x*Exp(-x*x);};
        var int2 = new Integral(double.NegativeInfinity, double.PositiveInfinity, f2, "x*Exp(-x*x)");
        
        Integral[] ints = {int1, int2};
        foreach(var _int in ints) {
            WriteLine($"Using normal integration: {_int.ToString()}.");
            double res1 = Integrator.integrate(_int, print_calls:true);
            WriteLine($"The integral {_int.ToString()} equals {res1}.");
            WriteLine();
        }

        return 0;
    }
}