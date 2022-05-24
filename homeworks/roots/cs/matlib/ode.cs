using System;
using static System.Math;
using System.Collections.Generic;


public static class ODE {

    /**
     * Func<double,vector,vector> the f from dy/dx=f(x,y)
     *   double x,  the current value of the variable
     *   vector y,  the current value y(x) of the sought function
     *   double h   the step to be taken 
     */
    public static (vector,vector) rkstep12(Func<double,vector,vector> f, double x, vector y, double h){ // Runge-Kutta Euler/Midpoint method (probably not the most effective)
        vector k0 = f(x,y); /* embedded lower order formula (Euler) */
        vector k1 = f(x+h/2,y+k0*(h/2)); /* higher order formula (midpoint) */
        vector yh = y+k1*h;     /* y(x+h) estimate */
        vector er = (k1-k0)*h;  /* error estimate */
        return (yh,er);
    }

    /**
     * Func<double,vector,vector> the f from dy/dx=f(x,y)
     *   double x,  the current value of the variable
     *   vector y,  the current value y(x) of the sought function
     *   double h   the step to be taken 
     */
    public static (vector,vector) rkstep45(Func<double,vector,vector> f, double x, vector y, double h){
        // Define the tableau values
        double c2 = 1.0/4;
        double c3 = 3.0/8;
        double c4 = 12.0/13;
        double c5 = 1.0;
        double c6 = 1.0/2;

        double a21 = 1.0/4;
        double a31 = 3.0/32;
        double a41 = 1932.0/2197;
        double a51 = 439.0/216;
        double a61 = -8.0/27;
        double a32 = 9.0/32;
        double a42 = -7200.0/2197;
        double a52 = -8;
        double a62 = 2;
        double a43 = 7296.0/2197;
        double a53 = 3680.0/513;
        double a63 = -3544.0/2565;
        double a54 = -845.0/4104;
        double a64 = 1859.0/4104;
        double a65 = -11.0/40;

        double b1 = 16.0/135;
        double b2 = 0;
        double b3 = 6656.0/12825;
        double b4 = 28561.0/56430;
        double b5 = -9.0/50;
        double b6 = 2.0/55;
    
        double b1s = 25.0/216;
        double b2s = 0;
        double b3s = 1408.0/2565;
        double b4s = 2197.0/4104;
        double b5s = -1.0/5;
        double b6s = 0;

        // Compute RK vectors from eqn. 10
        vector K1 = h * f(x, y);
        vector K2 = h * f(x + c2 * h, y + a21 * K1);
        vector K3 = h * f(x + c3 * h, y + a31 * K1 + a32 * K2);
        vector K4 = h * f(x + c4 * h, y + a41 * K1 + a42 * K2 + a43 * K3);
        vector K5 = h * f(x + c5 * h, y + a51 * K1 + a52 * K2 + a53 * K3 + a54 * K4);
        vector K6 = h * f(x + c6 * h, y + a61 * K1 + a62 * K2 + a63 * K3 + a64 * K4 + a65 * K5);

        // Estimate local error
        vector y_n = y + b1*K1 + b2*K2 + b3*K3 + b4*K4 + b5*K5 + b6*K6;  // eqn. 18
        vector y_np = y + b1s*K1 + b2s*K2 + b3s*K3 + b4s*K4 + b5s*K5 + b6s*K6;  // eqn. 22
        vector err = y_n - y_np;
        return (y_n,err);
    }

    /**
     * Func<double,vector,vector> f the f from dy/dx=f(x,y)
        double a,                     the start-point a
        vector ya,                    y(a)
        double b,                     the end-point of the integration
        double h=0.01,                  initial step-size
        double acc=0.01,              absolute accuracy goal
        double eps=0.01               relative accuracy goal
     */
    public static (GenericList<double>, GenericList<vector>) 
        driver(Func<double, vector, vector> f, double a, vector y, double b, double h=0.01, double acc=1e-8, double eps=1e-8){
        // Initializing list that are returned
        var xs = new GenericList<double>();
        var ys = new GenericList<vector>();

        while(a < b){
            // Last step b leq a+h
            if(b <= a+h) 
                h = b-a;

            // Make a step with the rekstep12 routine
            (vector yh, vector err) = rkstep45(f, a, y, h);


            vector tol = new vector(yh.size);
            for (int i = 0; i < yh.size; i++){
                tol[i] = Max(acc, Abs(yh[i]) * eps) * Sqrt(h / (b-a));
            }

            bool ok = true;
            for(int j=0;j<tol.size;j++) {
                ok = (ok && err[j]<tol[j]);
            }
            if (ok){
                a+=h; 
                y=yh;
                xs.push(a); 
                ys.push(y);
            }
            double factor = tol[0]/Abs(err[0]);
            for(int j=1;j<tol.size;j++) {
                factor = Min(factor,tol[j]/Abs(err[j]));
            }
            h *= Min( Pow(factor, 0.25) * 0.95, 2);  // reajust stepsize
            
        }

        return (xs, ys);
    }


    public static vector 
        driver_naive(Func<double,vector,vector> f, double a, vector ya, double b, double h=0.01, double acc=0.01, double eps=0.01){
        // if(a>b) throw new Exception("driver: a>b");
        double x=a; vector y=ya;
        do {
            if(x>=b) return y; /* job done */
            if(x+h>b) h=b-x;   /* last step should end at b */
            (vector yh, vector erv) = rkstep12(f,x,y,h);
            double tol = Max(acc,yh.norm()*eps) * Sqrt(h/(b-a));
            double err = erv.norm();
            if(err<=tol){ x+=h; y=yh; } // accept step
            h *= Min( Pow(tol/err,0.25)*0.95 , 2); // reajust stepsize
        }while(true);
    }

}

