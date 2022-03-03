using static System.Console;
using static System.Math;


public static class MainProgram{

    static double gamma(double x){
        // single precision gamma function (Gergo Nemes, from Wikipedia)
        if(x<0)return PI/Sin(PI*x)/gamma(1-x);
        if(x<9)return gamma(x+1)/x;
        double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
        return Exp(lngamma);
    }

    private static double loggamma(double x){
        // single precision gamma function (Gergo Nemes, from Wikipedia)
        if(x<0)return loggamma(-x);
        if(x<9)return loggamma(x+1) - Log(x);      
	double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
    	return lngamma; 
    }

    private static int Fac(int x, int acc=1){
        if (x==0) return acc;
        else return Fac(x-1, acc*x);
    }
    
    public static int Main(){
        using (var f = new System.IO.StreamWriter("tabulated.txt")){
            for (int i = 1; i < 4; i++){
                f.WriteLine($"{i}\t{Fac(i-1)}");
            }
        }
        for (double x = -4.5 ; x < 4; x += 0.01){
            WriteLine($"{x}\t{gamma(x)}");
        }
        using (var f = new System.IO.StreamWriter("out2.txt")){
            for (double x=1; x < 1e6; x+=10){
                f.WriteLine($"{x}\t{loggamma(x)}");
            }
        }
        return 0;
    }
}
