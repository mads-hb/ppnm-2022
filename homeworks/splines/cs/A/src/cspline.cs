using static System.Math;

public class CubicInterpolation: Spline{
    protected double[] c;
    protected double[] b;
    protected double[] d;


    public CubicInterpolation(int n, double[] x, double[] y) : base(n, x, y){
        b = new double[n];
        c = new double[n];
        d = new double[n];

        for(int i=0; i<n; i++){
            x[i] = x[i];
            y[i] = y[i];
        }

        double[] h = new double[n-1]; 
        double[] p = new double[n-1];//VLA
        for(int i=0; i<n-1; i++){
            h[i] = x[i+1] - x[i];
        }
        for(int i=0; i<n-1; i++){
            p[i] = (y[i+1] - y[i]) / h[i];
        }

        double[] D = new double[n]; 
        double[] B = new double[n];
        double[] Q = new double[n-1]; //buildingthetridiagonalsystem:
        D[0] = 2;
        for(int i=0; i<n-2; i++){
            D[i+1] = 2 * h[i] / h[i+1] + 2;
            D[n-1] = 2;
        }
        Q[0] = 1;
        for(int i=0; i<n-2; i++){
            Q[i+1] = h[i] / h[i+1];
        }
        for(int i=0; i<n-2; i++){
            B[i+1] = 3 * (p[i] + p[i+1] * h[i] / h[i+1]);
        }
        B[0] = 3 * p[0];
        B[n-1] = 3 * p[n-2];//Gausselimination:
        for(int i=1; i<n; i++){
            D[i] -= Q[i-1] / D[i-1];
            B[i] -= B[i-1] / D[i-1];
        }
        b[n-1] = B[n-1] / D[n-1];//back-substitution:
        for(int i=n-2; i>=0; i--){
            b[i] = (B[i] - Q[i] * b[i+1]) / D[i];
        }
        for(int i=0; i<n-1; i++){
            c[i] = (-2 * b[i] - b[i+1] + 3 * p[i]) / h[i];
            d[i]=(b[i] + b[i+1] - 2 * p[i]) / h[i] / h[i];
        }
    }


    override public double eval(double z){
        int i = binsearch(z);
        double h = z-x[i];//calculatetheinerpolatingspline:
        double val = y[i] + h * (b[i] + h * (c[i] + h * d[i]));
        return val;
    }

    override public double integ(double z, double constant = 0){
        int j = binsearch(z);
        double integral = 0;
        double h, yi, bi, ci, di;
        for(int i = 0; i < j; i++){
            h = x[i+1] - x[i];
            yi = y[i];
            bi = b[i];
            ci = c[i];
            di = d[i];
            integral += yi * h + 0.5 * bi * Pow(h,2) + 1.0/3 * ci * Pow(h,3) + 1.0/4 * di * Pow(h,4);
        }
        h = z - x[j];
        yi = y[j];
        bi = b[j];
        ci = c[j];
        di = d[j];   
        integral += yi * h + 0.5 * bi * Pow(h,2) + 1.0/3 * ci * Pow(h,3) + 1.0/4 * di * Pow(h,4);
        return integral;
    }

    override public double deriv(double z){
        int i = binsearch(n);
        double h = z-x[i];
        double bi = b[i];
        double ci = c[i];
        double di = d[i];
        double val = bi + 2*ci*h + 3 * di * Pow(h, 2);

        return val;
    }
}