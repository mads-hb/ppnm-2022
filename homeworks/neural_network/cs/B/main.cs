using static System.Console;
using System;
using static System.Math;

static class MainProgram{

    private static double G(double x){
        return Cos(5*x-1)*Exp(-x*x);
    }

    public static int Main(){
        WriteLine("------------------------------------------------");
        WriteLine("Testing the NeuralNetwork implementation on the function:");
        WriteLine("Cos(5*x-1)*Exp(-x*x). Plot is shown in file exerciseAplot.png");
        double a = -1;
        double b = 1;
        int N = 20;
        vector t = new vector(N);
        vector y = new vector(N);

        double delta = (b-a)*1.0/N;
        using (var writer = new System.IO.StreamWriter("tabulated.txt")){
            for(int i = 0; i < N; i++){
                double t0 = a + delta * i;
                t[i] = t0;
                y[i] = G(t0);
                writer.WriteLine($"{t[i]}\t{y[i]}");
            }
        }
        
        Func<double, double> activation = (x) => x*Exp(-x*x);
        Func<double, double> activation_int = (x) => -Exp(-x*x)/2;
        Func<double, double> activation_diff = (x) => (1 - 2*x*x)*Exp(-x*x);

        var nn = new NeuralNetworkCalculus(5, activation, activation_int, activation_diff);
        nn.train(t, y);
        using (var writer = new System.IO.StreamWriter("fit.txt")){
            for (double i = a; i < b; i += 0.01){
                writer.WriteLine($"{i}\t{nn.response(i)}\t{nn.response_integral(i)}\t{nn.response_derivative(i)}");
            }
        }
        
        WriteLine("------------------------------------------------");
        return 0;
    }
}