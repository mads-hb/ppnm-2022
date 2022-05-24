using System;
using static System.Console;
using static System.Math;


public class MainProgram{


    /** Integrate the equation -(1/2)f'' -(1/r)f = εf from rmin up to r 
     * with energy ε. Return the wavefunction at endpoint.
     */
    public static double auxillary(double r, double energy, double r_min, double acc, double eps){
        // Schrodinger equation to solve.
        Func<double,vector,vector> schrodinger = (x,y) => {
            return new vector(new double[] {y[1], -2*(energy + 1/x)*y[0]});
        };

        // Initial parameters
        vector y0 = new vector(new double[] {r_min - r_min*r_min, 1 - 2*r_min});
        vector y_res = ODE.driver_naive(schrodinger, r_min, y0, r, acc:acc, eps:eps);

        return y_res[0];
    }

    /** Returning the root of the auxillary function M(ε) ≡ F_ε(rmax), 
     * where F_ε(rmax) is the solution found by the ODE solver.
     */
    public static double solver(double r_min, double r_max, double acc=1e-3, double eps=1e-3){
        // Minimize over energy. Signature to match Newton method must be 
        // Func<vector,vector>.
        Func<vector,vector> f = (vector v) => {
            double endpoint = auxillary(r_max, v[0], r_min, acc:acc, eps:eps);
            return new vector(endpoint);
        };

        // Find negative root.
        vector res = RootFinding.Newton(f, new vector(-1.0));
        return res[0];
    }

    public static void Main(){
        double r_min = 1e-3;
        double r_max = 8;

        double energy = solver(r_min, r_max);
        WriteLine($"The estimate energy is {energy} Hartree and analytically we expect 0.5 Hartree.");

        using(var outfile = new System.IO.StreamWriter("hydrogen_wavefunction.txt")){
            for(double r=0; r<r_max; r+=0.5){
                outfile.WriteLine($"{r} {auxillary(r, energy, r_min, 1e-3, 1e-3)} {r*Exp(-r)}");
            }
        }

        using(var outfile = new System.IO.StreamWriter("r_convergence.txt")){
            r_min = 1e-3;

            for(r_max = 0.5; r_max <= 15; r_max += 0.5){
                energy = solver(r_min, r_max);
                outfile.WriteLine($"{r_max} {energy} {-0.5}");
            }
            outfile.WriteLine("\n");

            r_max = 8;
            for(r_min = 0.5; r_min >= 1e-3; r_min -= 0.01){
                energy = solver(r_min, r_max);
                outfile.WriteLine($"{r_min} {energy} {-0.5}");
            }
        }

        using(var outfile = new System.IO.StreamWriter("tol_convergence.txt")){
            r_min = 1e-6;
            r_max = 8.0;

            for(double acc=0.01; acc >= 1e-12; acc-=5e-5){
                energy = solver(r_min, r_max, acc:acc, eps:1e-3);
                outfile.WriteLine($"{acc} {energy} {-0.5}");
            }

            outfile.WriteLine("\n");

            for(double eps=0.01; eps >= 1e-12; eps-=5e-5){
                energy = solver(r_min, r_max, acc:1e-3, eps: eps);
                outfile.WriteLine($"{eps} {energy} {-0.5}");
            }


        }
    }

}