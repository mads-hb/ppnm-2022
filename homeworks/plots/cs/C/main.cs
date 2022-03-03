using static System.Console;
using static System.Math;
using static cmath;


public static class MainProgram{

    static double gamma(double x){
        // single precision gamma function (Gergo Nemes, from Wikipedia)
        if(x<0)return PI/Sin(PI*x)/gamma(1-x);
        if(x<9)return gamma(x+1)/x;
        double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
        return Exp(lngamma);
    }

    private static complex cgamma(complex z){
        // single precision gamma function (Gergo Nemes, from Wikipedia)
    	if(z.Re<0)return PI/sin(PI*z)/cgamma(1-z);
    	if(z.Re<9)return cgamma(z+1)/z;
    	complex lngamma=z*log(z+1/(12*z-1/z/10))-z+log(2*PI/z)/2;
    	return exp(lngamma);
	}

    private static int Fac(int x, int acc=1){
        if (x==0) return acc;
        else return Fac(x-1, acc*x);
    }
    
    public static int Main(){
        for (double x = -5 ; x < 5; x += 0.05){
            for (double y = -5; y < 5; y+= 0.05){
                complex z = new complex(x, y);
                WriteLine($"{x}\t{y}\t{abs(cgamma(z))}");
            }
        }
        return 0;
    }
}
