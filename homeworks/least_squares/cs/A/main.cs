using System;
using static System.Console;
using System.Diagnostics;
using static System.Math;


public static class MainProgram{

    static (double[], double[], double[]) read_numbers(){
        int length = 9;
        double[] x = new double[length], y = new double[length], dy = new double[length];
        using (var infile = new System.IO.StreamReader("data.txt")){
            string s;
            double xi, yi, dyi;
            for (int i = 0; i < length; ++i){
                s = infile.ReadLine();
                string[] subs = s.Split(' ');
                xi = double.Parse(subs[0]);
                yi = double.Parse(subs[1]);
                dyi = double.Parse(subs[2]);
                x[i] = xi; 
                y[i] = yi;
                dy[i] = dyi;
            }
        }
        return (x, y, dy);
    }

    public static void ExerciseA(){
        double[] x, y, dy;
        (x, y, dy) = read_numbers();
        for (int i = 0; i < y.Length; i++){
            dy[i] = dy[i]/y[i];
            y[i] = Log(y[i]);
        }
        double[][] data = {x, y, dy};
        var fs = new Func<double,double>[] { z => 1.0, z => z};
        var coefs = LeastSquares.fit(data, fs);
        using (var outfile = new System.IO.StreamWriter("fit.txt")){
            for (double value = 1; value < 16; value += 0.1){
                double res = 0;
                for (int k = 0; k < fs.Length; k++){
                    res += coefs[k] * fs[k](value);
                }
                outfile.WriteLine($"{value} {Exp(res)}");
            }
        }
        WriteLine($"The lambda parameter is estimated to be {-coefs[1]}.");
        WriteLine($"The half-life is then {Log(2)/-coefs[1]} days.");
        WriteLine("The modern value is 3.6319 days.");
    }


    public static int Main(){
        WriteLine("--------------------- Solving exercise A: ---------------------");
        ExerciseA();
        return 0;
    }
}