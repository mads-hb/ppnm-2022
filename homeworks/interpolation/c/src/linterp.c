#include <assert.h>
#include <stdlib.h>
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



double linterp(int n, double *x, double *y, double z){
    // printf("n=%i, z=%f, x[0]=%f, x[n-1]=%f\n", n, z, x[0], x[n-1]);
    assert (n > 1 && z >= x[0] && z <= x[n-1]);
    int i = binsearch(n, x, z);
    double dy = y[i+1] - y[i], dx = x[i+1] - x[i]; 
    assert(dx > 0);
    return y[i] + dy/dx * (z - x[i]);
}


double linterp_integ(int n, double *x, double *y, double z){
    assert (n > 1 && z >= x[0] && z <= x[n-1]);
    int i = 0;
    double integral = 0, dy, dx, p;
    while (z > x[i+1]){
        dy = y[i+1] - y[i];
        dx = x[i+1] - x[i];
        integral += y[i]*dx + 0.5 * dy * dx;
        i++;
    }
    p = (y[i+1] - y[i]) / (x[i+1] - x[i]);
    dx = z-x[i];
    integral += y[i] * dx + 0.5 * p * pow(dx, 2);
    return integral;
}