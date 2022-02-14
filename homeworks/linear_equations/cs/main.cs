using static System.Console;
using System.Diagnostics;
using static System.Math;


public static class MainProgram{

    public static void ExerciseA1(){
        int n = 10, m = 7;
        Matrix A = Matrix.RandomMatrix(n, m);
        Matrix Q = A.copy();
        Matrix R = new Matrix(m, m);
        A.print("Printing A");
        GR_solve.decomp(Q, R);
        Q.print("Q is:");
        R.print("R is:");
        Matrix QR = Q*R;
        QR.print("Product QR:");

        Matrix QtQ = Q.T * Q;
        QtQ.print("Q.T * Q is:");
    }

    public static void ExerciseA2(){
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


        WriteLine("Solving Ax=b for x... x is:");
        Vector x = GR_solve.solve(A, b);
        x.print("Solution is:");

        WriteLine("Let's find A*x that should equal b:");
        Vector b2 = A * x;
        b2.print("A*x is:");
    }



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

    public static void ExerciseC(){
        using (var outfile = new System.IO.StreamWriter("timing.txt")){
            for(int i=1; i <= 300; i++){
                int n = i;
                Matrix A = Matrix.RandomMatrix(n,n);
                Matrix R = new Matrix(n,n);
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                GR_solve.decomp(A, R);
                stopwatch.Stop();
                double timing = stopwatch.ElapsedMilliseconds / 1000.0;
                double fit = Pow(i, 3) * 0.00000000345;
                outfile.WriteLine($"{i}\t{timing}\t{fit}");
            }
        }
    }

    public static int Main(){
        WriteLine("--------------------- Solving exercise A1: ---------------------");
        ExerciseA1();
        WriteLine("--------------------- Solving exercise A2: ---------------------");
        ExerciseA2();
        WriteLine("--------------------- Solving exercise B: ---------------------");
        ExerciseB();
        ExerciseC();
        return 0;
    }
}