using static integrate;
using System;

public static class MainProgram{
    private static double err(double x){
        Func<double, double> f = delegate(double t) {return Math.Exp(-Math.Pow(t, 2));};
        return 2.0/Math.Sqrt(Math.PI) * quad(f, 0, x);
    }

    public static int Main(){
        Func<double, double> f = delegate(double x){return Math.Log(x)/Math.Sqrt(x);};

        double res = quad(f, 0, 1);
        Console.WriteLine($"The integral of Log(x)/Sqrt(x) from 0 to 1 is {res}. The exact value is 4.");
        
        using (var writer = new System.IO.StreamWriter("err.txt")){
            for (double x = -3; x<3; x+=0.01){
                writer.WriteLine($"{x}\t{err(x)}");
            }
        }
        return 0;
    }
}
