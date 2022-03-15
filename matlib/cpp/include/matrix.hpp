#ifndef MATLIB_MATRIX_HPP
#define MATLIB_MATRIX_HPP

class Matrix;
class RowVector;
class ColumnVector;


class Matrix
{
protected:
    int size1;
    int size2;
    double* data;

public:
    Matrix(int n, int m);

    explicit Matrix(int n) : Matrix(n, n) {}

    void print();

    int get_height() const {return size1;}

    int get_length() const {return size2;}

    Matrix copy();

    void set(int n, int m, double value);

    double get(int n, int m);

    ColumnVector get_column(int n);

    RowVector get_row(int n);

    Matrix operator * (double value);

    Matrix operator * (Matrix &other);

    Matrix transpose ();

    static Matrix random_matrix(int n, int m);

    static Matrix identity(int n);

    ~Matrix();
    
};

class RowVector: public Matrix{
public:
    explicit RowVector(int n) : Matrix(1, n){ }

    void set(int i, double value);

    double get(int i);

    ColumnVector transpose ();

    static RowVector random_vector(int n);
};

class ColumnVector: public Matrix{
public:
    explicit ColumnVector(int n) : Matrix(n, 1){ }

    void set(int i, double value);

    double get(int i);

    RowVector transpose ();

    static ColumnVector random_vector(int n);
};

#endif MATLIB_MATRIX_HPP