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


    /**
     * Perform Monte Carlo integration using quasi random numbers
     * @param Func<vector,double> f
     * @param vector a
     * @param vector b
     * @param int N The number of random points generated.
     * @return (double, double) The integral value and the error respectively.
     */
    public static (double, double) quasi(Func<vector,double> f,vector a,vector b,int N){
        int dim = a.size; double V=1; 
        for(int i = 0; i < dim; i++){
            V *= b[i] - a[i];
        }
        double sum = 0, sum2 = 0;

        // We sometimes need to skip a random point since this yields nan or inf.
        int skip = 0; 
        for(int i = 0; i < N; i++){
            var halton_vec = halton(i+skip, a.size);
            var halton_vec2 = halton(i+skip, a.size, skip_base:2);

            var x = new vector(a.size);
            var x2 = new vector(a.size);
            for(int k = 0; k < a.size; k++){
                x[k] = a[k] + halton_vec[k] * (b[k]-a[k]);
                x2[k] = a[k] + halton_vec2[k] * (b[k]-a[k]);
            }
            double fx = f(x);
            double fx2 = f(x2);

            if (double.IsNaN(fx) || double.IsInfinity(fx) || double.IsNaN(fx2) || double.IsInfinity(fx2)){
                // Redo iteration with a new base for halton seq.
                --i; ++skip;
            } else{
                sum += fx;
                sum2 += fx2;
            }
        }
        double mean = sum/N;
        double mean2 = sum2/N;
        var result = (mean*V, V*Abs(mean - mean2));

        return result;
    }

    /**Define the corput sequence
     */    
    private static double corput(int n, int _base){
        double q = 0;
        double bk = 1.0 / _base;
        while (n>0){
            q += (n % _base) * bk; 
            n /= _base;
            bk /= _base;
        }
        return q;
    }

    /**Define the halton sequence
     */
    private static vector halton(int n , int d, int skip_base=0){
        int[] _base = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67};
        int maxd = _base.Length; 
        if (d > maxd){
            throw new ArgumentException();
        }
        var x = new vector(d);
        for (int i = 0; i<d ; i++) {
            // Find the index we want retrieve.
            int ix = (i+skip_base) % _base.Length;
            x[i] = corput(n, _base[ix]);
            }
        return x;
        }


}