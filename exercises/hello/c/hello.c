#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>


int main(){
	// Declare pointer to hold username
	char *buf;
	// Allocate memory
	buf = (char *) malloc(10*sizeof(char));
	// Get user name
	buf = getlogin();
	// Print
	printf("Hello, %s!\n", buf);
	return 0;
}
