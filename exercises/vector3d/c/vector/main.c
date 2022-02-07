#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "vector.h"


/** 
 * Print a vector
 **/
void vector_print(vector *v){
    for (int i = 0; i <= v->total; i++){
        printf("\t%s\n", (char *) vector_get(v, i));
    }
}


int main()
{
    char* stars = "***********";
    printf("%s Test Vector Implementation %s\n", stars, stars);

    vector v;
    vector_init(&v);

    printf("Empty vector");
    vector_print(&v);
    printf("\n");

    vector_append(&v, "Hello");
    vector_append(&v, "to");
    vector_append(&v, "everyone");
    vector_append(&v, "I");
    vector_append(&v, "know");

    printf("Items of vector\n");
    vector_print(&v);
    printf("\n");

    vector_delete(&v, 3);
    printf("Deleted item at index 3:\n");
    vector_print(&v);
    printf("\n");

    vector_delete(&v, 2);
    printf("Deleted item at index 2:\n");
    vector_print(&v);
    printf("\n");

    vector_delete(&v, 2);
    printf("Deleted item at index 2 again:\n");
    vector_print(&v);
    printf("\n");

    printf("Set new values:\n");
    vector_set(&v, 1, "G'day");
    vector_set(&v, 2, "mate!");
    vector_print(&v);
    printf("\n");

    
    printf("Removing memory.\n");
    vector_free(&v);
    printf("%s Finished Test %s\n", stars, stars);
    return 0;
}