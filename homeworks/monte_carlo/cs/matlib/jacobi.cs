using System;
using static System.Math;
using static System.Console;

public static class Jacobi {

    /**
     * Jacobi rotation multiplied from right.
     */
    private static void timesJ(matrix A, int p, int q, double phi){
        double c = Cos(phi), s = Sin(phi);
        for(int i=0; i < A.size1; i++){
            double A_ip = A[i,p];
            double A_iq = A[i,q];
            A[i,p] = c*A_ip - s*A_iq;
            A[i,q] = s*A_ip + c*A_iq;
        }
    }

    /**
     * Jacobi rotation multiplied from left.
     */
    private static void Jtimes(matrix A, int p, int q, double phi){
        double c = Cos(phi), s = Sin(phi);
        for(int i=0; i<A.size1; i++){
            double A_pi = A[p,i];
            double A_qi = A[q,i];
            A[p,i] = c*A_pi + s*A_qi;
            A[q,i] = -s*A_pi + c*A_qi;
        }
    }

    /**
     * Diagonalize matrix A.
     */
    public static int diag(matrix A, matrix V, bool optimize=false){
        if (optimize) {return diag_optimize(A, V);}
        else {return diag_normal(A, V);}
    }

    /**
     * Diagonalize matrix A.
     */
    private static int diag_normal(matrix A, matrix V){
        int sweeps = 0;
        int n = A.size1;
        V.set_identity();
        bool has_changed;

        do{
            sweeps++;
            has_changed = true;
            for(int p=0; p<n-1; p++){
                for(int q=p+1; q<n; q++){
                    double A_pq = A[p,q];
                    double A_pp = A[p,p];
                    double A_qq = A[q,q];
                    double phi = 0.5 * Atan2(2*A_pq, A_qq - A_pp);
                    double c = Cos(phi);
                    double s = Sin(phi);
                    double A_pp_n = c*c*A_pp - 2*s*c*A_pq + s*s*A_qq;
                    double A_qq_n = s*s*A_pp + 2*s*c*A_pq + c*c*A_qq;
                    if(A_pp_n != A_pp || A_qq_n != A_qq){
                        has_changed = false;
                        // Values of A are given in eq. 10 and corresponds to
                        // multiplying with rotation J on both sides of A.
                        timesJ(A, p, q, phi);
                        Jtimes(A, p, q, -phi);
                        // Values of V are given by rotation V*J
                        timesJ(V, p, q, phi);
                    }
                }
            }
        } while(!has_changed);  
        return sweeps;
    }


    /**
     * Diagonalize matrix A.
     */
    private static int diag_optimize(matrix A, matrix V){
        int sweeps = 0;
        int n = A.size1;
        V.set_identity();
        bool has_changed;

        do{
            sweeps++;
            has_changed = true;
            for(int p=0; p<n-1; p++){
                for(int q=p+1; q<n; q++){
                    double A_pq = A[p,q];
                    double A_pp = A[p,p];
                    double A_qq = A[q,q];
                    double phi = 0.5 * Atan2(2*A_pq, A_qq - A_pp);
                    double c = Cos(phi);
                    double s = Sin(phi);
                    double A_pp_n = c*c*A_pp - 2*s*c*A_pq + s*s*A_qq;
                    double A_qq_n = s*s*A_pp + 2*s*c*A_pq + c*c*A_qq;

                    if(A_pp_n != A_pp || A_qq_n != A_qq){
                        has_changed = false;

                        // Update only the upper part of matrix A. Improves runtime of algortihm.
                        A[p,p] = A_pp_n;
                        A[q,q] = A_qq_n;
                        A[p,q] = 0.0;

                        for(int i=0; i<p; i++){
                            double A_ip = A[i,p];
                            double A_iq = A[i,q];
                            A[i,p] = c*A_ip-s*A_iq;
                            A[i,q] = s*A_ip+c*A_iq;
                        }   
                        
                        for(int i=p+1; i<q; i++){
                            double A_pi = A[p,i];
                            double A_iq = A[i,q];
                            A[p,i] = c*A_pi-s*A_iq;
                            A[i,q] = s*A_pi+c*A_iq;
                        }   


                        for(int i=q+1; i<n; i++){
                            double A_pi = A[p,i];
                            double A_qi = A[q,i];
                            A[p,i] = c*A_pi-s*A_qi;
                            A[q,i] = s*A_pi+c*A_qi;
                        }

                        // Values of V are given by rotation V*J
                        timesJ(V, p, q, phi);
                    }
                }
            }

        } while(!has_changed);
        for(int i=0; i<A.size1; i++){
            for(int j=0; j<i; j++){
                A[i,j] = A[j,i];
            }
        }
        return sweeps;
    }
}