using static System.Console;
using System.Diagnostics;
using static System.Math;


public static class MainProgram{

    public static void ExerciseB(){
        int n = 9;
        Matrix A = Matrix.RandomMatrix(n, n);
        Matrix Q = A.copy();
        Matrix R = new Matrix(n, n);

        Vector b = Vector.RandomVector(n);

        A.print("Matrix A before decomposition:");
        b.print("Vector b");

        
        GR_solve.decomp(Q, R);
        Q.print("Matrix Q after decomposition:");
        R.print("Matrix R after decomposition:");


        Matrix B = GR_solve.inverse(A);
        B.print("Finding the inverse of A:");

        Matrix I = A * B;
        I.print("Checking AB=I");
        I = B*A;
        I.print("Checking BA=I");
    }


    public static int Main(){
        WriteLine("--------------------- Solving exercise B: ---------------------");
        ExerciseB();
        return 0;
    }
}