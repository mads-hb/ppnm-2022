using static System.Console;
using System;
using static System.Math;

static class MainProgram{

    public static int Main(){
        WriteLine("Let's solve some test integrals");
        Func<double, double> f1 = delegate(double x){return Sqrt(x);};
        var int1 = new Integral(0, 1 , f1, "Sqrt(x)");

        Func<double, double> f2 = delegate(double x){return 1/Sqrt(x);};
        var int2 = new Integral(0, 1 , f2, "1/Sqrt(x)");

        Func<double, double> f3 = delegate(double x){return 4*Sqrt(1-x*x);};
        var int3 = new Integral(0, 1 , f3, "4*Sqrt(1-x*x)");

        Func<double, double> f4 = delegate(double x){return Log(x)/Sqrt(x);};
        var int4 = new Integral(0, 1 , f4, "Log(x)/Sqrt(x)");
        
        Integral[] ints = {int1, int2, int3, int4};
        foreach(var _int in ints) {
            double res = Integrator.integrate(_int);
            WriteLine($"The integral {_int.ToString()} equals {res}.");
        }


        Func<double, double> errfunc = delegate(double z){
            Func<double, double> inner = delegate(double x){return 2/Sqrt(PI) * Exp(-x*x);};
            return Integrator.integrate(inner, 0, z);
        };

        using (var writer = new System.IO.StreamWriter("err.txt")){
            for (double x = -3; x<3; x+=0.01){
                writer.WriteLine($"{x}\t{errfunc(x)}");
            }
        }

        return 0;
    }
}