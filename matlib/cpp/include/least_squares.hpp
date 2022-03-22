//
// Created by Mads Hansen Baattrup on 15/03/2022.
//
#ifndef MATLIB_LEAST_SQUARES_HPP
#define MATLIB_LEAST_SQUARES_HPP

#include <functional>
#include "vector"
#include "matrix.hpp"


namespace least_squares {

    class FitResult {
    private:
        std::vector<double> *coef;
        Matrix *cov;

    public:
        /**
         * Constructor for the FitResult type.
         * @param coef
         * @param covb
         */
        FitResult(std::vector<double>* coef, Matrix* covb);

        /**
         * @return A vector of doubles containing the coefficients for the functions used in fitting.
         */
        std::vector<double>* get_coef() {return coef;}

        /**
         * @return Covariance matrix of fit parameters.
         */
        Matrix* get_covariance() {return cov;}

        /**
         * Destructor.
         */
        ~FitResult();
    };


    /**
     * Perform least squares fitting of input data.
     * @param xs Vector of doubles containing the x values of data
     * @param ys Vector of doubles containing the y values of data
     * @param yerr Vector of doubles containing the y error of data
     * @param funcs Vector of functions used for the linear combination to which data is fitted.
     * @return FitResults containing the coefficients for the functions as well as the covariance matrix.
     */
    FitResult fit(std::vector<double>* xs, std::vector<double>* ys, std::vector<double>* yerr, std::vector<std::function<double (double)>>* funcs);

}

#endif //MATLIB_LEAST_SQUARES_HPP
