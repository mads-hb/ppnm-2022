using System.Diagnostics;
using System;

static class MainProgram{
    static int Main(){
        CheckEigenvalues();
        TimeRoutine();
        return 0;
    }

    static void CheckEigenvalues(){
        int N = 10;

        System.Console.WriteLine($"Generating random diagonal matrix of dimension {N}x{N}.");
        var diag = matrix.random_diagonal(N);
        diag.print();
        System.Console.WriteLine("\n");

        System.Console.WriteLine($"Generating random column vector of dimension {N}.");
        var col = matrix.random_column(N);
        col.print();
        double sigma = 1;
        System.Console.WriteLine("\n");

        System.Console.WriteLine($"The eigenvalues found are:");
        var eigs =  SymmetricRankOne.Eigenvalues(diag, col, sigma);
        eigs.print();
        System.Console.WriteLine("\n");


        System.Console.WriteLine($"This can be compared with the diagonal matrix obtained through our Jacobi routine:");
        var V = new matrix(N, N);
        var A = diag + sigma*col*col.transpose();
        Jacobi.diag(A, V);
        A.print();

        using (var outfile = new System.IO.StreamWriter($"data/eig_spectrum.txt")){
            for (int i=0; i<N; i++){
                outfile.WriteLine($"{i+1}\t{eigs[i]}\t{A[i,i]}");
            }
        }
    }

    static void TimeRoutine(){
        int MAX = 200;

        double[] x, y_rank1, y_jacobi, dy;

        
        x = new double[MAX];
        y_rank1 = new double[MAX];
        y_jacobi = new double[MAX];
        dy = new double[MAX];

        using (var outfile = new System.IO.StreamWriter($"data/timing.txt")){
            for(int i=1; i <= MAX; i++){
                int N = i;

                // Generate random matrix of size N.
                var diag = matrix.random_diagonal(N);
                var col = matrix.random_column(N);
                double sigma = 1;
                

                // Define Matrix for Jacobi routine
                var V = new matrix(N, N);
                var A = diag + sigma*col*col.transpose();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                try {
                    SymmetricRankOne.Eigenvalues(diag, col, sigma);
                } catch (NotSupportedException e) {
                        continue;
                }
                
                stopwatch.Stop();
                double timing_rank1 = stopwatch.ElapsedMilliseconds / 1000.0;
                y_rank1[i-1] = timing_rank1;

                stopwatch = new Stopwatch();
                stopwatch.Start();
                Jacobi.diag(A, V);
                stopwatch.Stop();
                double timing_jacobi = stopwatch.ElapsedMilliseconds / 1000.0;
                y_jacobi[i-1] = timing_jacobi;

                x[i-1] = i;
                dy[i-1] = 1e-5;
                outfile.WriteLine($"{i}\t{timing_rank1}\t{timing_jacobi}");
            }
        }

        // Use least squares fit:
        double[][] data_rank1 = {x, y_rank1, dy};
        var fs_rank1 = new Func<double,double>[] { z => 1, z => z, z => z*z};        
        double[] coefs_rank1;
        matrix covb_rank1;
        (coefs_rank1, covb_rank1) = LeastSquares.fit_covariance(data_rank1, fs_rank1);

        double[][] data_jacobi = {x, y_jacobi, dy};
        var fs_jacobi= new Func<double,double>[] { z => 1, z => z, z => z*z, z => z*z*z};        
        double[] coefs_jacobi;
        matrix covb_jacobi;
        (coefs_jacobi, covb_jacobi) = LeastSquares.fit_covariance(data_jacobi, fs_jacobi);


        using (var outfile = new System.IO.StreamWriter($"data/fitting.txt")){
            for (double value = 1; value < MAX; value += 0.1){
                double res_rank1 = 0;
                for (int k = 0; k < fs_rank1.Length; k++){
                    res_rank1 += coefs_rank1[k] * fs_rank1[k](value);
                }

                double res_jacobi = 0;
                for (int k = 0; k < fs_jacobi.Length; k++){
                    res_jacobi += coefs_jacobi[k] * fs_jacobi[k](value);
                }
                outfile.WriteLine($"{value}\t{res_rank1}\t{res_jacobi}");
            }
        }
    }
}