#include <stdio.h>
#include <stdlib.h>
#include "write_data.h"


int main(int argc, char const *argv[]){
    fprintf(stdout, "x\tcos(x)\tsin(x)\n");

    for (int i = 1; i < argc; ++i){
        write_data(strtod(argv[i], NULL), stdout);
    }
    return 0;
}