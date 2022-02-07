#include <stdio.h>
#include <stdlib.h>

#include "vector.h"


/** Initalize a vector
 **/
void vector_init(vector *v){
    // Initialize the capacity and the total number of elements in the vector.
    v->capacity = VECTOR_INIT_CAPACITY;
    v->total = 0;
    // Allocate memory of the vector to be equal to the size of a data point 
    // multiplied with the capacity of the vector.

    #ifdef DEBUG_ON
    printf("The sizeof(void *) is %li\n", sizeof(void *));    
    #endif
    
    v->items = malloc(sizeof(void *) * v->capacity);
}


/** 
 * Get the total length of a vector
 **/
int vector_total(vector *v) {
    return v->total;
}

/** 
 * Resize the memory block that stores the items of the vector v.
 * We define it to be static so only functions defined in this file can use it.
 **/
static void vector_resize(vector *v, int new_capacity){
    // Create a pointer to a pointer in memory with unspecified type.
    // Set the size of the memory block to be equal to size of a data point 
    // multiplied new capacity.
    #ifdef DEBUG_ON
    printf("vector_resize: %d to %d\n", v->capacity, new_capacity);
    #endif

    void **items = realloc(v->items, sizeof(void *) * new_capacity);

    // Check if items is nullpointer
    if (items){
        v->capacity = new_capacity;
        v->items = items;
    }
}


/**
 * Append an item of unspecified type to the vector.
 **/
void vector_append(vector *v, void *d){
    // Check if there is any memory left in the vector
    if (v->total == v->capacity){
        // No empty spots in vector. Resize to the double length.
        vector_resize(v, 2*v->capacity);
    }
    // v->items is a pointer to a pointer of unknown data type.
    // Increment v->total and place element last in array.
    v->total += 1;  
    v->items[v->total] = d;

}


/** 
 * Set an index of vector to some element
 **/
void vector_set(vector *v, int ix, void *d){
    if (0 <= ix && ix <= v->total){
        // Appending to vector is only possible if 0 <= ix <= length of vector.
        v->items[ix] = d;
    }
}


/** 
 * Get an item at index of vector
 **/
void *vector_get(vector *v, int ix){
    if (0 <= ix && ix <= v->total){
        // Getting from vector is only possible if 0 <= ix <= length of vector.
        return v->items[ix];
    } else {
        return NULL;
    }
}

/** 
 * Delete an item at index of vector
 **/
void vector_delete(vector *v, int ix){
    // Deleting from vector is only possible if 0 <= ix <= length of vector.
    if (0 <= ix && ix <= v->total){
        // Move all items larger than ix one index down. Only run loop until 
        // v->total - 1 since the new vector length will now be one less.
        for (int i = ix; i < v->total - 1; ++i){
            v->items[i] = v->items[i+1];
            v->items[i+1] = NULL; 
        }
        v->total -= 1;

        // Resize vector if short enough.
        if (v->total > 0 && v->total == v->capacity / 4){
            vector_resize(v, v->capacity / 2);
        }
    } else {
        return;
    }
}


/** 
 * Release all memory taken by a vector
 **/
void vector_free(vector *v){
    free(v->items);
}
