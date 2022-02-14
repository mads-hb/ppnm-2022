#ifndef LINTERP
#define LINTERP

/** Implement a function that interpolates the given tabulated function, 
 * {x[i], y[i]}, at a given point z using linear spline interpolation.
 **/
double linterp(int n, double x[], double y[], double z);

/** Implement a function that calculates the integral of the the given 
 * tabulated function, {x[i], y[i]}, from x[0] to the given point z.
 **/
double linterp_integ(int n, double x[], double y[], double z);

#endif