using System;
using static System.Math;


public static class MonteCarlo {

    private static Random random_generator = new Random();


    /**
     * Perform Monte Carlo integration using pseudo random numbers
     * @param Func<vector,double> f
     * @param vector a
     * @param vector b
     * @param int N The number of random points generated.
     * @return (double, double) The integral value and the error respectively.
     */
    public static (double, double) pseudo(Func<vector,double> f,vector a,vector b,int N){
        int dim = a.size; double V=1; 
        for(int i = 0; i < dim; i++){
            V *= b[i] - a[i];
        }
        double sum = 0, sum2 = 0;

        for(int i = 0; i < N; i++){
            var x = random_vector(a, b);
            double fx = f(x);
            sum += fx;
            sum2 += fx*fx;
        }
        double mean = sum/N;
        double sigma = Sqrt( sum2 / N - mean*mean);
        var result=(mean*V, sigma*V/Sqrt(N));

        return result;
    }

    /** Generate a random vector of length N where entry i is in interval
     * between a[i] and b[i].
     * @param vector a has length N.
     * @param vector b has length N.
     * @return the random vector of length N.
     */
    private static vector random_vector(vector a, vector b){
        if (a.size != b.size){
            throw new ArgumentException();
        }
        var x = new vector(a.size);
        for(int k = 0; k < a.size; k++){
            var rand_d = random_generator.NextDouble();
            x[k] = a[k] + rand_d * (b[k]-a[k]);
        }
        return x;
    }

}