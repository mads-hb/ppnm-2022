#include <stdio.h>
#include <stdlib.h>
#include <math.h>


void write_data(double x, FILE *stream){
    double c = cos(x);
    double s = sin(x);
    fprintf(stream, "%f\t%f\t%f\n", x,c,s);
}