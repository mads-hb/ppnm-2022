#include <stdio.h>
#include <stdlib.h>
#include "write_data.h"

int main(int argc, char const *argv[]){
    if (argc != 3){
        fprintf(stderr, "Wrong number of arguments supplied.\n");
        return -1;
    }
    double x;
    FILE *infile, *outfile;
    infile = fopen(argv[1], "r"), 
    outfile = fopen(argv[2], "w");
    fprintf(outfile, "x\tcos(x)\tsin(x)\n");
    do {
        int items=fscanf(infile, "%lg", &x);
        if(items==EOF){
            break;
        }
        write_data(x, outfile);
    } while(1);
    return 0;
}