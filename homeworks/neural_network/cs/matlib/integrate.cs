using System;
using static System.Double;
using static System.Math;


public class Integral {
    private double _a;
    private double _b;
    private Func<double, double> _f;
    private string _f_str;


    public double a { get{return _a;} }

    public double b { get{return _b;} }

    public Func<double, double> f { get{return _f;} }

    public string f_str { get{return _f_str;} }

    public Integral(double a, double b, Func<double, double> f, string f_str){
        _a = a;
        _b = b;
        _f = f;
        _f_str = f_str;
    }

    public override string ToString(){
        return $"Integral<[{a}, {b}] ({_f_str})>";
    }
}


public static class Integrator {

    private static int rec_calls = 0;


    /** Recursively compute the integral of a function on the interval [a,b].
     * @param Func<double,double> f the function to integrate.
     * @param double a is the starting point of the integration region.
     * @param double b is the ending point of the integration region.
     * @param double delta=0.001 is the absolute accuracy goal.
     * @param double epsilon=0.001 is the relative accuracy goal.
     * @param bool print_calls=false print the number of recursive calls after integrating.
     **/
    public static double integrate(Func<double, double> f, double a, double b, double delta=0.001, double epsilon=0.001, bool print_calls=false){
        var _int = new Integral(a, b, f, "");
        return integrate(_int, delta:delta, epsilon:epsilon, print_calls:print_calls);
    }

    /** Recursively compute the integral of a function on the interval [a,b].
     * @param Integral i is the integral to do.
     * @param double delta=0.001 is the absolute accuracy goal.
     * @param double epsilon=0.001 is the relative accuracy goal.
     * @param bool print_calls=false print the number of recursive calls after integrating.
     **/
    public static double integrate(Integral i, double delta=0.001, double epsilon=0.001, bool print_calls=false){
        double a_input = i.a;
        double b_input = i.b;
        var f_input = i.f;

        Func<double, double> f;
        double a, b;
        if ( double.IsNegativeInfinity(a_input) &&  double.IsPositiveInfinity(b_input)){
            // Both limits are inf. Use eqn. 61
            f = delegate(double t) {
                return ( f_input( (1-t)/t ) + f_input( - (1-t)/t ) ) / (t*t);
            };
            a = 0;
            b = 1;

        } else if (double.IsNegativeInfinity(a_input)) {

            // Only upper bound is inf. Use eqn. 65
            f = delegate(double t) {
                return f_input( b_input - (1-t)/t ) / (t*t);
            };
            a = 0;
            b = 1;

        } else if (double.IsPositiveInfinity(b_input)) {
            // Only lower bound is inf. Use eqn. 63
            f = delegate(double t) {
                return f_input( a_input + (1-t)/t ) / (t*t);
            };
            a = 0;
            b = 1;
        } else {
            f = f_input;
            a = a_input;
            b = b_input;
        }

        double h=b-a;
        // first call, no points to reuse
        double f2 = f(a+2*h/6); 
        double f3 = f(a+4*h/6);

        // Reset counter
        rec_calls = 0;
        double result = _rec_integrate(f, a, b, delta, epsilon, f2, f3);
        if (print_calls){
            Console.WriteLine($"The total amount of recursive calls was {rec_calls}.");
        }
        return result;
    }
    

    /** Helper function to be called recursively to compute the integral of a function on the interval [a,b].
     * @param Func<double,double> f the function to integrate.
     * @param double a is the starting point of the integration region.
     * @param double b is the ending point of the integration region.
     * @param double delta=0.001 is the absolute accuracy goal.
     * @param double epsilon=0.001 is the relative accuracy goal.
     * @param is the 
     **/
    private static double _rec_integrate(Func<double,double> f, double a, double b, double delta, double epsilon, double f2, double f3, int acc = 0){
        rec_calls += 1;
        double h = b - a;

        double f1=f(a+h/6), f4=f(a+5*h/6);

        // higher order rule
        double Q = (2*f1+f2+f3+2*f4)/6*(b-a);

        // lower order rule
        double q = (f1 + f2 + f3 + f4) / 4 * (b - a);

        // Check if error is low enough. Otherwise split integral into two and recompute.
        double err = Abs(Q-q);
        if (err <= delta + epsilon * Abs(Q)){
            return Q;
        }
        else {
            return _rec_integrate(f, a, (a+b)/2, delta/Sqrt(2), epsilon, f1, f2) + _rec_integrate(f, (a+b)/2, b, delta/Sqrt(2), epsilon, f3, f4);
        }
    }


    /** Recursively compute the integral using Clenshaw-Curtis.
     * @param Func<double,double> f the function to integrate.
     * @param double a is the starting point of the integration region.
     * @param double b is the ending point of the integration region.
     * @param double delta=0.001 is the absolute accuracy goal.
     * @param double epsilon=0.001 is the relative accuracy goal.
     * @param bool print_calls=false print the number of recursive calls after integrating.
     **/
    public static double clenshaw_curtis(Func<double, double> f, double a, double b, double delta=0.001, double epsilon=0.001, bool print_calls=false){
        var _int = new Integral(a, b, f, "");
        return clenshaw_curtis(_int, delta:delta, epsilon:epsilon, print_calls:print_calls);
    }


    /** Recursively compute the integral using Clenshaw-Curtis.
     * @param Integral i is the integral to do.
     * @param double delta=0.001 is the absolute accuracy goal.
     * @param double epsilon=0.001 is the relative accuracy goal.
     * @param bool print_calls=false print the number of recursive calls after integrating.
     **/
    public static double clenshaw_curtis(Integral i, double delta=0.001, double epsilon=0.001, bool print_calls=false){
        double b = i.b;
        double a = i.a;
        var f = i.f;

        Func<double, double> f_new;
        Integral new_int;
        if (a == -1 && b == 1){
            f_new = delegate(double x){return f(Cos(x)) * Sin(x);};
            new_int = new Integral(0, PI, f_new, i.f_str);
        } else {
            f_new = delegate(double x){
                return f( (a+b)/2+(b-a) / 2*Cos(x) ) * Sin(x) * (b-a)/2;
            };
            new_int = new Integral(0, PI, f_new, i.f_str);
        } 

        return integrate(new_int, delta:delta, epsilon:epsilon, print_calls:print_calls);
    }
}