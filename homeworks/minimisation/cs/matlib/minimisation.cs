using System;
using static System.Math;


public static class Minimisation{

    private static int iters = 0;

    /** Use a quasi newton method to find the minimum of an objective function.
     * @param Func<vector,vector> f the function to find roots for.
     * @param vector x0 the initial guess.
     * @param double eps the tolerance if the result.
     */
    public static vector QuasiNewton(Func<vector,double> f, vector x0, double eps=1e-12){
        double alpha = 1e-4;

        iters = 0;

        int n = x0.size;

        vector x = x0.copy();
        vector grad_x = gradient(f, x);

        matrix B = matrix.id(n);

        do {
            iters += 1;

            // eqn. 6
            vector delta_x = -B * grad_x;

            // init s and f(x+s)
            vector s;
            double f_of_xs;
            // eqn. 8 and Amijo condition eqn. 9
            double lambda = 1;
            do {
                s = delta_x*lambda;
                f_of_xs = f(x + s);
                // Reset B to identity if lambda is too small
                if(lambda < 1.0 / Pow(2,5)){
                    B.setid();
                    break; // accept anyway
                }
                lambda /= 2;
            } while (f_of_xs >= f(x) + alpha * s.dot(grad_x));
            

            // Prepare for SR1 ipdate
            vector grad_x_new = gradient(f, x+s);

            // eqn. 12
            vector y = grad_x_new - grad_x;
            vector u = s - B*y;

            // eqn. 18 - SR1 update
            double uTy = u.dot(y);
            if(Abs(uTy) > eps){
                B.update(u, u, 1/uTy); // SR1 update
            }

            // Prepare for next iteration
            x += s;
            grad_x = grad_x_new;

        } while(grad_x.norm() > eps);

        System.Console.WriteLine($"The number of iterations was {iters}.");
        return x;
    }


    /** Compute the gradient of a multidimensional function.
     */
    private static vector gradient(Func<vector, double> f, vector x, double dx=1e-6){
        int n = x.size;
        vector grad = new vector(n);
        double fx = f(x);;

        for(int i = 0; i < n; i++){
            x[i] += dx;
            grad[i] = (f(x) - fx)/dx;
            x[i] -= dx;         
        }
        return grad;
    }

}