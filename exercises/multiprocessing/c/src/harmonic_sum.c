#include <stdlib.h>
#include <stdio.h>
#include "harmonic_sum.h"


double compute_harmonic_sum(HarmonicSum *hsum){
    for (int i = hsum->a; i< hsum->b; i++){
        hsum->sum += 1.0/i;
    }
    return hsum->sum;
}
