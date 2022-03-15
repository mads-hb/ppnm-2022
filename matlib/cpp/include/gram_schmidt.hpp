//
// Created by Mads Hansen Baattrup on 15/03/2022.
//
#ifndef MATLIB_GRAM_SCHMIDT_HPP
#define MATLIB_GRAM_SCHMIDT_HPP


#include "matrix.hpp"

namespace gram_schmidt {
    /**
     * Decompose a matrix, A = QR, by performing in-place Gram-Schmidt orthogonalization. On exit, the matrix A is replaced
     * with the orthogonal matrix Q. The algorithm requires that n>=m for A i.e. A is a tall matrix.
     * @param A The matrix to decompose of size [n, m].
     * @param R A matrix of size [m, m]. On exit, it holds the computed R matrix that is upper triangular.
     */
    void decompose(Matrix *A, Matrix *R);

    /**
     * Solve the equation Ax=b by using we can write A=QR and that Q is orthogonal to obtain: Rx = Q_T * b, with Q_T
     * being the decomposed matrix transposed. Afterwards perform backwards substitution to obtain solution to system.
     * The algorithm requires that n=m for A i.e. A is a square matrix.
     * @param Q The decomposed matrix. Must be a square matrix.
     * @param R The upper triangular matrix.
     * @param b The right hand side of the equation.
     * @param x On exit, replaced with the solution to the system of equations.
     */
    void solve(Matrix *Q, Matrix *R, ColumnVector* b, ColumnVector* x);

    /**
     * Calculates the inverse of the matrix A into the matrix B. Takes as parameters the decomposed version of A: A=QR.
     * The algorithm requires that n=m for A i.e. A is a square matrix.
     * @param Q The decomposed matrix. Must be a square matrix.
     * @param R The upper triangular matrix.
     * @param B A square matrix of size n. On exit, it is replaced with the inverse of A.
     */
    void inverse(Matrix* Q, Matrix* R, Matrix* B);
}

#endif //MATLIB_GRAM_SCHMIDT_HPP
