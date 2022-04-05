using static System.Math;


public static class GR_solve{
    public static void decomp(matrix A, matrix R){
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


    public static vector solve(matrix Q, matrix R, vector b){
        // We know that Rx=QT*b=y. This can be solved by backwards substitution since
        // R is upper triangular.
        vector y = Q.T * b;
        vector x = new vector(y.size);
        for(int i = x.size-1; i >= 0 ; i--){
            x[i] = y[i];
            for(int j = i+1; j < y.size; j++){
                x[i] -= R[i,j]*x[j];
            }
            x[i] = x[i]/R[i,i];
        }
        return x;
    }

    public static vector solve(matrix A, vector b){
        matrix Q = A.copy();
        matrix R = new matrix(A.size2, A.size2);
        decomp(Q, R);
        return solve(Q, R, b);

    }

    public static matrix inverse(matrix Q, matrix R){
        int n = Q.size1;
        int m = Q.size2;
        matrix A = new matrix(m,n);
        vector e;
        for(int i = 0; i < n; i++){
            e = new vector(n);
            e[i] = 1;
            A[i] = solve(Q, R, e);
        }
        return A;

    }

    public static matrix inverse(matrix A){
        matrix Q = A.copy();
        matrix R = new matrix(A.size2, A.size2);
        decomp(Q, R);
        return inverse(Q, R);
    }
}