public class LinearInterpolation: Spline{
    public LinearInterpolation(int n, double[] x, double[] y) : base(n, x, y){}


    override public double eval(double z){
        int i = binsearch(z);
        double dy = y[i+1] - y[i];
        double dx = x[i+1] - x[i]; 
        return y[i] + dy/dx * (z - x[i]);
    }

    override public double integ(double z, double c = 0){
        int i = 0;
        double integral = 0;
        double dy, dx, p;
        while (z > x[i+1]){
            dy = y[i+1] - y[i];
            dx = x[i+1] - x[i];
            integral += y[i]*dx + 0.5 * dy * dx;
            i++;
        }
        p = (y[i+1] - y[i]) / (x[i+1] - x[i]);
        dx = z-x[i];
        integral += y[i] * dx + 0.5 * p * System.Math.Pow(dx, 2) + c;
        return integral;
    }

    override public double deriv(double z){
        throw new NotImplementedException("Derivative undefined for linear spline.");
    }
}