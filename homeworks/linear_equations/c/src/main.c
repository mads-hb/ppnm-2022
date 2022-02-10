#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <math.h>
#include "matrix.h"
#include <gs_solve.h>



/* generate a random floating point number from min to max */
double randfrom(double min, double max) {
    double range = (max - min); 
    double div = RAND_MAX / range;
    return min + (rand() / div);
}


void populate_matrix(matrix *A){
    srand(time(0));
    for (int i = 0; i < A->size1; ++i){
        for (int j = 0; j < A->size2; ++j){
            double r = randfrom(-10, 10);
            matrix_set(A, i, j, r);
        }
    }
}


void populate_vector(vector *v){
    for (int i = 0; i < v->size; ++i){
        double r = randfrom(-10, 10);
        vector_set(v, i, r);
    }
}


void exerciseA1(){
    int n = 10, m = 7;
    matrix *A = matrix_alloc(n, m);
    populate_matrix(A);
    // printf("Printing A\n");
    // matrix_print(A);
    matrix *R = matrix_alloc(m, m);
    printf("Matrix A before decomposition:\n");
    matrix_print(A);
    GS_decomp(A, R);
    printf("Matrix A after decomposition:\n");
    matrix_print(A);
    printf("Matrix R after decomposition:\n");
    matrix_print(R);
    printf("Product QR:\n");
    matrix *C = matrix_alloc(A->size1, R->size2);
    matrix_product(A, R, C);
    matrix_print(C);

    printf("Product QT*Q:\n");
    matrix *QT = matrix_alloc(A->size2, A->size1);
    matrix *C2 = matrix_alloc(QT->size1, A->size2);
    matrix_transpose(A, QT);
    matrix_product(QT, A, C2);
    matrix_print(C2);
    
    matrix_free(A); matrix_free(R); matrix_free(C); matrix_free(QT); matrix_free(C2);
}


void exerciseA2(){
    // Create a square matrix
    int n = 9;
    matrix *A = matrix_alloc(n, n);
    matrix *R = matrix_alloc(n, n);
    populate_matrix(A);
    vector *x = vector_alloc(n);
    vector *b = vector_alloc(n);
    populate_vector(b);

    printf("Matrix A before decomposition:\n");
    matrix_print(A);
    printf("Vector b\n");
    vector_print(b);

    printf("Matrix Q after decomposition:\n");
    GS_decomp(A, R);
    matrix_print(A);
    printf("Matrix R after decomposition:\n");
    matrix_print(R);


    printf("Solving Ax=b for x... x is:\n");
    GS_solve(A, R, b, x);
    vector_print(x);

    printf("Let's find A*x that should equal b:\n");
    // Currently, the matrix A holds Q. Let's multiply with R to get original A.
    matrix *C = matrix_alloc(A->size1, R->size2);
    matrix_product(A, R, C);

    // Calculate the matrix product A * x and place result in vector b2.
    vector *b2 = vector_alloc(n);
    for (int i = 0; i < n; ++i){
        double val = 0;
        for (int j = 0; j < n; ++j){
            val += matrix_get(C, i, j) * vector_get(x, j);
        }
        vector_set(b2, i, val);
    }
    vector_print(b2);

    
    matrix_free(A); matrix_free(R); matrix_free(C); vector_free(x); vector_free(b); vector_free(b2);
}


void exerciseB(){
    int n = 9;
    matrix *A = matrix_alloc(n, n);
    matrix *R = matrix_alloc(n, n);
    populate_matrix(A);
    matrix_print(A);

    printf("Matrix Q after decomposition:\n");
    GS_decomp(A, R);
    matrix_print(A);
    printf("Matrix R after decomposition:\n");
    matrix_print(R);

    printf("The inverse matrix is:\n");
    matrix *B = matrix_alloc(n, n);
    GS_inverse(A, R, B);
    matrix_print(B);


    printf("Let's compute AB:\n");
    // Currently, the matrix A holds Q. Let's multiply with R to get original A.
    matrix *A2 = matrix_alloc(A->size1, R->size2);
    matrix_product(A, R, A2);

    matrix *C = matrix_alloc(A2->size1, A2->size2);
    matrix_product(A2, B, C);
    matrix_print(C);
    matrix_product(B, A2, C);
    matrix_print(C);
    printf("... they both equal the identity.\n");


    matrix_free(A), matrix_free(R), matrix_free(B); matrix_free(A2); matrix_free(C);
}


void exerciseC(){
    int i = 1;
    FILE *outfile = fopen("timing.txt", "w");
    while (i<=300){
        int n = i;
        matrix *A = matrix_alloc(n, n);
        matrix *R = matrix_alloc(n, n);
        populate_matrix(A);
        clock_t begin = clock();
        GS_decomp(A, R);
        clock_t end = clock();
        double timing = (double)(end - begin) / CLOCKS_PER_SEC;;
        matrix_free(A), matrix_free(R);
        double fit = pow(i, 3) * 0.00000000345;
        fprintf(outfile, "%d\t%f\t%f\n", i, timing, fit);
        i++;
    }
    fclose(outfile);
}

int main(){
    printf("***** Exercise A1 *****\n\n");
    exerciseA1();
    printf("\n\n***** Exercise A2 *****\n\n");
    exerciseA2();
    printf("\n\n***** Exercise B *****\n\n");
    exerciseB();
    printf("\n\n***** Exercise C *****\n\n");
    exerciseC();
    return 0;
}