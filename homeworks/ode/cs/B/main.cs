using static System.Console;
using System;
using System.Collections.Generic;

static class MainProgram{

    public static int Main(){
        double b = 0.25;
        double c = 5;
        Func<double, vector, vector> pend = delegate(double t, vector y){
            double theta = y[0];
            double omega = y[1];
            double[] arr = {omega, -b*omega - c*Math.Sin(theta)};
            vector dydt = new vector(arr);
            return dydt; 
        };
        
        
        double[] y0arr = {Math.PI-0.1, 0.0};
        vector y0 = new vector(y0arr);
        GenericList<double> xlist;
        GenericList<vector> ylist;

        (xlist, ylist) = ODE.driver(pend, 0, y0, 10);
        WriteLine("The system has been integrated with the updated driver and a resulting plot is seen in the figure out.png." +
            "This agrees quite well with the figure shown on the scipy documentation page.");
        using (var writer = new System.IO.StreamWriter("diff.txt")){
            for (int i = 0; i < xlist.size; i++){
                writer.Write($"{xlist.get(i)} \t");
                for (int k=0; k < ylist.get(i).size; k++){
                    writer.Write($"{ylist.get(i)[k]}\t");
                }
                writer.Write("\n");
            }
        }
        return 0;
    }
}