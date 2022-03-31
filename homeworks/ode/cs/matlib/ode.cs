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
     * Func<double,vector,vector> f the f from dy/dx=f(x,y)
        double a,                     the start-point a
        vector ya,                    y(a)
        double b,                     the end-point of the integration
        double h=0.01,                  initial step-size
        double acc=0.01,              absolute accuracy goal
        double eps=0.01               relative accuracy goal
     */
    public static (GenericList<double>, GenericList<vector>) 
        driver(Func<double, vector, vector> f, double a, vector y, double b, double h=0.01, double acc=0.01, double eps=0.01){
        // Initializing list that are returned
        var xs = new GenericList<double>();
        var ys = new GenericList<vector>();

        while(a < b){
            // Last step b leq a+h
            if(b <= a+h) 
                h = b-a;

            // Make a step with the rekstep12 routine
            (vector yh, vector err) = rkstep12(f, a, y, h);


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
        if(a>b) throw new Exception("driver: a>b");
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

