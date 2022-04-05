using static System.Console;
using static System.Math;

static class MainProgram{

    public static int Main(){
        // Run all individual parts of the homework
        WriteLine("Test for convergence when increasing rmax. Check rmax.png");
        rmax_convergence();
        WriteLine("Test for convergence when increasing N. Check npoints.png");
        npoints_convergence();
        WriteLine("Draw wavefunctions and analytical wavefunctions from Griffiths. Check exerciseBplot.png");
        wavefunctions();

        return 0;
    }


    private static matrix build_hamiltonian(int npoints, double rmax, double dr){
        vector r = new vector(npoints);
        for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
        matrix H = new matrix(npoints,npoints);
        for(int i=0;i<npoints-1;i++){
          matrix.set(H,i,i,-2);
          matrix.set(H,i,i+1,1);
          matrix.set(H,i+1,i,1);
          }
        matrix.set(H,npoints-1,npoints-1,-2);
        H*=-0.5/dr/dr;
        for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i];
        return H;
    }

    private static void rmax_convergence(){
        using(var outfile = new System.IO.StreamWriter("rmax.txt")){
            for(int r_max = 2; r_max < 30; r_max++){
                double dr=0.2;
                int npoints = (int)(r_max/dr-1);
                
                matrix H = build_hamiltonian(npoints, r_max, dr);
                var D = H.copy();
                matrix V = new matrix(H.size1, H.size2);
                Jacobi.diag(D, V);

                outfile.WriteLine($"{r_max} {D[0,0]} {D[1,1]} {D[2,2]}");
            }       
        }
    }

    private static void wavefunctions(){
        int r_max = 30;
        double dr=0.2;
        int npoints = (int)(r_max/dr-1);

        matrix H = build_hamiltonian(npoints, r_max, dr);
        var D = H.copy();
        matrix V = new matrix(H.size1, H.size2);
        Jacobi.diag(D, V);

        using(var outfile = new System.IO.StreamWriter("eigenfunc.txt")){
            // Step between 0 and r_max in npoints steps with stepsize dr. 
            for(int i = 0; i<npoints; i++){
                outfile.WriteLine($"{dr*(i+1)} {V[0][i]/Sqrt(dr)} {-V[1][i]/Sqrt(dr)} {V[2][i]/Sqrt(dr)}");
            }
        }
        using(var outfile = new System.IO.StreamWriter("analytical.txt")){
            for(double r = 0; r<r_max; r+=0.01){
                double R1 = r*2*Exp(-r);
                double R2 = r*1.0/Sqrt(2)*(1-1.0/2*r)*Exp(-r/2);
                double R3 = r*2.0/(3*Sqrt(3))*(1-2.0/3*r+2.0/27*r*r)*Exp(-r/3);
                outfile.WriteLine($"{r} {R1} {R2} {R3}");
            }
        }       
    }

    private static void npoints_convergence(){
        using(var outfile = new System.IO.StreamWriter("npoints.txt")){
            for(int npoints = 5; npoints < 100; npoints++){
                double rmax=40;
                double dr = rmax/(npoints-1);
                
                matrix H = build_hamiltonian(npoints, rmax, dr);
                var D = H.copy();
                matrix V = new matrix(H.size1, H.size2);
                Jacobi.diag(D, V);

                outfile.WriteLine($"{npoints} {D[0,0]} {D[1,1]} {D[2,2]}");
            }       
        }
    }

}