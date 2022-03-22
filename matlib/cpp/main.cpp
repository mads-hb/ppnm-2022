#include <iostream>
#include "matrix.hpp"
#include "gram_schmidt.hpp"


int main(int argc, char const *argv[])
{
    int N = 10;
    auto A = Matrix::random_matrix(N, N);
    auto Q = A.copy();
    Matrix B(N);
    auto *R = new Matrix(N);
    gram_schmidt::decompose(&Q, R);
    gram_schmidt::inverse(&Q, R, &B);
    (B*A).print();
    return EXIT_SUCCESS;
}