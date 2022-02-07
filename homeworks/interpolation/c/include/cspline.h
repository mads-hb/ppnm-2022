#ifndef CSPLINE
#define CSPLINE

/** Implement a function that interpolates the given tabulated function, 
 * {x[i], y[i]}, at a given point z using cubic spline interpolation.
 **/
double cspline_eval(int n, double x[], double y[], double z);

/** Implement a function that calculates the integral of the the given 
 * tabulated function, {x[i], y[i]}, from x[0] to the given point z.
 **/
double cspline_integ(int n, double x[], double y[], double z);

/** Implement a function that calculates the derivative of the the given 
 * tabulated function, {x[i], y[i]}, from x[0] to the given point z.
 **/
double cspline_deriv(int n, double x[], double y[], double z);

#endif