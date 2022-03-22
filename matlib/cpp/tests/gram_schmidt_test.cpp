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

    TEST(TestSolve){
        int N = 10;
        Matrix A = Matrix::random_matrix(N, N);
        auto *R = new Matrix(N, N);
        auto Q = A.copy();
        gram_schmidt::decompose(&Q, R);

        ColumnVector b = ColumnVector::random_vector(N);
        ColumnVector *x = new ColumnVector(N);

        gram_schmidt::solve(&Q, R, &b, x);

        Matrix b2 = (A * (*x));
        CHECK(b.get_length() == b2.get_length() && b.get_height() == b2.get_height());
        ColumnVector v_b2 = b2.get_column(0);
        for (int i = 0; i < b.get_height(); ++i) {
            CHECK_CLOSE(b.get(i), v_b2.get(i), 1e8);
        }
    }

    TEST_FIXTURE(MatrixFixture, TestInverse){
        Matrix *B = gram_schmidt::inverse(A);
        CHECK_EQUAL(B->get(0,0), -1.5);
        CHECK_EQUAL(B->get(0,1), 0.5);
        CHECK_EQUAL(B->get(1,0), 1);
        CHECK_EQUAL(B->get(1,1), 0);
    }

    TEST(TestInverseShape){
        Matrix A = Matrix::random_matrix(10, 10);
        Matrix *B = gram_schmidt::inverse(&A);
        CHECK(B->get_length() == 10 && B->get_height());
    }

    TEST(TestInverseProduct){
        int N = 10;
        auto A = Matrix::random_matrix(N, N);
        auto Q = A.copy();
        Matrix B(N);
        auto *R = new Matrix(N);
        gram_schmidt::decompose(&Q, R);
        gram_schmidt::inverse(&Q, R, &B);
        Matrix C = (B*A);

        // Check product is identity;
        for (int i = 0; i < R->get_height(); ++i) {
            for (int j = 0; j < R->get_length(); ++j) {
                if (j == i) {
                    // Upper traingular part
                            CHECK_CLOSE(C.get(i, j), 1, 1e8);
                } else {
                            CHECK_CLOSE(C.get(i, j), 0, 1e8);
                }
            }
        }

        // Check commutation
        Matrix C2 = A*B;
        for (int i = 0; i < R->get_height(); ++i) {
            for (int j = 0; j < R->get_length(); ++j) {
                CHECK_CLOSE(C2.get(i,j), C.get(i,j), 1e8);
            }
        }
    }


}