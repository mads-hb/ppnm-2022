using static System.Math;


public static class GR_solve{
    static void decomp(Matrix A, Matrix R, Matrix Q){

        // Iterate over second dimension of matix - columns.
        for (int i = 0; i < A.size2; ++i){
            // Compute the norm of the column a_i of matrix A.
            double ai_norm = A[i].norm();

            // Set the entry R_ii to sqrt(a_i * a_i)
            R[i, i] = ai_norm;

            // Set the column Q_i to a_i divided by the ai_norm computed before. 
            Q[i] = A[i] / ai_norm;
            
            // Enter second loop
            for (int j = i; j < A.size2; ++j){
                // Compute the product q_i and a_j and set R_ij equal to it.
                double q_dot_a = Q[i].dot(A[j]);
                R[i,j] = q_dot_a;

                for (int k = 0; k < A.size1; ++k){
                    double val = A[k, j] - q_dot_a * Q[k, i];
                    A[k, j] = val;
                }

            }
        }
    }

    static Vector solve(Matrix Q, Matrix R, Vector b){
        return new Vector(1);

    }

    static Vector solve(Matrix A, Vector b){
        return new Vector(1);

    }

    static Matrix inverse(Matrix Q, Matrix R){
        return new Matrix(1,1);

    }

    static Matrix inverse(Matrix A){
        return new Matrix(1,1);

    }
}