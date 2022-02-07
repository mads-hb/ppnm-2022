#include <stdlib.h>
#include <assert.h>
#include <stdio.h>
#include <math.h>

/** Binary search algorithm to find the location of the index i of the 
 * interval containing z, x[i] < z < x[i+1].
 * @param n is the number of datapoints
 * @param x is a pointer to array
 * @param z is the value to search for.
 **/
static int binsearch(int n, double *x, double z){
    assert(n > 1 && x[0] <= z && z <= x[n-1]);
    int i = 0, j = n - 1;
    while(j - i > 1){
        int mid = (i + j) / 2;
        if(z > x[mid]){
            i = mid;
        } else {
            j = mid;
        }
    }
    return i;
}


typedef struct{
    int n;
    double *x, *y, *b, *c, *d;
} cspline;


cspline* csplinealloc(int n, double *x, double *y){//buildsnaturalcspline
    cspline *s = (cspline *) malloc(sizeof(cspline));
    s->x = (double *) malloc(n*sizeof(double));
    s->y = (double *) malloc(n*sizeof(double));
    s->b = (double *) malloc(n*sizeof(double));
    s->c = (double *) malloc((n-1)*sizeof(double));
    s->d = (double *) malloc((n-1)*sizeof(double));
    s->n = n;
    for(int i=0; i<n; i++){
        s->x[i] = x[i];
        s->y[i]=y[i];
    }
    double h[n-1], p[n-1];//VLA
    for(int i=0; i<n-1; i++){
        h[i] = x[i+1] - x[i];
        assert(h[i] > 0);
    }
    for(int i=0; i<n-1; i++){
        p[i] = (y[i+1] - y[i]) / h[i];}
    double D[n], Q[n-1], B[n];//buildingthetridiagonalsystem:
    D[0] = 2;
    for(int i=0; i<n-2; i++){
        D[i+1] = 2 * h[i] / h[i+1] + 2;
        D[n-1] = 2;
    }
    Q[0] = 1;
    for(int i=0; i<n-2; i++){
        Q[i+1] = h[i] / h[i+1];
    }
    for(int i=0; i<n-2; i++){
        B[i+1] = 3 * (p[i] + p[i+1] * h[i] / h[i+1]);
    }
    B[0] = 3 * p[0];
    B[n-1] = 3 * p[n-2];//Gausselimination:
    for(int i=1; i<n; i++){
        D[i] -= Q[i-1] / D[i-1];
        B[i] -= B[i-1] / D[i-1];
    }
    s->b[n-1] = B[n-1] / D[n-1];//back-substitution:
    for(int i=n-2; i>=0; i--){
        s->b[i] = (B[i] - Q[i] * s->b[i+1]) / D[i];
    }
    for(int i=0; i<n-1; i++){
        s->c[i] = (-2 * s->b[i] - s->b[i+1] + 3 * p[i]) / h[i];
        s->d[i]=(s->b[i] + s->b[i+1] - 2 * p[i]) / h[i] / h[i];
    }
    return s;
}


void csplinefree(cspline *s){//freetheallocatedmemory
    free(s->x);
    free(s->y);
    free(s->b);
    free(s->c);
    free(s->d);
    free(s);
}


double cspline_eval(int n, double *x, double *y, double z){
    cspline *s = csplinealloc(n, x, y);
    assert(z >= s->x[0] && z <= s->x[s->n-1]);
    int i = binsearch(n, x, z);
    double h = z-s->x[i];//calculatetheinerpolatingspline:
    double val = s->y[i] + h * (s->b[i] + h * (s->c[i] + h * s->d[i]));
    csplinefree(s);
    return val;
}


double cspline_deriv(int n, double x[], double y[], double z){
    cspline *s = csplinealloc(n, x, y);
    assert(z >= s->x[0] && z <= s->x[s->n-1]);
    int i = binsearch(n, x, z);
    double h = z-s->x[i];
    double bi = s->b[i];
    double ci = s->c[i];
    double di = s->d[i];
    double val = bi + 2*ci*h + 3*di*pow(h, 2);
    csplinefree(s);

    return val;
}


double cspline_integ(int n, double x[], double y[], double z){
    cspline *s = csplinealloc(n, x, y);
    int j = binsearch(n, x, z);
    double integral = 0;
    double h, yi, bi, ci, di;
    for(int i = 0; i < j; i++){
        h = s->x[i+1] - s->x[i];
        yi = s->y[i];
        bi = s->b[i];
        ci = s->c[i];
        di = s->d[i];
        integral += yi * h + 0.5 * bi * pow(h,2) + 1./3 * ci * pow(h,3) + 1./4 * di * pow(h,4);
    }
    h = z - s->x[j];
    yi = s->y[j];
    bi = s->b[j];
    ci = s->c[j];
    di = s->d[j];   
    integral += yi * h + 0.5 * bi * pow(h,2) + 1./3 * ci * pow(h,3) + 1./4 * di * pow(h,4);
    return integral;
}
