//
// Created by mads on 15/03/2022.
//
#ifndef MATLIB_GRAM_SCHMIDT_HPP
#define MATLIB_GRAM_SCHMIDT_HPP


#include "matrix.hpp"

namespace gram_schmidt {
    void decompose(Matrix *A, Matrix *R);

    void solve(Matrix *Q, Matrix *R, ColumnVector* b, ColumnVector* x);

    void inverse(Matrix* Q, Matrix* R, Matrix* B);
}

#endif //MATLIB_GRAM_SCHMIDT_HPP
