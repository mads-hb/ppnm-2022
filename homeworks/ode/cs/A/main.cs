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
        vector y_stop;
        y_stop = ODE.driver_naive(pend, 0, y0, 10);
        y_stop.print("The solution for theta(10) and omega(10) is:");
        WriteLine("This agrees quite well with the figure shown on the scipy documentation page.");
        return 0;
    }
}