using static System.Math;

public static class MainProgram{

    static double ex(double x){
        if (x < 0) {return 1/ex(-x);}
        if (x > 1.0/8) {return Pow(ex(x/2), 2);} 
	    return 1+x*(1+x/2*(1+x/3*(1+x/4*(1+x/5*(1+x/6*(1+x/7*(1+x/8*(1+x/9*(1+x/10)))))))));	
    }
    
    public static int Main(){
        for (double x = -5; x < 5; x+=0.01){
            System.Console.WriteLine($"{x}\t{ex(x)}\t{Exp(x)}");
        }
        return 0;
    }
}
