using static System.Console;
using System;
using static System.Math;

static class MainProgram{

    private static (double[], double[], double[]) read_numbers(){
        int length = 30;
        double[] x = new double[length], y = new double[length], dy = new double[length];
        using (var infile = new System.IO.StreamReader("higgs_data.txt")){
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

    private static double breit_wigner(double E, double m, double gamma, double A){
        return A / (Pow((E-m), 2) + (gamma*gamma) / 4 );
    }

    public static int Main(){
        WriteLine("------------------------------------------------");
        WriteLine("Testing the implementation to find the cross section of a Higgs decay:");

        (var x, var y, var dy) = read_numbers();

        Func<vector, double> loss = delegate(vector v){
            double m = v[0];
            double gamma = v[1];
            double A = v[2];
            double sum = 0;
            for(int i = 0; i < x.Length; i++){
                sum += Pow(breit_wigner(x[i], m, gamma, A) - y[i], 2)/Pow(dy[i], 2);
            }
            return sum;
        };

        vector x0 = new double[3] {120,2,6};
        vector res = Minimisation.QuasiNewton(loss, x0, eps:1e-4);
        WriteLine("This extremum is found at:");
        WriteLine($"m={res[0]:F4}, gamma={res[1]:F4}, A={res[2]:F4}.");

        
        WriteLine("------------------------------------------------");
        return 0;
    }
}