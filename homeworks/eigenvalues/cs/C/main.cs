using static System.Console;
using static System.Math;
using System.Diagnostics;
using System;


static class MainProgram{

    static int MAX = 300;

    public static int Main(){
        time_diag(false);
        time_diag(true);
        return 0;
    }

    public static void time_diag(bool optimize){
        double[] x, y, dy;

        
        x = new double[MAX];
        y = new double[MAX];
        dy = new double[MAX];

        using (var outfile = new System.IO.StreamWriter($"timing_opt_{optimize}.txt")){
            for(int i=1; i <= MAX; i++){
                int n = i;
                var A = matrix.random_symmetric(n);
                var V = new matrix(n,n);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Jacobi.diag(A, V, optimize: optimize);

                stopwatch.Stop();
                double timing = stopwatch.ElapsedMilliseconds / 1000.0;
                x[i-1] = i;
                y[i-1] = timing;
                dy[i-1] = 1e5;
                outfile.WriteLine($"{i}\t{timing}");
            }
        }

        // Use leats squares fit from last week:
        double[][] data = {x, y, dy};
        var fs = new Func<double,double>[] { z => 1, z => z, z => z*z, z => z*z*z};
        
        double[] coefs;
        matrix covb;

        (coefs, covb) = LeastSquares.fit_covariance(data, fs);
        using (var outfile = new System.IO.StreamWriter($"fit_opt_{optimize}.txt")){
            for (double value = 1; value < MAX; value += 0.1){
                double res = 0;
                for (int k = 0; k < fs.Length; k++){
                    res += coefs[k] * fs[k](value);
                }
                outfile.WriteLine($"{value} {res}");
            }
        }
    }

}