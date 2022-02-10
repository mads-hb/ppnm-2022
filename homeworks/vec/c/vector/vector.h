#ifndef VECTOR_H
#define VECTOR_H


// Define the initial capacity of a vector
#define VECTOR_INIT_CAPACITY 4


/**
 * Define the vector type
 **/
typedef struct vector {
    void **items;
    int capacity;
    int total;
} vector;


/** Initalize a vector
 **/
void vector_init(vector *);



/** 
 * Get the total length of a vector
 **/
int vector_total(vector *);



/**
 * Append an item of unspecified type to the vector.
 **/
void vector_append(vector *, void *);



/** 
 * Set an index of vector to some element
 **/
void vector_set(vector *, int, void *);



/** 
 * Get an item at index of vector
 **/
void *vector_get(vector *, int);



/** 
 * Delete an item at index of vector
 **/
void vector_delete(vector *, int);



/** 
 * Release all memory taken by a vector
 **/
void vector_free(vector *);

#endif
