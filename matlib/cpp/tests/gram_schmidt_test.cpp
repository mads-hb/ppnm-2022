#include <unittest++/UnitTest++.h>
#include "matrix.hpp"
#include "gram_schmidt.hpp"


SUITE(GramSchmidtTest) {

    class MatrixFixture{
    public:
        MatrixFixture(){
            A = new Matrix(2);
            A->set(0,0, 0);
            A->set(0,1, 1);
            A->set(1,0, 2);
            A->set(1,1, 3);
        }
        ~MatrixFixture(){
            delete A;
        }

        Matrix *A;
    };


    TEST_FIXTURE(MatrixFixture, TestDecomposeValue) {
        // Multiply with
        auto *R = new Matrix(A->get_length(), A->get_length());
        gram_schmidt::decompose(A, R);
        CHECK(A->get(0,0) == 0);
        CHECK(A->get(0,1) == 1);
        CHECK(A->get(1,0) == 1);
        CHECK(A->get(1,1) == 0);

        CHECK(R->get(0,0) == 2);
        CHECK(R->get(0,1) == 3);
        CHECK(R->get(1,0) == 0);
        CHECK(R->get(1,1) == 1);
    }

    TEST(TestDecomposeShape){
        // Random tall matrix
        Matrix A = Matrix::random_matrix(10, 7);
        auto *R = new Matrix(A.get_length(), A.get_length());
        gram_schmidt::decompose(&A, R);

        // Check whether R is upper triangular
        for (int i = 0; i < R->get_height(); ++i) {
            for (int j = 0; j < R->get_length(); ++j) {
                if (j >= i) {
                    // Upper traingular part
                    CHECK(R->get(i, j) != 0);
                } else {
                    CHECK_EQUAL(R->get(i, j), 0);
                }
            }
        }
    }

    TEST(TestDecomposeOrthogonal){
        // Random tall matrix
        Matrix A = Matrix::random_matrix(10, 7);
        auto *R = new Matrix(A.get_length(), A.get_length());
        auto Q = A.copy();
        gram_schmidt::decompose(&Q, R);

        // Check if QT * Q is identity.
        Matrix QT = Q.transpose();
        for (int i = 0; i < R->get_height(); ++i) {
            for (int j = 0; j < R->get_length(); ++j) {
                if (j == i) {
                    // Upper traingular part
                    CHECK_CLOSE(R->get(i, j), 1, 1e8);
                } else {
                    CHECK_CLOSE(R->get(i, j), 0, 1e8);
                }
            }
        }
    }

    TEST(TestDecomposeEqual){
        // Random tall matrix
        Matrix A = Matrix::random_matrix(10, 7);
        auto *R = new Matrix(A.get_length(), A.get_length());
        auto Q = A.copy();
        gram_schmidt::decompose(&Q, R);

        // Check if QT * Q is identity.
        Matrix A2 = Q*(*R);
        CHECK_EQUAL(A.get_length(), A2.get_length());
        CHECK_EQUAL(A.get_height(), A2.get_height());
        for (int i = 0; i < A.get_height(); ++i) {
            for (int j = 0; j < A.get_length(); ++j) {
                CHECK_CLOSE(A.get(i, j), A2.get(i, j), 1e8);
            }
        }
    }
}