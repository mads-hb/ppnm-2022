using static System.Math;


public static class GR_solve{
    public static void decomp(Matrix A, Matrix R){
        int m = A.size2;
        for(int i = 0; i < m; i++){
            R[i, i] = A[i].norm();
            A[i] = A[i]/R[i,i];
            for(int j = i+1; j < m; j++){
                R[i,j] = A[i].dot(A[j]);
                A[j] -= A[i]*R[i,j];
            }
        }   

    }


    public static Vector solve(Matrix Q, Matrix R, Vector b){
        // We know that Rx=QT*b=y. This can be solved by backwards substitution since
        // R is upper triangular.
        Vector y = Q.T * b;
        Vector x = new Vector(y.size);
        for(int i = x.size-1; i >= 0 ; i--){
            x[i] = y[i];
            for(int j = i+1; j < y.size; j++){
                x[i] -= R[i,j]*x[j];
            }
            x[i] = x[i]/R[i,i];
        }
        return x;
    }

    public static Vector solve(Matrix A, Vector b){
        Matrix Q = A.copy();
        Matrix R = new Matrix(A.size2, A.size2);
        decomp(Q, R);
        return solve(Q, R, b);

    }

    public static Matrix inverse(Matrix Q, Matrix R){
        int n = Q.size1;
        int m = Q.size2;
        Matrix A = new Matrix(m,n);
        Vector e;
        for(int i = 0; i < n; i++){
            e = new Vector(n);
            e[i] = 1;
            A[i] = solve(Q, R, e);
        }
        return A;

    }

    public static Matrix inverse(Matrix A){
        Matrix Q = A.copy();
        Matrix R = new Matrix(A.size2, A.size2);
        decomp(Q, R);
        return inverse(Q, R);
    }
}