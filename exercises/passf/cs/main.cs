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
        int k = 0;
        System.Func<double, double> f = delegate(double x) {return Sin(k*x);};
        for (int i=1; i<=3; i++) {
            k = i;
            make_table(f, 0, PI, 0.1);
            WriteLine("\n\n\n");
        }
        return 0;
    }
}
