using System;
using static System.Math;


static class MainProgram{

    static void ExerciseA(){
        int i = 1; 
        while(i + 1 > i){i++;}
        Console.WriteLine("My max int i = {0}",i);
        Console.WriteLine("Compare to int.MaxValue {0}",int.MaxValue);
        
        i = 1; 
        while(i - 1 < i){i--;}
        Console.WriteLine("My min int i = {0}",i);
        Console.WriteLine("Compare to int.MinValue {0}",int.MinValue);

        Console.WriteLine("Alternatively, max int is min - 1 = {0}",i-1);
    }

    static void ExerciseB(){
        double x = 1; 
        while(1 + x != 1){x/=2;} 
        x*=2;
        float y = 1F; 
        while((float)(1F+y) != 1F){y/=2F;} 
        y*=2F;

        Console.WriteLine($"The double precision is {x}. Pow(2,-52) = {System.Math.Pow(2,-52)}");
        Console.WriteLine($"The single precision is {y}. Pow(2,-23) = {System.Math.Pow(2,-23)}");
    }

    static void ExerciseC(){
        int n = (int) 1e6;
        double epsilon = Pow(2,-52);
        double tiny = epsilon/2;
        double sumA=0, sumB=0;

        sumA += 1; 
        for(int i=0; i < n; i++){
            sumA += tiny;
        }
        Console.WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");

        for(int i=0;i<n;i++){
            sumB += tiny;
        }
        sumB+=1;
        Console.WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");
    }

    private static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
        return (Abs(a - b) < tau) || (Abs(a-b)/(Abs(a) + Abs(b)) < epsilon);
    }

    static void ExerciseD(){
        Console.WriteLine("Implemented approx method. Let's test it");
        Console.WriteLine($"approx(1,1) is {approx(1,1)}");
        Console.WriteLine($"approx(1,2) is {approx(1,2)}");
        Console.WriteLine($"approx(1,0.5) is {approx(1,0.5)}");
        Console.WriteLine($"approx(0,Pow(2,-52)) is {approx(0,Pow(2,-52))}");
        Console.WriteLine($"approx(0,Pow(2,-52)/2) is {approx(0,Pow(2,-52)/2)}");
        Console.WriteLine($"approx(100,100+Pow(2,-23)) is {approx(100,100+Pow(2,-23))}");

    }

    static int Main(){
        Console.WriteLine("\n***Test starting***\n");
        ExerciseA();
        Console.WriteLine("\n***************\n");
        ExerciseB();
        Console.WriteLine("\n***************\n");
        ExerciseC();
        Console.WriteLine("\n***************\n");
        ExerciseD();
        return 0;
    }
}