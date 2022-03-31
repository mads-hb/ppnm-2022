using static System.Console;
using System;
using System.Collections.Generic;
using static System.Math;

static class MainProgram{

    static int G = 1; // gravitational constant
    static int[] m = {1,1,1}; // masses of objets



    public static int Main(){

        Func<double, vector, vector> three_body = delegate(double t, vector y){
            double r1x = y[0];
            double r1y = y[1];
            double r1z = y[2];
            vector r1 = new vector(r1x, r1y, r1z);
            double v1x = y[3];
            double v1y = y[4];
            double v1z = y[5];
            vector v1 = new vector(v1x, v1y, v1z);

            // Define second object
            double r2x = y[6];
            double r2y = y[7];
            double r2z = y[8];
            vector r2 = new vector(r2x, r2y, r2z);
            double v2x = y[9];
            double v2y = y[10];
            double v2z = y[11];
            vector v2 = new vector(v2x, v2y, v2z);

            // define third object
            double r3x = y[12];
            double r3y = y[13];
            double r3z = y[14];
            vector r3 = new vector(r3x, r3y, r3z);
            double v3x = y[15];
            double v3y = y[16];
            double v3z = y[17];
            vector v3 = new vector(v3x, v3y, v3z);


            // Compute acceleration vectors.
            vector a1 = G * m[1] / Pow((r2 - r1).norm(), 2) * (r2 - r1) / (r2-r1).norm()
                + G * m[2] / Pow((r3 - r1).norm(), 2) * (r3 - r1) / (r3-r1).norm();

            vector a2 = G *  m[0] / Pow((r1 - r2).norm(), 2) * (r1 - r2) / (r1-r2).norm()
                + G * m[2] / Pow((r3 - r2).norm(), 2) * (r3 - r2) / (r3-r2).norm();

            vector a3 = G * m[0] / Pow((r1 - r3).norm(), 2) * (r1 - r3) / (r1-r3).norm()
                + G * m[1] / Pow((r2 - r3).norm(), 2) * (r2 - r3) / (r2-r3).norm();

            // Put all parameters into a new vector componentwise.
            double[] arr = {v1[0], v1[1], v1[2], a1[0], a1[1], a1[2],
                            v2[0], v2[1], v2[2], a2[0], a2[1], a2[2],
                            v3[0], v3[1], v3[2], a3[0], a3[1], a3[2]};

            vector dydt = new vector(arr);
            return dydt; 
        };
        
        
        double[] y0arr = {-0.97000436, 0.24308753, 0, 0.4662036850, 0.4323657300, 0,
                          0, 0, 0, -0.93240737, -0.86473146, 0,
                          0.97000436, -0.24308753, 0, 0.4662036850, 0.4323657300, 0};

        vector y0 = new vector(y0arr);
        GenericList<double> xlist;
        GenericList<vector> ylist;
        (xlist, ylist) = ODE.driver(three_body, 0, y0, 10);
        WriteLine("Using the parameters given in the wikipedia page I manage to create an " + 
            "animation showing how the three body system evolves over time. This can be seen " 
            + "in the animation three_body.gif.");

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