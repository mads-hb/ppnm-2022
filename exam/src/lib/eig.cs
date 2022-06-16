using System;
using System.Collections.Generic;
using static System.Math;


public static class SymmetricRankOne{

    /**
     * Sort the rows of the matrix D, and the column vector u, such that the
     * diagonal of D is sorted.
     */
    private static (matrix, matrix) Sort(matrix D, matrix u){
        int N = D.size1;
        matrix D_cp = new matrix(N,N);
        matrix u_cp = new matrix(N, 1);

        var already_found = new HashSet<int>();
        for (int i=0; i < N; i++){
            double min_val = Double.PositiveInfinity;
            int min_ix = 0;
            for (int j=0; j < N; j++){
                if (D[j,j] <= min_val && !already_found.Contains(j)) {
                    min_val = D[j,j];
                    min_ix = j;
                }
            }
            already_found.Add(min_ix);
            D_cp[i, i] = D[min_ix, min_ix];
            u_cp[i, 0] = u[min_ix, 0];
        }
        return (D_cp, u_cp);
    }

    /**
     * Find the eigenvalues of a matrix on the form:
     *                  A = D + u*u^T
     * where D is a diagonal matrix and u is a column vector. Use symmtric 
     * rank-1 updates to solve equation leading to the eigenvalues.
     */
    public static vector Eigenvalues(matrix D, matrix u, double sigma) {
        // Check input dimensions.
        if (D.size1 != D.size2) {
            throw new NotSupportedException("The matrix D is not diagonal.");
        }
        if (u.size2 != 1) {
            throw new NotSupportedException("The matrix u is not a column vector.");
        }
        if (D.size1 != u.size1) {
            throw new NotSupportedException("Shapes of D and u don't match.");
        }

        // Sort based on diagonal matrix entries.
        (D, u) = SymmetricRankOne.Sort(D, u);
        int N = D.size1;

        // Find the roots individually since that is more stable.
        var roots = new vector(N);
        for (int i = 0; i < N; i++){
            // Define the system of secular equation we need to solve.
            Func<vector,vector> f = eigs => {
            vector res = new vector(1);
            double sum = 1;
            for (int m = 0; m<N; m++){
                if (u[m,0] != 0 && D[m,m]-eigs[0] != 0) {
                    sum += sigma*u[m,0]*u[m,0]/(D[m,m]-eigs[0]);
                }
            }
            res[0] = sum;
            return res;
            };

            // Initialize starting guesses as halfway between sorted diagonal 
            // entries as suggested in notes.
            var x0 = new vector(1);
            if (sigma >= 0){
                if (i == N-1){
                    var inner_prod = (u.transpose() * u)[0,0];
                    x0[0] = (2*D[N-1, N-1] + sigma * inner_prod)/2;
                } else {
                    x0[0] = (D[i,i]+D[i+1,i+1])/2;
                }
            } else {
                if (i == 0){
                    var inner_prod = (u.transpose() * u)[0,0];
                    x0[0] = (2*D[0,0]+ sigma * inner_prod)/2;
                } else {
                    x0[0] = (D[i-1,i-1]+D[i,i])/2;
                }
            }
            var root = RootFinding.Newton(f, x0, max_iter:10000, eps:1e-5);
            roots[i] = root[0];
        }
        return roots;
    }
}