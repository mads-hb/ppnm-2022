using System;
using static System.Math;


public class NeuralNetwork{
    private int n; /* number of hidden neurons */
    private Func<double,double> f; /* activation function */
    private vector _params; /* network parameters */

    public NeuralNetwork(int n, Func<double,double> f){
        this.n = n;
        this.f = f;
        vector v = new vector(3*n);
        for (int i = 0; i < 3*n; i++){
            v[i] = 1;
        }
        _params = v;
    }

    public double response(double x){
        double sum = 0;
        for (int i = 0; i < n; i++){
            double a = _params[3*i];
            double b = _params[3*i + 1];
            double w = _params[3*i + 2];
            sum += f((x-a)/b) * w;
        }
        return sum;
    }

    public void train(vector x,vector y){
        Func<vector,double> cost = param0 => {
            _params = param0;
            double sum = 0;

            for(int i=0; i<x.size; i++){
                sum += Pow(response(x[i]) - y[i], 2);
            }

            return sum/x.size;
        };

        vector initial_params = _params.copy();
        vector res = Minimisation.SimplexDownhill(cost, initial_params, scale:5, eps:1e-16);
        _params = res;
    }
}


public class NeuralNetworkCalculus{
    private int n; /* number of hidden neurons */
    private Func<double,double> f; /* activation function */
    private Func<double,double> f_int; /* activation function for integral*/
    private Func<double,double> f_diff; /* activation function for derivative*/
    private vector _params; /* network parameters */

    public NeuralNetworkCalculus(int n, Func<double,double> f, Func<double,double> f_int, Func<double,double> f_diff){
        this.n = n;
        this.f = f;
        this.f_int = f_int;
        this.f_diff = f_diff;
        vector v = new vector(3*n);
        for (int i = 0; i < 3*n; i++){
            v[i] = 1;
        }
        _params = v;
    }

    private double _response(double x, Func<double, double> func) {
        double sum = 0;
        for (int i = 0; i < n; i++){
            double a = _params[3*i];
            double b = _params[3*i + 1];
            double w = _params[3*i + 2];
            sum += func((x-a)/b) * w;
        }
        return sum;
    }

    public double response(double x){
        return _response(x, this.f);
    }

    public double response_integral(double x){
        return _response(x, this.f_int);
    }

    public double response_derivative(double x){
        return _response(x, this.f_diff);
    }


    public void train(vector x,vector y){
        Func<vector,double> cost = param0 => {
            _params = param0;
            double sum = 0;

            for(int i=0; i<x.size; i++){
                sum += Pow(response(x[i]) - y[i], 2);
            }

            return sum/x.size;
        };

        vector initial_params = _params.copy();
        vector res = Minimisation.SimplexDownhill(cost, initial_params, scale:5, eps:1e-16);
        _params = res;
    }
}