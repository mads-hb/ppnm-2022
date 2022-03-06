using static ode;
using System;
using System.Collections.Generic;

public static class MainProgram{

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
        var xlist = new List<double>();
        var ylist = new List<vector>();
        vector y_stop = ode.ivp(pend, 0, ref y0, 10, xlist, ylist);
        
        
        using (var writer = new System.IO.StreamWriter("diff.txt")){
            for (int i = 0; i < xlist.Count; i++){
                writer.WriteLine($"{xlist[i]}\t{ylist[i][0]}\t{ylist[i][1]}");
            }
        }
        
        return 0;
    }
}
