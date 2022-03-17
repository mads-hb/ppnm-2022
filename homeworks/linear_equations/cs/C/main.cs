using static System.Console;
using System.Diagnostics;
using static System.Math;


public static class MainProgram{

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
        WriteLine("--------------------- Solving exercise C ---------------------");
        WriteLine("See the .png file for a plot showing that the run time of the algorithm is O(n^3).");
        ExerciseC();
        return 0;
    }
}