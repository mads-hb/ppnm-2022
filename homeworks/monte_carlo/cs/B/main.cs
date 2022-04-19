using static System.Console;
using System;
using static System.Math;

static class MainProgram{

    public static int Main(){
        WriteLine("Integrating x*y on a 0-1 square.");
        Func<vector,double> f1 = x => x[0]*x[1];
        vector a1 = new double[2] {0,0};
        vector b1 = new double[2] {1,1};
        (var res1, var err1) = MonteCarlo.quasi(f1, a1, b1, 10000);
        WriteLine($"Result is {res1} and error is {err1}");
        WriteLine("Expected result is 0.25");
        WriteLine();

        WriteLine("Integrating x*y on a -1 - 1 square.");
        Func<vector,double> f2 = x => x[0]*x[1];
        vector a2 = new double[2] {-1,-1};
        vector b2 = new double[2] {1,1};
        (var res2, var err2) = MonteCarlo.quasi(f2, a2, b2, 1000000);
        WriteLine($"Result is {res2} and error is {err2}");
        WriteLine("Expected result is 0");
        WriteLine();

        WriteLine("Integrating a ∫_0^π  dx/π ∫_0^π  dy/π ∫_0^π  dz/π [1-cos(x)cos(y)cos(z)]^(-1)");
        Func<vector,double> f3 = x => 1/((1 - Cos(x[0])*Cos(x[1])*Cos(x[2]))*Pow(PI,3));
        vector a3 = new double[3] {0,0, 0};
        vector b3 = new double[3] {PI, PI, PI};
        (var res3, var err3) = MonteCarlo.quasi(f3, a3, b3, 1000000);
        WriteLine($"Result is {res3} and error is {err3}");
        WriteLine("Expected result is 1.3932039296856768591842462603255");
        WriteLine();

        WriteLine($"Lets check how the error scales with the number of iterations");
        using (var outstream = new System.IO.StreamWriter("err.txt")){
            for (int N = 10; N < 1e6; N += 10000){
                (var q_res, var q_err) = MonteCarlo.quasi(f3, a3, b3, N);
                (var p_res, var p_err) = MonteCarlo.pseudo(f3, a3, b3, N);
                outstream.WriteLine($"{N}\t{p_err}\t{q_err}");
            }
        }
        return 0;
    }
}