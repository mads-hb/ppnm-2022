#include <iostream>
#include <random>
#include <cassert>
#include "matrix.hpp"


Matrix::Matrix(int n, int m) {
    data = new double[n*m];
    size1 = n;
    size2 = m;
};

void Matrix::set(int i, int j, double value){
    assert(i < size1 && j < size2);
    data[i+j*size1] = value;
}

double Matrix::get(int i, int j){
    assert(i < size1 && j < size2);
    return data[i+j*size1];
}

Matrix Matrix::operator * (Matrix &other){
    assert(size2 == other.size1);
    Matrix res(size1, other.size2);

    for (int i = 0; i < size1; ++i){
        for (int j = 0; j < other.size2; ++j){
            double s = 0;
            for (int k = 0; k < other.size1; ++k){
                s += get(i, k) * other.get(k, j);
            }
            res.set(i, j, s);
        }
    }
    return res;
}


Matrix Matrix::operator*(double value) {
    Matrix res(size1, size2);

    for (int i = 0; i < size1; ++i){
        for (int j = 0; j < size2; ++j){
            res.set(i, j, value * res.get(i,j));
        }
    }
    return res;
}

Matrix Matrix::transpose(){
    Matrix res(size2, size1);

//    for (int i = 0; i < size1; i++) {
//        for (int j = 0; j< size2; j++){
//            res.set(j, i, get(i, j));
//        }
//    }
    for (int i = 0; i < size1; ++i){
        for (int j = 0; j < size2; ++j){
            res.set(j, i, get(i, j));
        }
    }

    return res;
}

 Matrix Matrix::random_matrix(int n, int m){
     assert(n >= 0);
     assert(m >= 0);
    std::random_device rd;
    std::default_random_engine eng(rd());
    std::uniform_real_distribution<double> distr(-10, 10);
    Matrix res(n, m);
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < m; j++){
            res.set(i, j, distr(eng));
        }
    }
    return res;

}


Matrix Matrix::identity(int n) {
    assert(n >= 0);
    Matrix res(n);
    for (int i = 0; i < n; ++i) {
        res.set(i, i, 1);
    }
    return res;
}

void Matrix::print(){
    for (int i = 0; i < size1; ++i){
        for (int j = 0; j < size2; ++j){
            printf("%.3f\t", get(i, j));
        }
        printf("\n");
    }
}

Matrix::~Matrix(){
    delete data;
}

ColumnVector Matrix::get_column(int n) {
    ColumnVector res(size1);
    for (int i = 0; i < size1; ++i){
        res.set(i, get(i, n));
    }
    return res;
}

RowVector Matrix::get_row(int n) {
    RowVector res(size2);
    for (int i = 0; i < size2; ++i){
        res.set(i, get(n, i));
    }
    return res;
}

Matrix Matrix::copy() {
    Matrix res(size1, size2);
    for (int i = 0; i < size1; ++i) {
        for (int j = 0; j < size2; ++j) {
            res.set(i, j, get(i, j));
        }
    }
    return res;
}

bool Matrix::operator==(Matrix &other) {
    if (get_height() != other.get_height() || get_length() != other.get_length()){
        return false;
    } else {
        for (int i = 0; i < size1; ++i) {
            for (int j = 0; j < size2; ++j) {
                if (get(i, j) != other.get(i, j)) {
                    return false;
                }
            }
        }
        return true;
    }
}


void RowVector::set(int i, double value) {
    return Matrix::set(0, i, value);
}

double RowVector::get(int i) {
    return Matrix::get(0, i);
}

ColumnVector RowVector::transpose() {
    ColumnVector col_vector(size2);
    for (int i = 0; i < size2; ++i){
        col_vector.set(i, get(i));
    }
    return col_vector;
}

RowVector RowVector::random_vector(int n) {
    return Matrix::random_matrix(1, n).get_row(0);
}

void ColumnVector::set(int i, double value) {
    return Matrix::set(i, 0, value);
}

double ColumnVector::get(int i) {
    return Matrix::get(i, 0);
}

RowVector ColumnVector::transpose() {
    RowVector row_vector(size1);
    for (int i = 0; i < size2; ++i){
        row_vector.set(i, get(i));
    }
    return row_vector;
}

ColumnVector ColumnVector::random_vector(int n) {
    return Matrix::random_matrix(n, 1).get_column(0);
}
