#include "cmath"
#include "memory"
#include "gram_schmidt.hpp"
#include "cassert"


void gram_schmidt::decompose(Matrix *A, Matrix *R) {
    assert(A->get_height()>=A->get_length());
    // Create matrix Q
    auto Q = std::unique_ptr<Matrix>(new Matrix(A->get_length(), A->get_height()));

    // Iterate over second dimension of matrix - columns.
    for (int i = 0; i < A->get_height(); ++i){
        // Compute the norm of the column a_i of matrix A.
        double ai_norm = 0;
        for (int j = 0; j < A->get_length(); ++j){
            ai_norm += pow(A->get(j, i), 2);
        }
        ai_norm = sqrt(ai_norm);

        // Set the entry R_ii to sqrt(a_i * a_i)
        R->set(i, i, ai_norm);

        // Set the column Q_i to a_i divided by the ai_norm computed before.
        for (int j = 0; j < Q->get_length(); ++j){
            double value = A->get(j, i) / ai_norm;
            Q->set(j, i, value);
        }

        // Enter second loop
        for (int j = i; j < A->get_height(); ++j){
            // Compute the product q_i and a_j and set R_ij equal to it.
            double q_dot_a = 0;
            for (int k = 0; k < Q->get_length(); ++k){
                q_dot_a += Q->get(k, i) * A->get(k, j);
            }
            R->set(i, j, q_dot_a);

            for (int k = 0; k < A->get_length(); ++k){
                double val = A->get(k, j) - q_dot_a * Q->get(k, i);
                A->set(k, j, val);
            }

        }
    }

    // Copy Q into A
    for (int i = 0; i < A->get_length(); ++i){
        for (int j = 0; j < A->get_height(); ++j){
            A->set(i, j, Q->get(i, j));
        }
    }
}

void gram_schmidt::solve(Matrix *Q, Matrix *R, ColumnVector *b, ColumnVector *x) {
    // Require a square matrix
    assert(Q->get_height() == Q->get_length());
    // We know that Rx=QT*b=y. This can be solved by backwards substitution since
    // R is upper triangular.
    auto QT = Q->transpose();
    // Create a new vector, y, that is equal to QT*b. QT is Q transposed.
    ColumnVector y = ((QT) * (*b)).get_column(0);
    assert(y.get_height() == x->get_height());
    // Do backwards substitution on upper triangular matrix R to find solution
    // vector y
    for (int i = y.get_height() - 1; i >= 0; i--) {
        double sum = y.get(i);
        for (int j = i + 1; j < y.get_height(); j++) {
            sum -= R->get(i, j) * y.get(j);
        }
        y.set(i, sum/R->get(i, i));
        x->set(i, sum/R->get(i, i));
    }

    // Copy all items from vector y into vector that is visible from outside,
    // x.
    for (int i = 0; i < x->get_height(); ++i){
        x->set(i, y.get(i));
    }
}

void gram_schmidt::inverse(Matrix *Q, Matrix *R, Matrix *B) {
    // Require a square matrix.
    assert(Q->get_height() == Q->get_length());
    auto QT = Q->transpose();
    auto B2 = std::unique_ptr<Matrix>(new Matrix(Q->get_height()));
    // Define a unit vector. Init all entries to 0.
    auto *e_i = new ColumnVector(Q->get_height());
    for (int j = 0; j < e_i->get_height(); ++j){
        e_i->set(j, 0);
    }
    // Iterate over unit vectors
    for (int i = 0; i < Q->get_height(); ++i){
        // Set entry i in unit vector to 1 and the rest to zero.
        e_i->set(i, 1);
        if (i>0){
            // remove 1 from last iteration in unit vector.
            e_i->set(i-1, 0);
        }

        // Solve the equation Ax=e_i. Set the i'th column equal to the solution x
        auto x_i = new ColumnVector(Q->get_height());
        gram_schmidt::solve(Q, R, e_i, x_i);
        for (int j = 0; j < x_i->get_height(); ++j){
            B2->set(i, j, x_i->get(j));
        }
        delete x_i;
    }
    delete e_i;

    // Set B equal to transpose of B2.
    for (int i = 0; i < B2->get_height(); ++i) {
        for (int j = 0; j < B2->get_length(); ++j) {
            B->set(j, i, B2->get(i, j));
        }
    }
}


Matrix* gram_schmidt::inverse(Matrix *A){
    auto *R = new Matrix(A->get_length(), A->get_length());
    gram_schmidt::decompose(A, R);
    auto *B = new Matrix(A->get_height(), A->get_length());
    gram_schmidt::inverse(A, R, B);
    return B;
}