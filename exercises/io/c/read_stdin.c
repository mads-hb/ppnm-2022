#include <stdio.h>
#include <stdlib.h>
#include "write_data.h"

int main(){
    double x;
    fprintf(stdout, "x\tcos(x)\tsin(x)\n");
    do {
        int items=scanf("%lg",&x);
        if(items==EOF){
            break;
        }
        write_data(x, stdout);
    } while(1);
    return 0;
}