//
// Created by Mads Hansen Baattrup on 15/03/2022.
//

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
    /**
     * Construct a Matrix of size n x m
     * @param n The number of rows in the matrix
     * @param m The number of columns in the matrix
     */
    Matrix(int n, int m);

    /**
     * Construct a square matrix.
     * @param n The size of the matrix.
     */
    explicit Matrix(int n) : Matrix(n, n) {}

    /**
     * Print the matrix to standard out stream.
     */
    void print();

    /**
     * Getter for the height of the matrix
     * @return The height of the matrix corresponding to the number of rows.
     */
    int get_height() const {return size1;}

    /**
     * Getter for the length of the matrix
     * @return The length of the matrix corresponding to the number of columns.
     */
    int get_length() const {return size2;}

    /**
     * Copy the matrix
     * @return An identical matrix at a new location on the heap.
     */
    Matrix copy();

    /**
     * Setter for the matrix
     * @param n The row index
     * @param m The column index
     * @param value The value to set in the matrix
     */
    void set(int n, int m, double value);

    /**
     * Getter for the matrix
     * @param n The row index
     * @param m The column index
     * @return The value at index [n, m]
     */
    double get(int n, int m);

    /**
     * Return a column of the matrix as an instance of a ColumnVector
     * @param n The column to return
     * @return ColumnsVector at index n
     */
    ColumnVector get_column(int n);

    /**
     * Return a row of the matrix as an instance of a RowVector
     * @param n The row to return
     * @return RowVector at index n
     */
    RowVector get_row(int n);

    /**
     * Multiply all entries in the matrix with a double
     * @param value The value to multiply with
     * @return New matrix with all entries scaled by value.
     */
    Matrix operator * (double value);

    /**
     * Compute the matrix product between two matrices.
     * @param other Matrix to multiply with
     * @return A new matrix corresponding to the product of the two matrices.
     */
    Matrix operator * (Matrix &other);

    /**
     * Return a transposed version of the matrix.
     * @return Transposed version of this matrix
     */
    Matrix transpose ();

    /**
     * Static method to generate a random matrix of size [n,m] with values on the interval [-10,10].
     * @param n The number of rows in the matrix.
     * @param m The number of columns in the matrix.
     * @return A matrix with random entries of size [n,m]
     */
    static Matrix random_matrix(int n, int m);

    /**
     * Static method to generate an identity matrix of size n.
     * @param n The size of the identity matrix.
     * @return An identity matrix of size n.
     */
    static Matrix identity(int n);

    /**
     * Destructor for the matrix. Deallocate memory on the heap.
     */
    ~Matrix();
    
};

class RowVector: public Matrix{
public:

    /**
     * A RowVector is a matrix with only one row.
     * @param n The number of columns in the vector.
     */
    explicit RowVector(int n) : Matrix(1, n){ }

    /**
     * Set the item at index i of the vector.
     * @param i index
     * @param value The value to set.
     */
    void set(int i, double value);

    /**
    * Get the item at index i of the vector.
    * @param i index
    * @return The value at index.
    */
    double get(int i);

    /**
     * Transpose the vector
     * @return Return a ColumnVector
     */
    ColumnVector transpose ();

    /**
     * Generate a random RowVector of size n with values on the interval [-10,10].
     * @param n Size of ColumnVector to generate
     * @return A RowVector with random entries.
     */
    static RowVector random_vector(int n);
};

class ColumnVector: public Matrix{
public:
    /**
    * A ColumnVector is a matrix with only one column.
    * @param n The number of rows in the vector.
    */
    explicit ColumnVector(int n) : Matrix(n, 1){ }

    /**
    * Set the item at index i of the vector.
    * @param i index
    * @param value The value to set.
    */
    void set(int i, double value);

    /**
    * Get the item at index i of the vector.
    * @param i index
    * @return The value at index.
    */
    double get(int i);

    /**
     * Transpose the vector
     * @return Return a RowVector
     */
    RowVector transpose ();

    /**
     * Generate a random RowVector of size n with values on the interval [-10,10].
     * @param n Size of ColumnVector to generate
     * @return A ColumnVector with random entries.
     */
    static ColumnVector random_vector(int n);
};

#endif MATLIB_MATRIX_HPP