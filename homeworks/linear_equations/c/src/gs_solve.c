#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <assert.h>

#include "matrix.h"


void GS_decomp(matrix *A, matrix* R){
    // Create matrix Q
    matrix *Q = matrix_alloc(A->size1, A->size2);

    // Iterate over second dimension of matix - columns.
    for (int i = 0; i < A->size2; ++i){
        // Compute the norm of the column a_i of matrix A.
        double ai_norm = 0;
        for (int j = 0; j < A->size1; ++j){
            ai_norm += pow(matrix_get(A, j, i), 2);
        }
        ai_norm = sqrt(ai_norm);

        // Set the entry R_ii to sqrt(a_i * a_i)
        matrix_set(R, i, i, ai_norm);
        
        // Set the column Q_i to a_i divided by the ai_norm computed before.
        for (int j = 0; j < Q->size1; ++j){    
            double value = matrix_get(A, j, i) / ai_norm;
            matrix_set(Q, j, i, value);
        }

        // Enter second loop
        for (int j = i; j < A->size2; ++j){
            // Compute the product q_i and a_j and set R_ij equal to it.
            double q_dot_a = 0;
            for (int k = 0; k < Q->size1; ++k){
                q_dot_a += matrix_get(Q, k, i) * matrix_get(A, k, j);
            }
            matrix_set(R, i, j, q_dot_a);

            for (int k = 0; k < A->size1; ++k){
                double val = matrix_get(A, k, j) - q_dot_a * matrix_get(Q, k, i);
                matrix_set(A, k, j, val);
            }

        }
    }

    // Copy Q into A
    for (int i = 0; i < A->size1; ++i){
        for (int j = 0; j < A->size2; ++j){
            matrix_set(A, i, j, matrix_get(Q, i, j));
        }
    }
    matrix_free(Q);
}


void GS_solve(matrix* Q, matrix* R, vector* b, vector* x){
    // We know that Rx=QT*b=y. This can be solved by backwards substitution since
    // R is upper triangular.
    matrix *QT = matrix_alloc(Q->size2, Q->size1);
    matrix_transpose(Q, QT);

    // Create a new vector, y, that is equal to QT*b. QT is Q transposed.
    vector *y = vector_alloc(QT->size1);
    for (int i = 0; i < y->size; ++i){
        double val = 0;
        for (int k = 0; k < b->size; ++k){
             val += matrix_get(QT, i, k) * vector_get(b, k);
         }
         vector_set(y, i, val);
    }

    // Do backwards substitution on upper triangular matrix R to find solution
    // vector y
    for (int i = y->size - 1; i >= 0; i--) {
        double sum = vector_get(y, i);
        for (int j = i + 1; j < y->size; j++) {
            sum -= matrix_get(R, i, j)*vector_get(y, j);
        }
        vector_set(y, i, sum/matrix_get(R, i, i));
    }

    // Copy all items from vector y into vector that is visible from outside, 
    // x.
    for (int i = 0; i < x->size; ++i){
        vector_set(x, i, vector_get(y,i));
    }


    // Release memory allocated to y and Q transpose.
    matrix_free(QT); vector_free(y);
}


void GS_inverse(matrix* Q, matrix* R, matrix* B){
    assert(Q->size1 == Q->size2);
    matrix *B2 = matrix_alloc(Q->size1, Q->size1);

    // Define a unit vector
    vector *e_i = vector_alloc(Q->size1);
        for (int j = 0; j < e_i->size; ++j){
            vector_set(e_i, j, 0);
        }
    // Iterate over unit vectors 
    for (int i = 0; i < Q->size2; ++i){
        
        // Set entry i in unit vector to 1 and the rest to zero.
        vector_set(e_i, i, 1);
        if (i>0){
            vector_set(e_i, i-1, 0);
        }
        

        // Solve the equation Ax=e_i. Set the i'th column equal to the solution x
        vector *x_i = vector_alloc(Q->size1); 
        GS_solve(Q, R, e_i, x_i);
        for (int j = 0; j < x_i->size; ++j){
            matrix_set(B2, i, j, vector_get(x_i, j));
        }
        vector_free(x_i);
    }

    // Transpose the the inverse matrix.
    vector_free(e_i);
    matrix_transpose(B2, B);
    matrix_free(B2);
}
