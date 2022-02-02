#include <math.h>
#include <complex.h>
#include <stdio.h>


void print_complex(complex z){
	printf("%f + i%f\n", creal(z), cimag(z));

}

// 2, ei, eiπ, ii, sin(iπ)

int main(){
	double z1 = sqrt(2);
	printf("sqrt(2) = %f\n", z1);

	complex z2 = cexp(I);
	printf("e^i = ");
	print_complex(z2);

	complex z3 = cexp(I * M_PI);
	printf("e^(i*pi) = ");
	print_complex(z3);

	complex z4 = cpow(I, I);
	printf("i^i = ");
	print_complex(z4);

	complex z5 = csin(I * M_PI);
	printf("sin(i*pi) = ");
	print_complex(z5);

	complex z6 = csinh(2.0);
	printf("sinh(2.0) = ");
	print_complex(z6);

	return 0;
}
