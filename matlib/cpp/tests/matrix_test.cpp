#include <unittest++/UnitTest++.h>
#include "matrix.hpp"


SUITE(MatrixTest) {

    class SquareMatrixFixture{
    public:
        SquareMatrixFixture(){
            identity = new Matrix(size);
            for (int i = 0; i < size; ++i) {
                identity->set(i, i, 1);
            }
            zero = new Matrix(size);
            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < size; ++j) {
                    zero->set(i, i, 0);
                }
            }
        }
        ~SquareMatrixFixture(){
            delete identity;
            delete zero;
        }

        int size = 4;
        Matrix *identity;
        Matrix *zero;
    };

    TEST_FIXTURE(SquareMatrixFixture, TestZero) {
        Matrix A = (*identity) * (*zero);
        CHECK(!((*identity) == (*zero)));
        CHECK(A == (*zero));
    }

    TEST_FIXTURE(SquareMatrixFixture, TestIdentity) {
        Matrix A = Matrix::random_matrix(size, size);
        CHECK(!(A == (*identity)));
        CHECK(A * (*identity) == A);
    }

    TEST_FIXTURE(SquareMatrixFixture, TestGetters) {
        CHECK(identity->get_length() == size);
        CHECK(identity->get_height() == size);
    }

    TEST_FIXTURE(SquareMatrixFixture, TestCopy) {
        // Check whether we get a new instance
        Matrix A = zero->copy();
        CHECK(A == *zero);
        A.set(0,0,1);
        CHECK(!(A == *zero));

        // Check whether we are changing the instance through a reference.
        Matrix *B = zero;
        CHECK(*B == *zero);
        B->set(0,0,1);
        CHECK(*B == *zero);
    }

    TEST_FIXTURE(SquareMatrixFixture, TestGet) {
        CHECK(identity->get(0,0) == 1);
        CHECK(identity->get(0,1) == 0);
    }

    TEST_FIXTURE(SquareMatrixFixture, TestSet) {
        Matrix A = identity->copy();
        A.set(0,0, 1);
        CHECK(A == *identity);
        A.set(0,0, 2);
        CHECK(!(A == *identity));
    }
}