#define _POSIX_C_SOURCE_199309L

#include <stdlib.h>
#include <stdio.h>
#include <pthread.h>
#include <time.h>
#include "harmonic_sum.h"


void* func(void * p){
    // Function to run in seperate thread. Should take
    // a parameter of type (void *) that is cast to type
    // HarmonicSum and then the sum is computed.
    HarmonicSum *hsum = (HarmonicSum*) p;
    compute_harmonic_sum(hsum);    
    return NULL;
}


int main(){
    // Define number of threads to iterate up to.
    int a = 1, b = (int)1e9;
    int N_MAX = 21;
    
    printf("Compute the harmonic sum on the interval [%i, %i[:\n", a, b);
    for (int n_threads = 1; n_threads < N_MAX; n_threads++) {
        // Define variables used for timing and start a monotonic clock
        // Note: cannot use clock_t tic = clock(); since this measures
        // CPU time and we do multiprocessing.
        struct timespec tic, toc;
        double elapsed = 0;
        clock_gettime(CLOCK_MONOTONIC, &tic);
        
        // Define delta to split into n_threads even intervals
        int delta = (b - a) / n_threads;

        // Define and allocate arrays to hold sums and threads.
        HarmonicSum **data = malloc(sizeof(HarmonicSum*)*n_threads);
        pthread_t* threads = (pthread_t*) malloc(sizeof(pthread_t) * n_threads);
        
        for (int i = 0; i < n_threads; i++){
            // Allocate a sum for each entry in data and set starting 
            // conditions.
            data[i] = (HarmonicSum *) malloc(sizeof(HarmonicSum));
            data[i]->a = a + delta*i;
            data[i]->b = a + delta * (i+1);
            data[i]->sum = 0;

            // Create a thread that works on func with input data[i].
            pthread_create(&threads[i], NULL, func, data[i]);
        }
    
        double sum = 0;
        for (int i = 0; i < n_threads; i++){
            // Join all threads and compute sum.
            pthread_join(threads[i], NULL);
            sum += data[i]->sum;
        }

        // Clean up structs and arrays
        for (int i = 0; i < n_threads; i ++){
            free(data[i]);
        }
        free(threads);
        free(data);

        // Time the process and print to console.
        clock_gettime(CLOCK_MONOTONIC, &toc);
        elapsed = toc.tv_sec - tic.tv_sec;
        elapsed += (toc.tv_nsec - tic.tv_nsec) / 1e9;
        printf("The sum using %i threads is %f and took %fs\n", n_threads, sum, elapsed);   
    }
    return 0;
}
