using static System.Math;

public class QuadraticInterpolation: Spline{
    protected double[] c;
    protected double[] b;
    public QuadraticInterpolation(int n, double[] x, double[] y) : base(n, x, y){
        b = new double[n];
        c = new double[n];

        int i;  
        for (i=0; i<n; i++){
            x[i] = x[i];
            y[i] = y[i];
        }
        double[] p = new double[n-1];
        double[] h = new double[n-1];//VLAfromC99

        for (i=0; i<n-1; i++){
            h[i] = x[i+1] - x[i];
            p[i] = (y[i+1] - y[i]) / h[i];
        }
        c[0] = 0; //recursionup:
        for (i=0; i<n-2; i++){
            c[i+1] = (p[i+1] - p[i] - c[i] * h[i]) / h[i+1];
        }
        c[n-2] /= 2;//recursiondown:
        for (i=n-3; i>=0; i--){
            c[i] = (p[i+1] - p[i] - c[i+1] * h[i+1]) / h[i];
        }
        for(i=0; i<n-1; i++){
            b[i] = p[i] - c[i] * h[i];
        }
    }


    override public double eval(double z){
        int i = binsearch(z);
        double h = z - x[i];
        double yi = y[i];
        double bi = b[i];
        double ci = c[i];
        double val = yi + h * (bi + h * ci);
        return val;
    }

    override public double integ(double z){
        int j = binsearch(z);
        double integral = 0;
        double h, yi, bi, ci;
        for (int i = 0; i < j; ++i){
            h = x[i+1] - x[i];
            yi = y[i];
            bi = b[i];
            ci = c[i];
            integral += yi * h + 0.5 * bi * Pow(h,2) + 1.0/3 * ci * Pow(h,3);
        }
        h = z - x[j];
        yi = y[j];
        bi = b[j];
        ci = c[j];
        integral += yi * h + 0.5 * bi * Pow(h,2) + 1.0/3 * ci * Pow(h,3);
        return integral;
    }

    override public double deriv(double z){
        int i = binsearch(z);
        double h = z - x[i];
        double bi = b[i];
        double ci = c[i];
        double val = bi + 2*ci*h;
        return val;
    }
}