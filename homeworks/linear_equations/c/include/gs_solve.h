#ifndef GS_SOLVE
#define GS_SOLVE 

#include "matrix.h"

void GS_decomp(matrix* A, matrix* R);

void GS_solve(matrix* Q, matrix* R, vector* b, vector* x);


#endif