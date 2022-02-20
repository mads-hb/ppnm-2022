using static System.Console;
using static System.Math;


public static class MainProgram{

    public static void make_table(System.Func<double, double> f, double a, double b, double dx){
        WriteLine("x\tf(x)");
        while (a <= b) {
            WriteLine($"{a}\t{f(a)}");
            a += dx;
        }
    }

    public static int Main(){
        System.Func<double, double, double> f = delegate(double x, double k) {return Sin(k*x);};
        for (int i=1; i<=3; i++) {
            make_table(x => f(x, 1), 0, PI, 0.01);
            WriteLine("\n\n\n");
        }
        return 0;
    }
}
