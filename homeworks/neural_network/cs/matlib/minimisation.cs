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

    public static vector SimplexDownhill(Func<vector,double> f, vector x0, double scale, double eps=1e-12){
        int n = x0.size;
        // Generate n+1 test points
        vector[] test_points = get_test_points(x0, n, scale);
        int low_ix, high_ix;
        double[] f_values;
        vector reflection, expansion, contraction;
        do {
            iters ++;
            // Get highest and lowest value

            (high_ix, low_ix, f_values) = get_high_low_index(f, test_points);
            // Get position of centroid
            var centroid = get_centroid(test_points, high_ix);
            
            // reflect
            reflection = centroid + (centroid - test_points[high_ix]);

            if (f(reflection) < f_values[low_ix]){
                // expand
                expansion = centroid + 2*(centroid - test_points[high_ix]);

                if (f(expansion) < f(reflection)){
                    test_points[high_ix] = expansion;
                } else {
                    test_points[high_ix] = reflection;
                }

            } else {
                if (f(reflection) < f_values[high_ix]){
                    test_points[high_ix] = reflection;
                } else {
                    contraction = centroid + 0.5 * (test_points[high_ix] - centroid);
                    if (f(contraction) < f_values[high_ix]){
                        test_points[high_ix] = contraction;
                    } else {
                        // reduce
                        for (int k = 0; k < n+1; k++){
                            if (k != low_ix){
                                test_points[k] = 0.5*(test_points[k] + test_points[low_ix]);
                            }
                        }
                    }
                }
            }

        } while (eps < std(f_values));
        return test_points[low_ix];
    }

    private static vector[] get_test_points(vector x0, int n, double scale){
        vector[] test_points = new vector[n+1];
        test_points[n] = x0.copy();
        for (int i = 0; i < n; i++){
            x0[i] += scale;
            test_points[i] = x0.copy();
            x0[i] -= scale;
        }
        return test_points;
    }

    private static (int, int, double[]) get_high_low_index(Func<vector,double> f, vector[] points){
        double f_high=Double.NegativeInfinity, f_low=Double.PositiveInfinity;
        int high=0, low=0;
        double[] fvalues = new double[points.Length];
        for (int i = 0;  i < points.Length; i++){
            double fval = f(points[i]);
            if (fval > f_high){
                high = i;
                f_high = fval;
            } else if (fval < f_low){
                low = i;
                f_low = fval;
            }
            fvalues[i] = f(points[i]);
        }
        // Console.WriteLine($"fhigh={f_high}, flow={f_low}");
        return (high, low, fvalues);
    }

    private static vector get_centroid(vector[] points, int highest_ix){
        // ignore highest when computing centroid
        vector res = new vector(points[0].size);
        for (int i = 0; i < points.Length; i++){
            if (i != highest_ix){
                res += points[i];
            }
        }
        res /= (points.Length - 1);
        return res;
    }

    private static double std(double[] arr){
        int n = arr.Length;
        double avg = 0;
        for (int i = 0; i < n; i++) {
            avg += arr[i];
        }
        avg /= n;
        double sum_squares = 0;
        for (int i = 0; i < n; i++) {
            sum_squares += Pow(arr[i] - avg, 2);
        }
        return Sqrt(sum_squares/(n-1));
    }

}