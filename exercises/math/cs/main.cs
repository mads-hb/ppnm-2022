using static System.Console;
using static System.Math;

public static class MainProgram{

    public static int Main(){
        double sqrt2=Sqrt(2.0);
        Write($"sqrt(2) = {sqrt2}\n");
        Write($"sqrt2*sqrt2 = {sqrt2*sqrt2} (should be equal 2)\n");

        double e_pi=Exp(PI);
        Write($"Exp(PI) = {e_pi}\n");
        Write($"Log(Exp(PI)) = {Log(e_pi)} (should be equal PI)\n");

        double pi_e=Pow(PI, E);
        Write($"Pow(PI, e) = {pi_e}\n");
        Write($"Log(Pow(PI, e), PI) = {Log(pi_e, PI)} (should be equal e)\n");
        return 0;
    }
}