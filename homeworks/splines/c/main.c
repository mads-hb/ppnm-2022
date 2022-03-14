#include <stdlib.h>
#include <stdio.h>  
#include "linterp.h"
#include "qspline.h"
#include "cspline.h"
#include <gsl/gsl_spline.h>

#define NUM_DATAPOINTS 9


/** Read a txt file with two columns seperated by tab and read all the items 
 * into the arrays x and y.
 **/
void read_data(double *x, double *y){
    FILE *infile = fopen("xy_data.txt", "r");
    for (int i = 0; i < NUM_DATAPOINTS; ++i){
        int items = fscanf(infile, "%lf\t%lf\n", &x[i], &y[i]);
        if (items == EOF){
            break;
        }
    }
    fclose(infile);
}

void exerciseA(){
    FILE *outfile = fopen("out1.txt", "w");
    double *x, *y;
    x = (double *) malloc(sizeof(double *) * NUM_DATAPOINTS);
    y = (double *) malloc(sizeof(double *) * NUM_DATAPOINTS);
    read_data(x, y);

    gsl_interp_accel *acc = gsl_interp_accel_alloc ();
    gsl_spline *spline = gsl_spline_alloc (gsl_interp_linear, NUM_DATAPOINTS);

    gsl_spline_init (spline, x, y, NUM_DATAPOINTS);

    double val = 0.1;
    do {
        double interp_val = linterp(NUM_DATAPOINTS, x, y, val);
        double interp_val_integ = linterp_integ(NUM_DATAPOINTS, x, y, val);
        double y_gsl = gsl_spline_eval(spline, val, acc);
        fprintf(outfile, "%f\t%f\t%f\t%f\n", val, interp_val, interp_val_integ, y_gsl);
        val += 0.1;
    } while (val < 8);
    gsl_spline_free (spline);
    gsl_interp_accel_free (acc);
    free(x), free(y);
    fclose(outfile);
}


void exerciseB(){
    FILE *outfile = fopen("out2.txt", "w");
    double *x, *y;
    x = (double *) malloc(sizeof(double *) * NUM_DATAPOINTS);
    y = (double *) malloc(sizeof(double *) * NUM_DATAPOINTS);
    read_data(x, y);

    double val = 0.1;
    do {
        double interp_val = qspline_eval(NUM_DATAPOINTS, x, y, val);
        double interp_val_integ = qspline_integ(NUM_DATAPOINTS, x, y, val);
        double interp_val_deriv = qspline_deriv(NUM_DATAPOINTS, x, y, val);
        fprintf(outfile, "%f\t%f\t%f\t%f\n", val, interp_val, interp_val_integ, interp_val_deriv);
        val += 0.1;
    } while (val < 8);
    free(x), free(y);
    fclose(outfile);
}


void exerciseC(){
    FILE *outfile = fopen("out3.txt", "w");
    double *x, *y;
    x = (double *) malloc(sizeof(double *) * NUM_DATAPOINTS);
    y = (double *) malloc(sizeof(double *) * NUM_DATAPOINTS);
    read_data(x, y);

    gsl_interp_accel *acc = gsl_interp_accel_alloc ();
    gsl_spline *spline = gsl_spline_alloc (gsl_interp_cspline, NUM_DATAPOINTS);

    gsl_spline_init (spline, x, y, NUM_DATAPOINTS);

    double val = 0.1;
    do {
        double interp_val = cspline_eval(NUM_DATAPOINTS, x, y, val);
        double interp_val_integ = cspline_integ(NUM_DATAPOINTS, x, y, val);
        double y_gsl = gsl_spline_eval(spline, val, acc);
        fprintf(outfile, "%f\t%f\t%f\t%f\n", val, interp_val, interp_val_integ, y_gsl);
        val += 0.1;
    } while (val < 8);
    gsl_spline_free (spline);
    gsl_interp_accel_free (acc);
    free(x), free(y);
    fclose(outfile);
}


int main(){
    exerciseA();
    exerciseB();
    exerciseC();    
    return 0;
}