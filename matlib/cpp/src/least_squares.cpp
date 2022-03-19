//
// Created by Mads Hansen Baattrup on 15/03/2022.
//

#include <functional>
#include <cassert>
#include <vector>
#include "matrix.hpp"
#include "gram_schmidt.hpp"
#include "least_squares.hpp"


least_squares::FitResult::FitResult(std::vector<double>* coef, Matrix* covb){
    this->coef = coef;
    this->cov = covb;
}

least_squares::FitResult::~FitResult(){
    delete cov;
    delete coef;
}

least_squares::FitResult least_squares::fit(std::vector<double>* xs, std::vector<double>* ys, std::vector<double>* yerr, std::vector<std::function<double (double)>>* funcs){
    int data_length = xs->size();
    assert(data_length == ys->size() && data_length == yerr->size());

    Matrix *A = new Matrix(data_length, funcs->size());
    ColumnVector *b = new ColumnVector(data_length);
    for (int i = 0; i < A->get_height(); i++) {
        for (int k = 0; k < A->get_length(); k++){
            double value = funcs->at(k)(xs->at(i)) / yerr->at(i);
            A->set(i, k, value);
        }
        b->set(i, ys->at(i) / yerr->at(i));
    }

    Matrix *R = new Matrix(A->get_length(), A->get_length());
    gram_schmidt::decompose(A, R);
    ColumnVector *c = new ColumnVector(b->get_height());
    gram_schmidt::solve(A, R, b, c);

    std::vector<double> *coef = new std::vector<double>();
    for (int i = 0; i < c->get_height(); i++){
        coef->emplace_back(c->get(i));
    }

    Matrix R_product = R->transpose() * *R;
    Matrix *cov = gram_schmidt::inverse(&R_product);
    FitResult res(coef, cov);
    return res;
}

