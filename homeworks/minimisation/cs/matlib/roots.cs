using System;
using static System.Math;


public static class RootFinding {

    /** Compute the Jacobian of a function using finite differences.
     * J_ik = d f_i / d x_k. 
     */
    private static matrix jacobian(Func<vector,vector> f, vector x){
        int n = x.size;
        matrix J = new matrix(n, n);
        vector f_of_x = f(x);
        for (int k = 0; k < n; k++){
            double delta_x =  Abs(x[k]) * Pow(2, -26);
            vector x_new = x.copy();
            x_new[k] += delta_x;

            vector diff = f(x_new) - f_of_x;
            for (int i = 0; i < n; i++){
                J[i, k] = diff[i] / delta_x;
            }
        }
        return J;
    }

    /** Use the Newton-Raphson method to find the roots of a multidimensional
     * function.
     * @param Func<vector,vector> f the function to find roots for.
     * @param vector x0 the initial guess.
     * @param int max_iter the maximum number of iterations before stopping.
     * @param double eps the tolerance if the result.
     */
    public static vector Newton(Func<vector,vector> f, vector x0, int max_iter=1000, double eps=1e-8){
        vector x = x0.copy();
        for(int i=0; i<x0.size; i++){
            if(x[i] == 0){
                x[i] = 1e-5;
            }
        } 

        // Are we converged
        bool conv = false;

        for (int _ = 0; _ < max_iter; _++){
            matrix J = jacobian(f, x);
            vector newton_step = GR_solve.solve(J, -f(x));

            double step_size = 1;
            do {
                step_size /= 2;
            } while (f(x + step_size * newton_step).norm() > (1 - step_size/2) * f(x).norm() & step_size > 1.0/32);

            x += step_size * newton_step;
            // Convergence criterion.
            if (f(x).norm() < eps){
                conv = true;
                break;
            }
        }

        if(!conv){
            throw new NotSupportedException("The result was not found.");
        }
        return x;
    }
}