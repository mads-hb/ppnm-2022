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
        vector x = new vector(N);
        vector y = new vector(N);

        double delta = (b-a)*1.0/N;
        using (var writer = new System.IO.StreamWriter("tabulated.txt")){
            for(int i = 0; i < N; i++){
                double x0 = a + delta * i;
                x[i] = x0;
                y[i] = G(x0);
                writer.WriteLine($"{x[i]}\t{y[i]}");
            }
        }
        
        var nn = new NeuralNetwork(5, s => s*Exp(-s*s));
        nn.train(x, y);
        using (var writer = new System.IO.StreamWriter("fit.txt")){
            for (double i = a; i < b; i += 0.01){
                writer.WriteLine($"{i}\t{nn.response(i)}");
            }
        }
        
        WriteLine("------------------------------------------------");
        return 0;
    }
}