#include <stdlib.h>
#include <assert.h>
#include <math.h>


typedef struct{
    int n;
    double *x, *y, *b, *c;
} qspline;


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


static qspline* qsplinealloc(int n, double *x, double *y){//buildsqspline
    qspline *s = (qspline*) malloc(sizeof(qspline));//spline
    s->b = (double*) malloc((n-1) * sizeof(double));//bi
    s->c = (double*) malloc((n-1) * sizeof(double));//ci
    s->x = (double*) malloc(n * sizeof(double));//xi
    s->y = (double*) malloc(n * sizeof(double));//yi
    s->n = n;
    for (int i=0; i<n; i++){
        s->x[i] = x[i];
        s->y[i] = y[i];
    }
    int i;
    double p[n-1], h[n-1];//VLAfromC99
    for (i=0; i<n-1; i++){
        h[i] = x[i+1] - x[i];
        p[i] = (y[i+1]-y[i]) / h[i];
    }
    s->c[0] = 0; //recursionup:
    for (i=0; i<n-2; i++){
        s->c[i+1] = (p[i+1] - p[i] - s->c[i] * h[i]) / h[i+1];
    }
    s->c[n-2] /= 2;//recursiondown:
    for (i=n-3; i>=0; i--){
        s->c[i] = (p[i+1] - p[i] - s->c[i+1] * h[i+1]) / h[i];
    }
    for(i=0; i<n-1; i++){
        s->b[i] = p[i] - s->c[i] * h[i];
    }
    return s;
}

static void qsplinefree(qspline *s){//freetheallocatedmemory
    free(s->x);
    free(s->y);
    free(s->b);
    free(s->c);
    free(s);
}

double qspline_eval(int n, double *x, double *y, double z){
    qspline *q = qsplinealloc(n, x, y);

    int i = binsearch(n, q->x, z);
    double h = z - q->x[i];
    double yi = q->y[i];
    double bi = q->b[i];
    double ci = q->c[i];
    double val = yi + h * (bi + h * ci);

    qsplinefree(q);
    return val;
}

double qspline_deriv(int n, double *x, double *y, double z){
    qspline *q = qsplinealloc(n, x, y);

    int i = binsearch(n, q->x, z);
    double h = z - q->x[i];
    double bi = q->b[i];
    double ci = q->c[i];
    double val = bi + 2*ci*h;

    qsplinefree(q);
    return val;
}



double qspline_integ(int n, double *x, double *y, double z){
    assert (n > 1 && z >= x[0] && z <= x[n-1]);
    
    qspline *q = qsplinealloc(n, x, y);
    
    int j = binsearch(n, q->x, z);
    double integral = 0;
    double h, yi, bi, ci;
    for (int i = 0; i < j; ++i){
        h = q->x[i+1] - q->x[i];
        yi = q->y[i];
        bi = q->b[i];
        ci = q->c[i];
        integral += yi * h + 0.5 * bi * pow(h,2) + 1./3 * ci * pow(h,3);
    }
    h = z - q->x[j];
    yi = q->y[j];
    bi = q->b[j];
    ci = q->c[j];
    integral += yi * h + 0.5 * bi * pow(h,2) + 1./3 * ci * pow(h,3);
    
    qsplinefree(q);    
    return integral;
}
