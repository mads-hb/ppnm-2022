#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <stdbool.h>
#include <limits.h>


void ExerciseA(){
    int i = 1;
    while(i+1>i) {i++;}
    printf("My max int i = %i\n", i);
    printf("The max int defined in limits.h is %i\n", INT_MAX);
    
    i = 1; 
    while(i - 1 < i){i--;}
    printf("My min int i = %i\n", i);
    printf("The min int defined in limits.h is %i\n", INT_MIN);


    printf("Alternatively, max int is min - 1 = %i\n", i-1);
}

void ExerciseB(){
    long double x=1; while(1+x!=1){x/=2;} x*=2;

    printf("The double precision is %Lg\n", x);
}

void ExerciseC(){
    int n = (int) 1e6;
    double epsilon = pow(2,-52);
    double tiny = epsilon/2;

    double sumA = 0, sumB = 0;
    sumA+=1;
    for(int i=0;i<n;i++){
        sumA += tiny;
    }
    
    printf("sumA-1 = %f should be %f\n", sumA-1, n*tiny);

    for(int i=0;i<n;i++){
        sumB += tiny;
    }
    sumB+=1;
    printf("sumB-1 = %f should be %f\n", sumB-1, n*tiny);
}

bool approx(double a, double b){
    double tau=1e-9;
    double epsilon=1e-9;
    // Remember to use fabs since abs casts to ints.
    return (fabs(a - b) < tau) || (fabs(a-b)/(fabs(a) + fabs(b)) < epsilon);
}

void ExerciseD(){
    printf("Implemented approx method. Let's test it\n");
    printf("approx(1,1) is %d\n", approx(1,1));
    printf("approx(1,2) is %d\n", approx(1,2));
    printf("approx(1,0.5) is %d\n", approx(1,0.5));
    printf("approx(0, pow(2,-52)) is %d\n", approx(0, pow(2,-52)));
    printf("approx(0, pow(2,-52)/2) is %d\n", approx(0, pow(2,-52)/2));
    printf("approx(100, 100+pow(2,-23)) is %d\n", approx(100, 100+pow(2,-23)));

}

 int main(){
    printf("\n***Test starting***\n");
    ExerciseA();
    printf("\n***************\n");
    ExerciseB();
    printf("\n***************\n");
    ExerciseC();
    printf("\n***************\n");
    ExerciseD();
    return 0;
}