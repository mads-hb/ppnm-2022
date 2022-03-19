using System;
using System.Diagnostics;



public static class LeastSquares {
    public static double[] fit(double[][] data, Func<double,double>[] funcs){
        Debug.Assert(data.Length == 3);
        double[] xs = data[0], ys = data[1], dys = data[2];
        Matrix A = new Matrix(xs.Length, funcs.Length);
        Vector b = new Vector(xs.Length);
        for (int i = 0; i < A.size1; i++) {
            for (int k = 0; k < A.size2; k++){
                A[i, k] = funcs[k](xs[i])/dys[i];
            }
            b[i] = ys[i] / dys[i];
        }

        Matrix R = new Matrix(A.size2, A.size2);
        GR_solve.decomp(A, R);
        Vector c = GR_solve.solve(R, A.T * b);
        double[] cs = new double[c.size];
        for (int i = 0; i < cs.Length; i++){
            cs[i] = c[i];
        }
        return cs;
    }

    /**
     * Fit and return vector of weigths as well as covariance matrix.
     **/
    public static (double[], Matrix) fit_covariance(double[][] data, Func<double,double>[] funcs){
        Debug.Assert(data.Length == 3);
        double[] xs = data[0], ys = data[1], dys = data[2];

        Matrix A = new Matrix(xs.Length, funcs.Length);
        Vector b = new Vector(xs.Length);
        for (int i = 0; i < A.size1; i++) {
            for (int k = 0; k < A.size2; k++){
                A[i, k] = funcs[k](xs[i])/dys[i];
            }
            b[i] = ys[i] / dys[i];
        }

        Matrix R = new Matrix(A.size2, A.size2);
        GR_solve.decomp(A, R);
        Vector c = GR_solve.solve(R, A.T * b);
        double[] cs = new double[c.size];
        for (int i = 0; i < cs.Length; i++){
            cs[i] = c[i];
        }


        return (cs, GR_solve.inverse(R.T * R));
    }
}