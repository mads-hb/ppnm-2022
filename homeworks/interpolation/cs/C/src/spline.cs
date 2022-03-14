public class NotImplementedException : System.Exception
{
    public NotImplementedException() : base() { }
    public NotImplementedException(string message) : base(message) { }
    public NotImplementedException(string message, System.Exception inner) : base(message, inner) { }
}


public abstract class Spline{

    public Spline(int n, double[] x, double[] y){
        this.n = n;
        this.x = x;
        this.y = y;
    }

    public int n { get; }
    public double[] x { get; }
    public double[] y { get; }


    protected int binsearch(double z){
        int i = 0;
        int j = n - 1;
        while(j - i > 1){
            int mid = (i + j) / 2;
            if(z > this.x[mid]){
                i = mid;
            } else {
                j = mid;
            }
        }
        return i;
    }


    abstract public double eval(double z);

    abstract public double integ(double z);

    abstract public double deriv(double z);
}